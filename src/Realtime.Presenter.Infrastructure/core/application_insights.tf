resource "azurerm_application_insights" "insights" {
  name = local.application_insights_name
  location = var.location
  application_type = "web"
  resource_group_name = azurerm_resource_group.group.name

  tags = var.common_tags
}