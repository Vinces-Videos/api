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
  region = var.region
  access_key = var.aws_access_id
  secret_key = var.aws_access_key
}

module "setup-iam" {
  source   = "./modules/iam"
  iam-name = "${var.name-prefix}-ecsagent"
}