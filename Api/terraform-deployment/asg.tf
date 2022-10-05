#Create an autoscaling group, heavily restrict at the moment
resource "aws_autoscaling_group" "create-asg" {
  name                 = "vinces-videos-asg-terraform"
  #Use aws_launch_configuration from the ecs.tf setup
  launch_configuration = aws_launch_configuration.create-launch-config.name
  availability_zones   = var.availability_zones
  max_size             = 1
  min_size             = 1
  desired_capacity     = 1
}