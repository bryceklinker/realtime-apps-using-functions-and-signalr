resource "azurerm_resource_group" "group" {
 location = var.location
  name = local.resource_group_name

 tags = var.common_tags
}