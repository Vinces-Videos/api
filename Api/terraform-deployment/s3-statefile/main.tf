# Configure the AWS Provider
provider "aws" {
  region = "eu-west-2"
}

# Create an S3 bucket to store our statefile. This prevents storing the state on file locally which means the state will be persisted in different environments.
resource "aws_s3_bucket" "backend-statefile"{
    bucket = "vinces-videos-statefile"

    # Stops terraform from destroying the object if it exists
    lifecycle {
      prevent_destroy = true
    }

    # Maintain version numbers
    versioning {
      enabled = true
    }

    server_side_encryption_configuration {
      rule {
        apply_server_side_encryption_by_default {
            sse_algorithm = "AES256"
        }
      }
    }
}