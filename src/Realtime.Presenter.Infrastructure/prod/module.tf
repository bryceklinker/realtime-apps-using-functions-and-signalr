variable "function_app_path" {
  type = string
}

variable "env" {
  type = string
}

variable "app_name" {
  type = string
}

module "realtime_presenter_prod" {
  source = "../core"
  
  env = var.env
  app_name = var.app_name
  function_app_path = var.function_app_path
  
  common_tags = {
    env = var.env
    application = var.app_name
  }
}