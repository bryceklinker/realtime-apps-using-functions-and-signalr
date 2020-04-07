resource "azurerm_storage_account" "storage" {
  account_replication_type = "LRS"
  account_tier = "Standard"
  location = var.location
  name = local.storage_account_name
  resource_group_name = azurerm_resource_group.group.name

  tags = var.common_tags
}

resource "azurerm_storage_container" "files_container" {
  name = local.files_container_name
  storage_account_name = azurerm_storage_account.storage.name
  container_access_type = "private"
}

resource "azurerm_storage_container" "deployments" {
  name = local.deployments_container_name
  storage_account_name = azurerm_storage_account.storage.name
}

resource "azurerm_storage_blob" "function_app_blob" {
  name = local.function_app_blob_name
  storage_account_name = azurerm_storage_account.storage.name
  storage_container_name = azurerm_storage_container.deployments.name
  type = "Block"
  source = var.function_app_path
}