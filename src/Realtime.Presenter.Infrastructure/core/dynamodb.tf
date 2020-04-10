//resource "aws_dynamodb_table" "current_slide" {
//
//  hash_key = "id"
//  name = "Presentations"
//  billing_mode = "PROVISIONED"
//  read_capacity  = 20
//  write_capacity = 20
//  
//  attribute {
//    name = "id"
//    type = "S"
//  }
//  
//  tags = var.common_tags
//}