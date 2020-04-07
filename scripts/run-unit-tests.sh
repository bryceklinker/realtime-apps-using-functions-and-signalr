#!/usr/bin/env bash

set -ex

export GITHUB_WORKSPACE=${GITHUB_WORKSPACE:-$(pwd)}

run_node_unit_tests() {
    pushd ${GITHUB_WORKSPACE}/src/Realtime.Presenter.Web
        yarn test
    popd
}

run_dotnet_unit_tests() {
    pushd ${GITHUB_WORKSPACE}/tests/Realtime.Presenter.Function.Tests
        dotnet test
    popd
    
    pushd ${GITHUB_WORKSPACE}/tests/Realtime.Presenter.Mobile.Tests
        dotnet test
    popd
}

main() {
    run_dotnet_unit_tests
    run_node_unit_tests
}

main