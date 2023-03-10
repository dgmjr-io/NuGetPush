#!/bin/zsh

# // ts-node "$( cd ${0%/*} && pwd -P )/ts/delete-github-package-version.ts" $ 1 $2 $3 $4 $5 $6 $7 $8 $9
node "$PSScriptRoot/js/delete-github-package-version.js" $args[0] $args[1] $args[2] $args[3]
