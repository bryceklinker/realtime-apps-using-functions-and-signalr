locals {
  resource_group_name = "rg-${var.app_name}-${var.env}"
  application_insights_name = "appi-${var.app_name}-${var.env}"
  storage_account_name = "st${var.app_name}${var.env}"
  function_app_name = "func-${var.app_name}-${var.env}"
  app_service_plan_name = "plan-${var.app_name}-${var.env}"
  signalr_name = "signal-${var.app_name}-${var.env}"
  
  files_container_name = "files"
  deployments_container_name = "deployments"
  function_app_blob_name = basename(var.function_app_path)
}