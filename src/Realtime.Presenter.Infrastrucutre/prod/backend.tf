terraform {
  backend "azurerm" {
    resource_group_name = "terraform-storage"
    storage_account_name = "stklinkerterraformstate"
    container_name = "presenter"
    key = "prod.terraform.tfstate"
  }
}