resource "azurerm_signalr_service" "signalr" {
  location = var.location
  name = local.signalr_name
  resource_group_name = azurerm_resource_group.group.name

  sku {
    capacity = 1
    name = "Free_F1"
  }

  features {
    flag = "ServiceMode"
    value = "Serverless"
  }
  tags = var.common_tags
}