variable "env" {
  type = string
}

variable "app_name" {
  type = string
}

variable "function_app_path" {
  type = string
}

variable "common_tags" {
  type = map(string)
}

variable "location" {
  type = string
  default = "centralus"
}

variable "aws_access_key" {
  type = string
}

variable "aws_secret_key" {
  type = string
}

variable "aws_default_region" {
  type = string
}