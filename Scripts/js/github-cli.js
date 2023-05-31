'use strict'
/*
 * github-cli.ts
 *
 *   Created: 2022-11-27-05:39:27
 *   Modified: 2022-12-05-04:14:53
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */
const __awaiter =
  (this && this.__awaiter) ||
  function (thisArg, _arguments, P, generator) {
    function adopt (value) {
      return value instanceof P
        ? value
        : new P(function (resolve) {
          resolve(value)
        })
    }
    return new (P || (P = Promise))(function (resolve, reject) {
      function fulfilled (value) {
        try {
          step(generator.next(value))
        } catch (e) {
          reject(e)
        }
      }
      function rejected (value) {
        try {
          step(generator.throw(value))
        } catch (e) {
          reject(e)
        }
      }
      function step (result) {
        result.done
          ? resolve(result.value)
          : adopt(result.value).then(fulfilled, rejected)
      }
      step((generator = generator.apply(thisArg, _arguments || [])).next())
    })
  }
const __generator =
  (this && this.__generator) ||
  function (thisArg, body) {
    let _ = {
      label: 0,
      sent: function () {
        if (t[0] & 1) throw t[1]
        return t[1]
      },
      trys: [],
      ops: []
    }
    let f
    let y
    let t
    let g
    return (
      (g = { next: verb(0), throw: verb(1), return: verb(2) }),
      typeof Symbol === 'function' &&
        (g[Symbol.iterator] = function () {
          return this
        }),
      g
    )
    function verb (n) {
      return function (v) {
        return step([n, v])
      }
    }
    function step (op) {
      if (f) throw new TypeError('Generator is already executing.')
      while (_) {
        try {
          if (
            ((f = 1),
            y &&
              (t =
                op[0] & 2
                  ? y.return
                  : op[0]
                    ? y.throw || ((t = y.return) && t.call(y), 0)
                    : y.next) &&
              !(t = t.call(y, op[1])).done)
          ) {
            return t
          }
          if (((y = 0), t)) op = [op[0] & 2, t.value]
          switch (op[0]) {
            case 0:
            case 1:
              t = op
              break
            case 4:
              _.label++
              return { value: op[1], done: false }
            case 5:
              _.label++
              y = op[1]
              op = [0]
              continue
            case 7:
              op = _.ops.pop()
              _.trys.pop()
              continue
            default:
              if (
                !((t = _.trys), (t = t.length > 0 && t[t.length - 1])) &&
                (op[0] === 6 || op[0] === 2)
              ) {
                _ = 0
                continue
              }
              if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) {
                _.label = op[1]
                break
              }
              if (op[0] === 6 && _.label < t[1]) {
                _.label = t[1]
                t = op
                break
              }
              if (t && _.label < t[2]) {
                _.label = t[2]
                _.ops.push(op)
                break
              }
              if (t[2]) _.ops.pop()
              _.trys.pop()
              continue
          }
          op = body.call(thisArg, _)
        } catch (e) {
          op = [6, e]
          y = 0
        } finally {
          f = t = 0
        }
      }
      if (op[0] & 5) throw op[1]
      return { value: op[0] ? op[1] : void 0, done: true }
    }
  }
