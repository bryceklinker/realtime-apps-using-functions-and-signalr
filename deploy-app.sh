#!/bin/bash

export LOCATION='centralus'
export RESOURCE_GROUP_NAME='realtime-app-rg'

export FUNCTION_APP_NAME='realtime-app-func'

export APP_INSIGHTS_KEY=''

export STORAGE_ACCOUNT_NAME='realtimepresappstorage'
export STORAGE_ACCOUNT_CONNECTION_STRING=''

export SIGNALR_NAME='realtime-app-signalr'
export SIGNALR_KEY=''
export SIGNALR_ENDPOINT="https://${SIGNALR_NAME}.service.signalr.net"

main() {
    create_resource_group_if_not_exists
    create_storage_account_if_not_exists
    create_signalr_if_not_exists
    create_app_insights_if_not_exists
    create_function_app_if_not_exists
    deploy_function_app
    update_function_app_settings
}

create_resource_group_if_not_exists() {
    echo "Checking that resource group ${RESOURCE_GROUP_NAME} exists.."
    EXISTS=$(az group exists -n "${RESOURCE_GROUP_NAME}")

    if [ "${EXISTS}" = "false" ]; then 
        echo "Creating resource group ${RESOURCE_GROUP_NAME}..."
        az group create -l "${LOCATION}" -n "${RESOURCE_GROUP_NAME}"
        echo "Created resource group ${RESOURCE_GROUP_NAME}."
    fi
}

create_storage_account_if_not_exists() {
    echo "Checking that storage account ${STORAGE_ACCOUNT_NAME} exists..."
    az storage account show -g "${RESOURCE_GROUP_NAME}" -n "${STORAGE_ACCOUNT_NAME}"

    if [ $? != 0 ]; then
        echo "Creating storage account ${STORAGE_ACCOUNT_NAME}..."
        az storage account create -n "${STORAGE_ACCOUNT_NAME}" -g "${RESOURCE_GROUP_NAME}" -l "${LOCATION}" --sku 'Standard_LRS'
        echo "Created storage account ${STORAGE_ACCOUNT_NAME}."
    fi

    STORAGE_ACCOUNT_CONNECTION_STRING=$(az storage account show-connection-string -g "${RESOURCE_GROUP_NAME}" -n "${STORAGE_ACCOUNT_NAME}" | jq .connectionString -r)
}

create_signalr_if_not_exists() {
    echo "Checking that signalr service ${SIGNALR_NAME} exists..."
    az signalr show -n "${SIGNALR_NAME}" -g "${RESOURCE_GROUP_NAME}"

    if [ $? != 0 ]; then
        echo "Creating signalr ${SIGNALR_NAME}..."
        az signalr create -n "${SIGNALR_NAME}" -g "${RESOURCE_GROUP_NAME}" --sku "Free_DS2" --location "${LOCATION}"
        echo "Created signalr ${SIGNALR_NAME}."
    fi

    SIGNALR_KEY=$(az signalr key list -n "${SIGNALR_NAME}" -g "${RESOURCE_GROUP_NAME}" | jq .primaryKey -r)
}

create_app_insights_if_not_exists() {
    echo "Checking if app insights ${FUNCTION_APP_NAME} exists..."
    az resource show -g "${RESOURCE_GROUP_NAME}" \
        -n "${FUNCTION_APP_NAME}" \
        --resource-type "Microsoft.Insights/components"

    if [ $? != 0 ]; then
        echo "Creating function app ${FUNCTION_APP_NAME}..."
        az resource create -g "${RESOURCE_GROUP_NAME}" \
            -n "${FUNCTION_APP_NAME}-insights" \
            --resource-type "Microsoft.Insights/components" \
            --location "${LOCATION}" \
            --properties '{"Application_Type":"Web"}'
        echo "Created function app ${FUNCTION_APP_NAME}."
    fi

    APP_INSIGHTS_KEY=$(az resource show -g "${RESOURCE_GROUP_NAME}" -n "${FUNCTION_APP_NAME}" --resource-type "Microsoft.Insights/components" | jq .properties.InstrumentationKey -r)
}

create_function_app_if_not_exists() {
    echo "Checking if function app ${FUNCTION_APP_NAME} exists..."
    az functionapp show -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}"

    if [ $? != 0 ]; then
        echo "Creating function app ${FUNCTION_APP_NAME}..."
        az functionapp create \
            --consumption-plan-location "${LOCATION}" \
            -n "${FUNCTION_APP_NAME}" \
            --os-type Windows \
            -g "${RESOURCE_GROUP_NAME}" \
            --runtime dotnet \
            --storage-account "${STORAGE_ACCOUNT_NAME}"
        echo "Created function app ${FUNCTION_APP_NAME}."
    fi
}

update_function_app_settings() {
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "AzureWebJobsStorage=${STORAGE_ACCOUNT_CONNECTION_STRING}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "StorageAccountConnectionString=${STORAGE_ACCOUNT_CONNECTION_STRING}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "SignalR:Endpoint=${SIGNALR_ENDPOINT}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "SignalR:Key=${SIGNALR_KEY}"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "WEBSITE_RUN_FROM_PACKAGE=1"
    az functionapp config appsettings set -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --settings "APPINSIGHTS_INSTRUMENTATIONKEY=${APP_INSIGHTS_KEY}"

    az functionapp cors remove -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --allowed-origins "https://functions.azure.com"
    az functionapp cors remove -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --allowed-origins "https://functions-staging.azure.com"
    az functionapp cors remove -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --allowed-origins "https://functions-next.azure.com"
    az functionapp cors remove -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --allowed-origins "*"
    az functionapp cors add -n "${FUNCTION_APP_NAME}" -g "${RESOURCE_GROUP_NAME}" --allowed-origins "*"
}

deploy_function_app() {
    echo "Deploying to ${FUNCTION_APP_NAME}..."
    az functionapp deployment source config-zip -g "${RESOURCE_GROUP_NAME}" -n "${FUNCTION_APP_NAME}" --src "${ARCHIVED_FUNCTION_PATH}"
    az functionapp restart -g "${RESOURCE_GROUP_NAME}" -n "${FUNCTION_APP_NAME}"
    echo "Deployed to ${FUNCTION_APP_NAME}."
}

main