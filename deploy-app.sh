#!/bin/bash
set -ex

LOCATION='centralus'
RESOURCE_GROUP_NAME='realtime-app-rg'
FUNCTION_APP_NAME='realtime-app-func'
STORAGE_ACCOUNT_NAME='realtimestorage'

main() {
    create_resource_group_if_not_exists
    create_storage_account_if_not_exists
    create_function_app_if_not_exists
    deploy_function_app
}

login_to_azure() {
    echo 'Logging in to azure...'
    az login --username $AZURE_USERNAME --password $AZURE_PASSWORD
    echo 'Finished logging into azure.'
}

create_resource_group_if_not_exists() {
    echo "Checking that resource group ${RESOURCE_GROUP_NAME} exists.."
    EXISTS=$(az group exists -n "${RESOURCE_GROUP_NAME}")

    if [ "${EXISTS}" = "false" ]; then 
        echo "Creating resource group ${RESOURCE_GROUP_NAME}..."
        az group create -l "${LOCAITON}" -n "${RESOURCE_GROUP_NAME}"
        echo "Created resource group ${RESOURCE_GROUP_NAME}."
    fi
}

create_storage_account_if_not_exists() {
    echo "Checking that storage account ${STORAGE_ACCOUNT_NAME} exists..."
    EXISTS=$(az storage account show -g "${RESOURCE_GROUP_NAME}" -n "${STORAGE_ACCOUNT_NAME}")

    if [ $EXISTS = 1 ]; then
        echo "Creating storage account ${STORAGE_ACCOUNT_NAME}..."
        az storage account create -n "${STORAGE_ACCOUNT_NAME}" -g "${RESOURCE_GROUP_NAME}" -l "${LOCAITON}"
        echo "Created storage account ${STORAGE_ACCOUNT_NAME}."
    fi
}

create_function_app_if_not_exists() {
    echo "Checking if function app ${FUNCTION_APP_NAME}..."
    EXISTS=$(az functionapp show --name "${FUNCTION_APP_NAME}" --resource-group "${RESOURCE_GROUP_NAME}")

    if [ $EXISTS = 1 ]; then
        echo "Creating function app ${FUNCTION_APP_NAME}..."
        az funcitonapp create --consumption_plan-location "${LOCATION}" --name "${FUNCTION_APP_NAME}" --os-type Windows --resource-group "${RESOURCE_GROUP_NAME}" --runtime dotnet --storage-account "${STORAGE_ACCOUNT_NAME}"
        echo "Created function app ${FUNCTION_APP_NAME}."
    fi
}

deploy_function_app() {
    echo "Deploying to ${FUNCTION_APP_NAME}..."
    az functionapp deployment source config-zip -g "${RESOURCE_GROUP_NAME}" -n "${FUNCTION_APP_NAME}" --src "${ARCHIVED_FUNCTION_PATH}"
    echo "Deployed to ${FUNCTION_APP_NAME}."
}

main