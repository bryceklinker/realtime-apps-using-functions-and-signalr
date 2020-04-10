resource "aws_dynamodb_table" "current_slide" {

  hash_key = "id"
  name = "Presentations"
  
  attribute {
    name = "id"
    type = "S"
  }
  
  tags = var.common_tags
}