"use strict";
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
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
exports.__esModule = true;
exports.deletePackageAsync = exports.deletePackageVersionAsync = void 0;
var node_fetch_1 = __importDefault(require("node-fetch"));
function deletePackageVersionAsync(packageId, version, token) {
    return __awaiter(this, void 0, void 0, function () {
        var _this = this;
        return __generator(this, function (_a) {
            return [2 /*return*/, new Promise(function (resolve, reject) { return __awaiter(_this, void 0, void 0, function () {
                    var versionsResult, response, versionId, deleteResponse, ex_1, ex_2;
                    var _a;
                    return __generator(this, function (_b) {
                        switch (_b.label) {
                            case 0:
                                versionsResult = undefined;
                                console.log("Using GitHub API to delete package version...");
                                _b.label = 1;
                            case 1:
                                _b.trys.push([1, 14, , 15]);
                                return [4 /*yield*/, (0, node_fetch_1["default"])("https://api.github.com/user/packages/nuget/".concat(packageId, "/versions"), { headers: { Authorization: "Bearer ".concat(token), Accept: 'application/vnd.github+json' }, method: 'GET' })];
                            case 2:
                                response = _b.sent();
                                if (!(response.status == 200)) return [3 /*break*/, 4];
                                return [4 /*yield*/, response.json()];
                            case 3:
                                versionsResult = (_b.sent());
                                return [3 /*break*/, 5];
                            case 4:
                                if (response.status == 404) {
                                    console.log("Package ".concat(packageId, " not found.  Skipping...  Message from GitHub: ").concat(response.statusText));
                                    resolve();
                                }
                                else {
                                    versionsResult = { message: response.statusText, documentation_url: response.url };
                                    reject(response.statusText);
                                }
                                _b.label = 5;
                            case 5:
                                versionId = (_a = versionsResult.find(function (v) { return v.name === version; })) === null || _a === void 0 ? void 0 : _a.id;
                                if (!(versionId && versionId != undefined)) return [3 /*break*/, 13];
                                _b.label = 6;
                            case 6:
                                _b.trys.push([6, 12, , 13]);
                                return [4 /*yield*/, (0, node_fetch_1["default"])("https://api.github.com/user/packages/nuget/".concat(packageId, "/versions/").concat(versionId), { headers: { Authorization: "Bearer ".concat(token), Accept: 'application/vnd.github+json' }, method: 'DELETE' })];
                            case 7:
                                deleteResponse = _b.sent();
                                if (!((deleteResponse === null || deleteResponse === void 0 ? void 0 : deleteResponse.status) == 200)) return [3 /*break*/, 8];
                                console.log("The package version ".concat(version, " was deleted successfully."));
                                return [3 /*break*/, 11];
                            case 8:
                                if (!(deleteResponse.status == 403)) return [3 /*break*/, 10];
                                console.log("The package version ".concat(version, " was not deleted because it is the last version.  Deleting the package instead..."));
                                return [4 /*yield*/, deletePackageAsync(packageId, token).then(function () { return resolve(); })];
                            case 9:
                                _b.sent();
                                return [3 /*break*/, 11];
                            case 10:
                                if (deleteResponse.status == 404) {
                                    console.log("The package version ".concat(version, " was not found."));
                                    resolve();
                                }
                                else if (deleteResponse.status == 401) {
                                    console.log("The package version ".concat(version, " was not deleted because the user is not authorized."));
                                    resolve();
                                }
                                else if (deleteResponse.status == 500) {
                                    console.log("The package version ".concat(version, " was not deleted because the server encountered an error."));
                                    resolve();
                                }
                                _b.label = 11;
                            case 11: return [3 /*break*/, 13];
                            case 12:
                                ex_1 = _b.sent();
                                reject(ex_1);
                                return [3 /*break*/, 13];
                            case 13: return [3 /*break*/, 15];
                            case 14:
                                ex_2 = _b.sent();
                                reject(ex_2);
                                return [3 /*break*/, 15];
                            case 15: return [2 /*return*/];
                        }
                    });
                }); })];
        });
    });
}
exports.deletePackageVersionAsync = deletePackageVersionAsync;
function deletePackageAsync(packageId, token) {
    return __awaiter(this, void 0, void 0, function () {
        var _this = this;
        return __generator(this, function (_a) {
            return [2 /*return*/, new Promise(function (resolve, reject) { return __awaiter(_this, void 0, void 0, function () {
                    var deletePackageResultJsonString, deleteResponse, ex_3;
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0:
                                deletePackageResultJsonString = "";
                                console.log("Deleting package ".concat(packageId, "..."));
                                _a.label = 1;
                            case 1:
                                _a.trys.push([1, 3, , 4]);
                                return [4 /*yield*/, (0, node_fetch_1["default"])("https://api.github.com/user/packages/nuget/".concat(packageId), { headers: { Authorization: "Bearer ".concat(token), Accept: 'application/vnd.github+json' }, method: 'DELETE' })];
                            case 2:
                                deleteResponse = _a.sent();
                                if ((deleteResponse === null || deleteResponse === void 0 ? void 0 : deleteResponse.status) == 200) {
                                    console.log("The package was deleted successfully.");
                                    resolve();
                                }
                                else if (deleteResponse.status == 404) {
                                    console.log("The package ".concat(packageId, " was not found. Skipping..."));
                                    resolve();
                                }
                                else if (deleteResponse.status == 401) {
                                    console.log("The package ".concat(packageId, " was not deleted because the user is not authorized."));
                                    reject();
                                }
                                return [3 /*break*/, 4];
                            case 3:
                                ex_3 = _a.sent();
                                reject();
                                return [3 /*break*/, 4];
                            case 4: return [2 /*return*/];
                        }
                    });
                }); })];
        });
    });
}
exports.deletePackageAsync = deletePackageAsync;
