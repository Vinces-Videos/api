#Create an elastic container registry to store the deployment files that github will create
resource "aws_ecr_repository" "create-ecr" {
  name         = lower("${var.name-prefix}-ecr") #ECR name must be lower case
  force_delete = true
}