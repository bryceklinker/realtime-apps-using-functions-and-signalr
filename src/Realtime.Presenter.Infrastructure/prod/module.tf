variable "function_app_path" {
  type = string
}

variable "env" {
  type = string
}

variable "app_name" {
  type = string
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

module "realtime_presenter_prod" {
  source = "../core"
  
  env = var.env
  app_name = var.app_name
  function_app_path = var.function_app_path
  aws_access_key = var.aws_access_key
  aws_default_region = var.aws_default_region
  aws_secret_key = var.aws_secret_key
  
  common_tags = {
    env = var.env
    application = var.app_name
  }
}