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

import {PackageVersion, ApiMessage} from "./github-cli-types";
import fetch from "node-fetch";

export async function deletePackageVersionAsync(packageId: string, version: string, token: string) : Promise<void> {
    return new Promise<void>(async (resolve, reject) => {
        var versionsResult:string|undefined|null|PackageVersion[]|ApiMessage = undefined;
        console.log("Using GitHub API to delete package version...");
        // console.log("Using git token: " + token);
        try {
            var response = await fetch(`https://api.github.com/user/packages/nuget/${packageId}/versions`, {headers: {Authorization: `Bearer ${token}`, Accept: 'application/vnd.github+json'}, method: 'GET'});
            if(response.status == 200)
                versionsResult = (await response.json()) as PackageVersion[];
            else if(response.status == 404)
            {
                console.log(`Package ${packageId} not found.  Skipping...  Message from GitHub: ${response.statusText}`);
                resolve();
            }
            else
            {
                versionsResult = {message: response.statusText, documentation_url: response.url};
                reject(response.statusText);
            }
    
            const versionId = (versionsResult as PackageVersion[]).find((v: PackageVersion) => v.name === version)?.id;

            if (versionId && versionId != undefined) {
                try {
                    var deleteResponse = await fetch(`https://api.github.com/user/packages/nuget/${packageId}/versions/${versionId}`, {headers: {Authorization: `Bearer ${token}`, Accept: 'application/vnd.github+json'}, method: 'DELETE'});
                    if(deleteResponse?.status == 200)
                    {
                        console.log(`The package version ${version} was deleted successfully.`);
                    }
                    else if(deleteResponse.status == 403)
                    {
                        console.log(`The package version ${version} was not deleted because it is the last version.  Deleting the package instead...`);
                        await deletePackageAsync(packageId, token).then(() => resolve());
                    }
                    else if(deleteResponse.status == 404)
                    {
                        console.log(`The package version ${version} was not found.`);
                        resolve();
                    }
                    else if(deleteResponse.status == 401)
                    {
                        console.log(`The package version ${version} was not deleted because the user is not authorized.`);
                        resolve();
                    }
                    else if(deleteResponse.status == 500)
                    {
                        console.log(`The package version ${version} was not deleted because the server encountered an error.`);
                        resolve();
                    }
                }
                catch(ex)
                {
                    reject(ex);
                }
            }
        }
        catch(ex)
        {
            reject(ex);
        }
    });
}

export async function deletePackageAsync(packageId: string, token:string) : Promise<void> {
    return new Promise<void>(async (resolve, reject) => {
        var deletePackageResultJsonString = "";
        console.log(`Deleting package ${packageId}...`);
        try {
            var deleteResponse = await fetch(`https://api.github.com/user/packages/nuget/${packageId}`, {headers: {Authorization: `Bearer ${token}`, Accept: 'application/vnd.github+json'}, method: 'DELETE'});
            if(deleteResponse?.status == 200)
            {
                console.log("The package was deleted successfully.")
                resolve();
            }
            else if(deleteResponse.status == 404)
            {
                console.log(`The package ${packageId} was not found. Skipping...`);
                resolve();
            }
            else if(deleteResponse.status == 401)
            {
                console.log(`The package ${packageId} was not deleted because the user is not authorized.`);
                reject();
            }
        }
        catch(ex) {
            reject();
        }
    });
}
