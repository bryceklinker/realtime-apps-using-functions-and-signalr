module "realtime_presenter_prod" {
  source = "../core"
  
  env = "prod"
  app_name = "presenter"
  function_app_path = "../function.zip"
  
  common_tags = {
    env = "prod"
    application = "presenter"
  }
}