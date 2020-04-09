data "azurerm_storage_account_sas" "function_shared_access" {
  connection_string = azurerm_storage_account.storage.primary_connection_string
  https_only = true
  start = "2020-04-07"
  expiry = "2021-04-07"
  resource_types {
    container = false
    object = true
    service = false
  }
  services {
    blob = true
    file = false
    queue = false
    table = false
  }
  permissions {
    add = false
    create = false
    delete = false
    list = false
    process = false
    read = true
    update = false
    write = false
  }
}

resource "azurerm_app_service_plan" "function_app_plan" {
  name = local.app_service_plan_name
  resource_group_name = azurerm_resource_group.group.name
  location = var.location
  kind = "FunctionApp"
  sku {
    tier = "Dynamic"
    size = "Y1"
  }

  tags = var.common_tags
}

resource "azurerm_function_app" "function_app" {
  app_service_plan_id = azurerm_app_service_plan.function_app_plan.id
  location = var.location
  name = local.function_app_name
  resource_group_name = local.resource_group_name
  storage_connection_string = azurerm_storage_account.storage.primary_connection_string
  version = "~3"

  app_settings = {
    FUNCTIONS_EXTENSION_VERSION = "~3"
    APPINSIGHTS_INSTRUMENTATIONKEY = azurerm_application_insights.insights.instrumentation_key
    FUNCTIONS_WORKER_RUNTIME = "dotnet"
    https_only = true
    FUNCTION_APP_EDIT_MODE = "readonly"
    WEBSITE_RUN_FROM_PACKAGE = "https://${azurerm_storage_account.storage.name}.blob.core.windows.net/${azurerm_storage_container.deployments.name}/${azurerm_storage_blob.function_app_blob.name}${data.azurerm_storage_account_sas.function_shared_access.sas}"
    StorageAccountConnectionString = azurerm_storage_account.storage.primary_connection_string
    AzureSignalRConnectionString = azurerm_signalr_service.signalr.primary_connection_string
  }
  
  site_config {
    cors {
      allowed_origins = [
        "*"
      ] 
    }
  }

  tags = var.common_tags
}