#System variables are set up here. Those with defaults can be left out of each of the .tfvar files if desired
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

variable "ec2-instance-type" {
  # Passed in from Github secrets when the workflow is ran
  default = "t2.micro"
  description = "The EC2 instance type"
  type = string
}

variable "ec2-ami-id" {
  default = "ami-070d0f1b66ccfd0fa"
  description = "The EC2 AMI id to use"
  type = string
}

variable "name-prefix" {
  default = "vinces-videos-"
  description = "The name prefix to use"
  type = string
}

variable "ecs-service-name" {
  default = null
  description = "The name to use for the elastic container service"
  type = string
}

variable "ecs-iam-agent-name" {
    default = "vinces-videos-ecs-agent"
    description = "The name of the IAM user created to serve as the ecs agent"
    type = string
}

variable "region" {
    default = "eu-west-2"
    description = "The region in which to set up the service"
    type = string
}

variable "availability_zones" {
    default = ["eu-west-2b"]
    description = "A list of availability zones for the autoscaling group (can be used in other places)"
    type = list(string)
}





