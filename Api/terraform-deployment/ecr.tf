#Create an elastic container registry to store the deployment files that github will create
resource "aws_ecr_repository" "create-ecr" {
  name         = "vinces-videos-api-terraform"
  force_delete = true
}