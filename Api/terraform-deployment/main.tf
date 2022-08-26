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

#start section
#for ecs hook up to the ec2 instances
data "aws_iam_policy_document" "ecs_agent" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ec2.amazonaws.com", "ecs-tasks.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "ecs_agent" {
  name               = "ecs-agent"
  assume_role_policy = data.aws_iam_policy_document.ecs_agent.json
}

resource "aws_iam_role_policy_attachment" "ecs_agent" {
  role       = aws_iam_role.ecs_agent.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonEC2ContainerServiceforEC2Role"
}

resource "aws_iam_role_policy_attachment" "ecs_agent_2" {
  role       = aws_iam_role.ecs_agent.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}

resource "aws_iam_instance_profile" "ecs_agent" {
  name = "ecs-agent"
  role = aws_iam_role.ecs_agent.name
}
#end section

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
  execution_role_arn       = aws_iam_role.ecs_agent.arn
  task_role_arn            = aws_iam_role.ecs_agent.arn
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
      #scratch is a publically hosted docker image. This gets us around the requirement for an image
      "image" : "scratch",
      #"image": "${aws_ecr_repository.create-ecr.arn}",
      #"image" : "668375330582.dkr.ecr.eu-west-2.amazonaws.com/vinces-videos-api-terraform:4b24795e9cebabc512e45332fa5c3972b0db2a0c"
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

#Create launch configuration for the EC2 instances we will be using as a template
resource "aws_launch_configuration" "create-launch-config" {
  name_prefix   = "vinces-videos-terraform-"
  image_id      = "ami-070d0f1b66ccfd0fa"
  instance_type = "t2.micro"
  key_name = "vincesvideo-api-keypair"
  security_groups = [ "sg-08a208eb04992a77c" ]
  iam_instance_profile = aws_iam_instance_profile.ecs_agent.arn
  user_data = "#!/bin/bash\necho ECS_CLUSTER=${aws_ecs_cluster.create-ecs-cluster.name} >> /etc/ecs/ecs.config"
  lifecycle {
    create_before_destroy = true
  }
}

#Create an autoscaling group, heavily restrict at the moment
resource "aws_autoscaling_group" "create-asg" {
  name                 = "vinces-videos-asg-terraform"
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