#!/usr/bin/env bash

set -ex

export ENVIRONMENT="${ENVIRONMENT:-'prod'}"
export APP_NAME="${APP_NAME:-'presenter'}"
export HASH=$(git rev-parse HEAD)
export GITHUB_WORKSPACE=${GITHUB_WORKSPACE:-$(pwd)}
export FUNCTION_APP_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Function"
export INFRASTRUCTURE_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Infrastructure"
export FUNCTION_BUILD_PATH="${INFRASTRUCTURE_PATH}/build/function_app"
export FUNCTION_ZIP_PATH="${INFRASTRUCTURE_PATH}/build/function_app_${HASH}.zip"
export WEB_SOURCE_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Web"
export STORAGE_ACCOUNT_NAME="stp${APP_NAME}${ENVIRONMENT}"
export STORAGE_FILES_CONTAINER="files"

build_function_app() {
    dotnet publish ${FUNCTION_APP_PATH} --configuration Release --output ${FUNCTION_BUILD_PATH}
    
    pushd ${FUNCTION_BUILD_PATH}
        zip -r ${FUNCTION_ZIP_PATH} ./*
    popd
     
}

deploy_infrastructure() {
    pushd ${INFRASTRUCTURE_PATH}/prod
        terraform init
        terraform plan -out=deploy.tfplan -var="function_app_path=${FUNCTION_ZIP_PATH}" -var="env=${ENVIRONMENT}" -var="app_name=${APP_NAME}"
        terraform apply -input=false ./deploy.tfplan
    popd
}

copy_web_content_to_blob_storage() {
    pushd ${WEB_SOURCE_PATH}
        yarn build
        
        az storage blob upload-batch -d "${STORAGE_FILES_CONTAINER}" --account-name "${STORAGE_ACCOUNT_NAME}" --account-key ${ARM_ACCESS_KEY} --source "./dist"
    popd
}

main() {
    build_function_app
    deploy_infrastructure
    copy_web_content_to_blob_storage
}

main