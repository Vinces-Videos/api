terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.27.0"
    }
  }
}

# Configure the AWS Provider
provider "aws" {
  region = "eu-west-2"
}

variable "iamExecutionRole" {
  type    = string
  default = "arn:aws:iam::668375330582:role/ecsInstanceRole"
}

#Create an elastic container registry to store the deployment files that github will create
resource "aws_ecr_repository" "create-ecr" {
  name = "vinces-videos-api-terraform"
}

#Should we push a version of the image up to our repo

resource "aws_ecs_cluster" "create-ecs-cluster" {
  name = "vinces-videos-cluster-terraform"
}

resource "aws_ecs_task_definition" "load-task-definitions" {
  family = "vinces-videos-task-definitions-terraform"
  #This could also be loaded from a file but we depend on the ecr repository arn specified in a previous step
  requires_compatibilities = ["EC2"]
  execution_role_arn       = var.iamExecutionRole
  task_role_arn            = var.iamExecutionRole
  container_definitions = jsonencode([
    {
      "dnsSearchDomains" : null,
      "environmentFiles" : null,
      "logConfiguration" : null,
      "entryPoint" : null,
      "portMappings" : [
        {
          "hostPort" : 80,
          "protocol" : "tcp",
          "containerPort" : 5000
        }
      ],
      "command" : null,
      "linuxParameters" : null,
      "cpu" : 0,
      "environment" : [
        {
          "name" : "ASPNETCORE_URLS",
          "value" : "http://*:5000"
        }
      ],
      "resourceRequirements" : null,
      "ulimits" : null,
      "dnsServers" : null,
      "mountPoints" : [],
      "workingDirectory" : null,
      "secrets" : null,
      "dockerSecurityOptions" : null,
      "memory" : 300,
      "memoryReservation" : null,
      "volumesFrom" : [],
      "stopTimeout" : null,
      #"image": "${aws_ecr_repository.create-ecr.arn}",
      "image" : "668375330582.dkr.ecr.eu-west-2.amazonaws.com/vinces-videos-api-terraform:4b24795e9cebabc512e45332fa5c3972b0db2a0c"
      "startTimeout" : null,
      "firelensConfiguration" : null,
      "dependsOn" : null,
      "disableNetworking" : null,
      "interactive" : null,
      "healthCheck" : null,
      "essential" : true,
      "links" : null,
      "hostname" : null,
      "extraHosts" : null,
      "pseudoTerminal" : null,
      "user" : null,
      "readonlyRootFilesystem" : null,
      "dockerLabels" : null,
      "systemControls" : null,
      "privileged" : null,
      "name" : "vinces-videos-container-terraform"
    }]
  )
}


resource "aws_launch_configuration" "create-launch-config" {
  name_prefix   = "vinces-videos-terraform-"
  image_id      = "ami-070d0f1b66ccfd0fa"
  instance_type = "t2.micro"
  key_name = "vincesvideo-api-keypair"
  security_groups = [ "sg-08a208eb04992a77c" ]
}

resource "aws_autoscaling_group" "create-asg" {
  name                 = "vinces-videos-asg-terraform-"
  launch_configuration = aws_launch_configuration.create-launch-config.name
  availability_zones = [ "eu-west-2b" ]
  max_size = 1
  min_size = 1
  desired_capacity = 1
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