exports.__esModule = true
exports.deletePackageAsync = exports.deletePackageVersionAsync = void 0
const constants_1 = require('./constants')
const core_1 = require('@octokit/core')
let octokit = new core_1.Octokit({ auth: process.env.GITHUB_TOKEN })
function deletePackageVersionAsync (orgId, packageId, version, token) {
  return __awaiter(this, void 0, void 0, function () {
    const _this = this
    return __generator(this, function (_a) {
      return [
        2 /* return */,
        new Promise(function (resolve, reject) {
          return __awaiter(_this, void 0, void 0, function () {
            let versions, versionToDelete, versionId, deleteResponse, ex_1
            let _a, _b
            return __generator(this, function (_c) {
              switch (_c.label) {
                case 0:
                  if (token != undefined) {
                    octokit = new core_1.Octokit({ auth: token })
                  }
                  _c.label = 1
                case 1:
                  _c.trys.push([1, 6, , 7])
                  return [
                    4 /* yield */,
                    octokit.request(
                      'GET /orgs/{org}/packages/{package_type}/{package_name}/versions',
                      {
                        org: orgId,
                        package_type: 'nuget',
                        package_name: packageId,
                        headers:
                          ((_a = {}),
                          (_a[constants_1.GITHUB_API_VERSION_HEADER_NAME] =
                            constants_1.GITHUB_API_VERSION),
                          (_a.Accept =
                            constants_1.GITHUB_API_RESPONSE_CONTENT_TYPE),
                          _a)
                      }
                    )
                  ]
                case 2:
                  versions = _c.sent()
                  console.log('Using GitHub API to delete package version...')
                  versionToDelete = versions.data.find(function (v) {
                    return v.name == version
                  })
                  versionId =
                    versionToDelete === null || versionToDelete === void 0
                      ? void 0
                      : versionToDelete.id
                  if (!(versionId && versionId != undefined)) {
                    return [3 /* break */, 4]
                  }
                  return [
                    4 /* yield */,
                    octokit.request(
                      'DELETE /orgs/{org}/packages/{package_type}/{package_name}/versions/{package_version_id}',
                      {
                        org: orgId,
                        package_type: 'nuget',
                        package_name: packageId,
                        package_version_id: versionId,
                        headers:
                          ((_b = {}),
                          (_b[constants_1.GITHUB_API_VERSION_HEADER_NAME] =
                            constants_1.GITHUB_API_VERSION),
                          (_b.Accept =
                            constants_1.GITHUB_API_RESPONSE_CONTENT_TYPE),
                          _b)
                      }
                    )
                  ]
                case 3:
                  deleteResponse = _c.sent()
                  if (deleteResponse.status != 204) {
                    console.log(
                      'The package version was not deleted because the server encountered an error: '.concat(
                        deleteResponse.status
                      )
                    )
                    reject(deleteResponse.status)
                  }
                  return [3 /* break */, 5]
                case 4:
                  console.log(
                    'The package version '.concat(
                      version,
                      ' was not found. Skipping...'
                    )
                  )
                  _c.label = 5
                case 5:
                  resolve()
                  return [3 /* break */, 7]
                case 6:
                  ex_1 = _c.sent()
                  reject(ex_1)
                  return [3 /* break */, 7]
                case 7:
                  return [2]
              }
            })
          })
        })
      ]
    })
  })
}
exports.deletePackageVersionAsync = deletePackageVersionAsync
function deletePackageAsync (orgId, packageId, token) {
  return __awaiter(this, void 0, void 0, function () {
    const _this = this
    return __generator(this, function (_a) {
      return [
        2 /* return */,
        new Promise(function (resolve, reject) {
          return __awaiter(_this, void 0, void 0, function () {
            let deletePackageResultJsonString, deleteResponse, ex_2
            let _a
            return __generator(this, function (_b) {
              switch (_b.label) {
                case 0:
                  if (token != undefined) {
                    octokit = new core_1.Octokit({ auth: token })
                  }
                  deletePackageResultJsonString = ''
                  console.log('Deleting package '.concat(packageId, '...'))
                  _b.label = 1
                case 1:
                  _b.trys.push([1, 3, , 4])
                  return [
                    4 /* yield */,
                    octokit.request(
                      "DELETE '/orgs/{org}/GET /user/packages/GET /user/packages/{package_type}/{package_name}",
                      {
                        org: orgId,
                        package_type: 'nuget',
                        package_name: packageId,
                        headers:
                          ((_a = {}),
                          (_a[constants_1.GITHUB_API_VERSION_HEADER_NAME] =
                            constants_1.GITHUB_API_VERSION),
                          (_a.Accept =
                            constants_1.GITHUB_API_RESPONSE_CONTENT_TYPE),
                          _a)
                      }
                    )
                  ]
                case 2:
                  deleteResponse = _b.sent()
                  if (deleteResponse.status == 204) {
                    console.log('The package was deleted successfully.')
                    resolve()
                  } else {
                    console.log(
                      'The package '
                        .concat(
                          packageId,
                          ' was not deleted because the server encountered an error: '
                        )
                        .concat(deleteResponse.status)
                    )
                    reject(deleteResponse.status)
                  }
                  return [3 /* break */, 4]
                case 3:
                  ex_2 = _b.sent()
                  reject(ex_2)
                  return [3 /* break */, 4]
                case 4:
                  return [2]
              }
            })
          })
        })
      ]
    })
  })
}
exports.deletePackageAsync = deletePackageAsync
