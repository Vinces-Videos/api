terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.27.0"
    }
  }

  backend "s3" {
    bucket = "vinces-videos-statefile"
    key = "global/s3/terraform.tfstate"
    region = "eu-west-2"
    #Variables are not allowed here otherwise I'd have specified the access key here
  }
}

# Configure the AWS Provider
provider "aws" {
  region = "eu-west-2"
  access_key = var.aws_access_id
  secret_key = var.aws_access_key
}

variable "aws_access_id" {
  # When not passed in, this is indicative of running from someones computer where it's assumed they have the correct environment variables setup for AWS
  default = null
  description = "Passed in from Github secrets when the workflow is ran"
  type = string
}

variable "aws_access_key" {
  # When not passed in, this is indicative of running from someones computer where it's assumed they have the correct environment variables setup for AWS
  default = null
  description = "Passed in from Github secrets when the workflow is ran"
  type = string
}

module "setup-iam" {
  source   = "./modules/iam"
  iam-name = "ecs-agent-vinces-videos"
}

#Should we push a version of the image up to our repo

resource "aws_ecs_cluster" "create-ecs-cluster" {
  name = "vinces-videos-cluster-terraform"
}

resource "aws_ecs_task_definition" "load-task-definitions" {
  family = "vinces-videos-task-definitions-terraform"
  #This could also be loaded from a file but we depend on the ecr repository arn specified in a previous step
  requires_compatibilities = ["EC2"]
  execution_role_arn       = module.setup-iam.iam-agent-arn
  task_role_arn            = module.setup-iam.iam-agent-arn
  container_definitions    = file("../../task-definitions.json")
}

#Create launch configuration for the EC2 instances we will be using as a template
resource "aws_launch_configuration" "create-launch-config" {
  name_prefix          = "vinces-videos-terraform-"
  image_id             = "ami-070d0f1b66ccfd0fa"
  instance_type        = "t2.micro"
  key_name             = "vincesvideo-api-keypair"
  security_groups      = ["sg-08a208eb04992a77c"]
  iam_instance_profile = module.setup-iam.iam-instance-profile-arn
  user_data            = "#!/bin/bash\necho ECS_CLUSTER=${aws_ecs_cluster.create-ecs-cluster.name} >> /etc/ecs/ecs.config"
  lifecycle {
    create_before_destroy = true
  }
}

#Create an autoscaling group, heavily restrict at the moment
resource "aws_autoscaling_group" "create-asg" {
  name                 = "vinces-videos-asg-terraform"
  launch_configuration = aws_launch_configuration.create-launch-config.name
  availability_zones   = ["eu-west-2b"]
  max_size             = 1
  min_size             = 1
  desired_capacity     = 1
}

resource "aws_ecs_service" "create-ecs-service" {
  name = "vinces-videos-service-terraform"
  #default is EC2 anyway
  launch_type                        = "EC2"
  cluster                            = aws_ecs_cluster.create-ecs-cluster.arn
  task_definition                    = aws_ecs_task_definition.load-task-definitions.arn
  desired_count                      = 1
  deployment_minimum_healthy_percent = 100
  deployment_maximum_percent         = 200
  #Optional: Allow external changes without Terraform plan difference
  lifecycle {
    ignore_changes = [desired_count]
  }
}