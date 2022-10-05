#Create a task definition object for the ECS to use
resource "aws_ecs_task_definition" "load-task-definitions" {
  family = "vinces-videos-task-definitions-terraform"
  #This could also be loaded from a file but we depend on the ecr repository arn specified in a previous step
  requires_compatibilities = ["EC2"]
  execution_role_arn       = module.setup-iam.iam-agent-arn
  task_role_arn            = module.setup-iam.iam-agent-arn
  container_definitions    = file("../../task-definitions.json")
}

#Create an elastic container service cluster
resource "aws_ecs_cluster" "create-ecs-cluster" {
  name = "${var.name-prefix}-cluster"
}

#Create launch configuration for the EC2 instances we will be using as a template
resource "aws_launch_configuration" "create-launch-config" {
  name_prefix          = var.name-prefix
  image_id             = var.ec2-ami-id
  instance_type        = var.ec2-instance-type
  key_name             = "vincesvideo-api-keypair"
  security_groups      = ["sg-08a208eb04992a77c"]
  iam_instance_profile = module.setup-iam.iam-instance-profile-arn
  user_data            = "#!/bin/bash\necho ECS_CLUSTER=${aws_ecs_cluster.create-ecs-cluster.name} >> /etc/ecs/ecs.config"
  lifecycle {
    create_before_destroy = true
  }
}

resource "aws_ecs_service" "create-ecs-service" {
  name                               = var.ecs-service-name
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