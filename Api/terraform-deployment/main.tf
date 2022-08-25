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
    type = string
    default = "arn:aws:iam::668375330582:role/ecsInstanceRole"
}

#Create an elastic container registry to store the deployment files that github will create
resource "aws_ecr_repository" "create-ecr" {
    name = "vinces-videos-api-terraform"
}

resource "aws_ecs_cluster" "create-ecs-cluster" {
    name = "vinces-videos-cluster-terraform"
}

resource "aws_ecs_task_definition" "load-task-definitions" {
    family = "vinces-videos-task-definitions-terraform"
    #This could also be loaded from a file but we depend on the ecr repository arn specified in a previous step
    requires_compatibilities = ["EC2"]
    execution_role_arn = var.iamExecutionRole
    task_role_arn = var.iamExecutionRole
    container_definitions = jsonencode([
        {
        "dnsSearchDomains": null,
        "environmentFiles": null,
        "logConfiguration": null,
        "entryPoint": null,
        "portMappings": [
            {
            "hostPort": 80,
            "protocol": "tcp",
            "containerPort": 5000
            }
        ],
        "command": null,
        "linuxParameters": null,
        "cpu": 0,
        "environment": [
            {
            "name":"ASPNETCORE_URLS",
            "value":"http://*:5000"
            }
        ],
        "resourceRequirements": null,
        "ulimits": null,
        "dnsServers": null,
        "mountPoints": [],
        "workingDirectory": null,
        "secrets": null,
        "dockerSecurityOptions": null,
        "memory": 300,
        "memoryReservation": null,
        "volumesFrom": [],
        "stopTimeout": null,
        "image": "${aws_ecr_repository.create-ecr.arn}",
        "startTimeout": null,
        "firelensConfiguration": null,
        "dependsOn": null,
        "disableNetworking": null,
        "interactive": null,
        "healthCheck": null,
        "essential": true,
        "links": null,
        "hostname": null,
        "extraHosts": null,
        "pseudoTerminal": null,
        "user": null,
        "readonlyRootFilesystem": null,
        "dockerLabels": null,
        "systemControls": null,
        "privileged": null,
        "name": "vinces-videos-container-terraform"
        }]
    )
}

resource "aws_ecs_service" "create-ecs-service" {
    name = "vinces-videos-service-terraform"
    #default is EC2 anyway
    launch_type = "EC2" 
    cluster = aws_ecs_cluster.create-ecs-cluster.arn
    task_definition = aws_ecs_task_definition.load-task-definitions.arn
    desired_count = 1
    deployment_minimum_healthy_percent = 100
    deployment_maximum_percent = 200
    #Optional: Allow external changes without Terraform plan difference
    lifecycle {
        ignore_changes = [desired_count]
    }
    ordered_placement_strategy {
        type = "spread"
        field = "attribute:ecs.availability-zone"
    }
}