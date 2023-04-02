"use strict";
/*
 * constants.ts
 *
 *   Created: 2023-03-28-11:52:17
 *   Modified: 2023-03-28-11:52:17
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */
var _a;
exports.__esModule = true;
exports.GITHUB_API_ACCEPT_HEADER = exports.GITHUB_API_VERSION_HEADER = exports.GITHUB_API_RESPONSE_CONTENT_TYPE = exports.GITHUB_API_VERSION_HEADER_NAME = exports.GITHUB_API_VERSION = exports.GITHUB_API_URL = void 0;
exports.GITHUB_API_URL = "https://api.github.com";
exports.GITHUB_API_VERSION = '2022-11-28';
exports.GITHUB_API_VERSION_HEADER_NAME = "X-GitHub-Api-Version";
exports.GITHUB_API_RESPONSE_CONTENT_TYPE = "application/vnd.github.v3+json";
exports.GITHUB_API_VERSION_HEADER = (_a = {}, _a[exports.GITHUB_API_VERSION_HEADER_NAME] = exports.GITHUB_API_VERSION, _a);
exports.GITHUB_API_ACCEPT_HEADER = { Accept: exports.GITHUB_API_RESPONSE_CONTENT_TYPE };
