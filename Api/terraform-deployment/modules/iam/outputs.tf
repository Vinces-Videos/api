output "iam-agent-arn" {
    description = "The ARN for the user created"
    value = aws_iam_role.ecs_agent.arn
}

output "iam-instance-profile-arn" {
    description = "The ARN for the instance profile created"
    value = aws_iam_instance_profile.ecs_agent.arn
}