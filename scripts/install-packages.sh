#!/usr/bin/env bash
set -ex

export GITHUB_WORKSPACE=${GITHUB_WORKSPACE:-$(pwd)}

install_os_dependencies() {
    brew install terraform
}

restore_dotnet_packages() {
  dotnet restore
}

restore_node_packages() {
  npm install --global yarn
  
  pushd ${GITHUB_WORKSPACE}/src/Realtime.Presenter.Web
    yarn install --network-timeout 1000000
  popd
}

main() {
  install_os_dependencies
  restore_dotnet_packages
  restore_node_packages
}

main