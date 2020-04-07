#!/usr/bin/env bash

set -ex

export GITHUB_WORKSPACE=${GITHUB_WORKSPACE:-$(pwd)}
export FUNCTION_APP_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Function"
export INFRASTRUCTURE_PATH="${GITHUB_WORKSPACE}/src/Realtime.Presenter.Infrastructure"

build_function_app() {
    dotnet publish ${FUNCTION_APP_PATH} --configuration Release --output "${INFRASTRUCTURE_PATH}/function"
    zip -r "${INFRASTRUCTURE_PATH}/function.zip" "${INFRASTRUCTURE_PATH}/function"
}

deploy_infrastructure() {
    pushd ${INFRASTRUCTURE_PATH}/prod
        terraform init
        terraform plan -out=deploy.tfplan
        terraform apply -input=false ./deploy.tfplan
    popd
}

main() {
    build_function_app
    deploy_infrastructure
}

main