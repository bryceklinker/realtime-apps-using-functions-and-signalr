variable "function_app_path" {
  type = string
}

module "realtime_presenter_prod" {
  source = "../core"
  
  env = "prod"
  app_name = "presenter"
  function_app_path = var.function_app_path
  
  common_tags = {
    env = "prod"
    application = "presenter"
  }
}