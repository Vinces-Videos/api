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
  default = "vinces-videos-terraform-"
  description = "The name prefix to use"
  type = string
}

variable "ecs-service-name" {
  default = null
  description = "The name to use for the elastic container service"
  type = string
}






