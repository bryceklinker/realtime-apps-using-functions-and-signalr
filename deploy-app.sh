#!/bin/bash

brew install jq

LOCATION='centralus'
RESOURCE_GROUP_NAME='realtime-app-rg'
FUNCTION_APP_NAME='realtime-app-func'
STORAGE_ACCOUNT_NAME='realtimestorage'

main() {
    login_to_azure
    create_resource_group_if_not_exists
    create_storage_account_if_not_exists
    create_function_app_if_not_exists
    deploy_function_app
}

login_to_azure() {
    az login -u ${AZURE_USERNAME} -p ${AZURE_PASSWORD}
}

create_resource_group_if_not_exists() {
    EXISTS=$(az group exists -n ${RESOURCE_GROUP_NAME} | jq length)

    if [ "${EXISTS}" = "false" ]; then 
        az group create -l ${LOCAITON} -n ${RESOURCE_GROUP_NAME}
    fi
}

create_storage_account_if_not_exists() {
    EXISTS=$(az storage account show -g ${RESOURCE_GROUP_NAME} -n ${STORAGE_ACCOUNT_NAME})

    if [ $EXISTS = 1 ]; then
        az storage account create -n ${STORAGE_ACCOUNT_NAME} -g ${RESOURCE_GROUP_NAME} -l ${LOCAITON}
    fi
}

create_function_app_if_not_exists() {
    EXISTS=$(az functionapp show --name "${FUNCTION_APP_NAME}" --resource-group "${RESOURCE_GROUP_NAME}")

    if [ $EXISTS = 1 ]; then
        az funcitonapp create --consumption_plan-location ${LOCATION} --name ${FUNCTION_APP_NAME} --os-type Windows --resource-group ${RESOURCE_GROUP_NAME} --runtime dotnet --storage-account ${STORAGE_ACCOUNT_NAME}
    fi
}

deploy_function_app() {
    az functionapp deployment source config-zip -g ${RESOURCE_GROUP_NAME} -n ${FUNCTION_APP_NAME} --src "${ARCHIVED_FUNCTION_PATH}"
}

main