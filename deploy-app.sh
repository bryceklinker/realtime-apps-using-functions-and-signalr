#!/bin/bash
set -e

brew install jq

LOCATION='centralus'
RESOURCE_GROUP_NAME='realtime-app-rg'
FUNCTION_APP_NAME='realtime-app-func'
STORAGE_ACCOUNT_NAME='realtimestorage'
STORAGE_ACCOUNT_CONNECTION_STRING=''

SIGNALR_NAME='realtime-app-signalr'
SIGNALR_KEY=''
SIGNALR_ENDPOINT="https://${SIGNALR_NAME}.service.signalr.net"

main() {
    create_resource_group_if_not_exists
    create_storage_account_if_not_exists
    create_signalr_if_not_exists
    create_function_app_if_not_exists
    update_function_app_settings
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

    STORAGE_ACCOUNT_CONNECTION_STRING=$(az storage account show-connection-string -g "${RESOURCE_GROUP_NAME}" -n "${STORAGE_ACCOUNT_NAME}" | jq .connectionString)
}

create_signalr_if_not_exists() {
    echo "Checking that signalr service ${SIGNALR_NAME} exists..."
    EXISTS=$(az signalr show -n ${SIGNALR_NAME} -g ${RESOURCE_GROUP_NAME})

    if [ $EXISTS = 1 ]; then
        echo "Creating signalr ${SIGNALR_NAME}..."
        az signalr create -n "${SIGNALR_NAME}" -g "${RESOURCE_GROUP_NAME}" --sku "Free_DS2" --location "${LOCATION}"
        echo "Created signalr ${SIGNALR_NAME}."
    fi
}

create_function_app_if_not_exists() {
    echo "Checking if function app ${FUNCTION_APP_NAME}..."
    EXISTS=$(az functionapp show -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}")

    if [ $EXISTS = 1 ]; then
        echo "Creating function app ${FUNCTION_APP_NAME}..."
        az funcitonapp create --consumption_plan-location "${LOCATION}" -n "${FUNCTION_APP_NAME}" --os-type Windows -g "${RESOURCE_GROUP_NAME}" --runtime dotnet --storage-account "${STORAGE_ACCOUNT_NAME}"
        echo "Created function app ${FUNCTION_APP_NAME}."
    fi

    SIGNALR_KEY=$(az signalr key list -n "${SIGNALR_NAME}" -g "${RESOURCE_GROUP_NAME} | jq .primaryKey")
}

update_function_app_settings() {
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "AzureWebJobsStorage=${STORAGE_ACCOUNT_CONNECTION_STRING}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "StorageAccountConnectionString=${STORAGE_ACCOUNT_CONNECTION_STRING}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "SignalR:Endpoint=${SIGNALR_ENDPOINT}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "SignalR:Key=${SIGNALR_KEY}"

    az functionapp cors add -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --allowed-origins "*"
}

deploy_function_app() {
    echo "Deploying to ${FUNCTION_APP_NAME}..."
    az functionapp deployment source config-zip -g "${RESOURCE_GROUP_NAME}" -n "${FUNCTION_APP_NAME}" --src "${ARCHIVED_FUNCTION_PATH}"
    echo "Deployed to ${FUNCTION_APP_NAME}."
}

main