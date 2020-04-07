#!/usr/bin/env bash

set -ex

export HASH=$(git rev-parse HEAD)
export GITHUB_WORKSPACE=${GITHUB_WORKSPACE:-$(pwd)}
export FUNCTION_APP_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Function"
export INFRASTRUCTURE_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Infrastructure"
export FUNCTION_BUILD_PATH="${INFRASTRUCTURE_PATH}/build/function_app"
export FUNCTION_ZIP_PATH="${INFRASTRUCTURE_PATH}/build/function_app_${HASH}.zip"

build_function_app() {
    dotnet publish ${FUNCTION_APP_PATH} --configuration Release --output ${FUNCTION_BUILD_PATH}
    
    pushd ${FUNCTION_BUILD_PATH}
        zip -r ${FUNCTION_ZIP_PATH} ./*
    popd
     
}

deploy_infrastructure() {
    pushd ${INFRASTRUCTURE_PATH}/prod
        terraform init
        terraform plan -out=deploy.tfplan -var="function_app_path=${FUNCTION_ZIP_PATH}"
        terraform apply -input=false ./deploy.tfplan
    popd
}

main() {
    build_function_app
    deploy_infrastructure
}

main