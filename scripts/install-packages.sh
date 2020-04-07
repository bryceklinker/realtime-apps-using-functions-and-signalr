#!/usr/bin/env bash
set -ex

export GITHUB_WORKSPACE=${GITHUB_WORKSPACE:-$(pwd)}

restore_dotnet_packages() {
  dotnet restore
}

restore_node_packages() {
  npm install --global yarn
  
  pushd ${GITHUB_WORKSPACE}/src/Realtime.Presenter.Web
    yarn install  
  popd
}

main() {
  restore_dotnet_packages
  restore_node_packages
}

main