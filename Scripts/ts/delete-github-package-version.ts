/*
 * delete-github-package-version.ts
 *
 *   Created: 2022-11-27-05:39:27
 *   Modified: 2022-12-05-04:15:02
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

import process from "process";
import { deletePackageVersionAsync } from "./github-cli.js";
import { PackageType } from "./github-cli-types.js";
import yargs from "yargs/yargs";
import { hideBin } from "yargs/helpers";
import { SemVer } from "semver";
// const argv = yargs(hideBin(process.argv));

const main = async () => {
  console.log(`yargs: ${JSON.stringify(yargs)}`);

  // console.log(`argv: ${JSON.stringify(argv)}`);

  const ToSemVer = (stringVer: string) => new SemVer(stringVer);
  console.log(`ToSemVer: ${JSON.stringify(ToSemVer)}`);

  const ToPackageType = (stringType: string) => stringType as PackageType;
  console.log(`ToPackageType: ${JSON.stringify(ToPackageType)}`);

  const argv = await yargs(hideBin(process.argv))
    .usage(
      "Usage: $0 <org> <package-id> <package-version> <package-type> [github-token]",
    )
    .option("org", {
      type: "string",
      describe: "The organization that owns the package",
      demandOption: true,
      string: true,
      message: "The organization is required",
      alias: ["o", "organization", "org"],
    })
    .option("package-id", {
      type: "string",
      describe: "The package's ID/name",
      demandOption: true,
      string: true,
      message: "The package ID is required",
      alias: ["i", "id", "package-id", "pkgid"],
    })
    .option("package-version", {
      describe: "The package's semver version number",
      demandOption: true,
      message: "Package version is required",
      alias: ["v", "version", "package-version"],
      coerce: ToSemVer,
    })
    .option("type", {
      type: "string",
      describe:
        "The package's type (one of npm, maven, rubygems, docker, nuget, container)",
      choices: ["npm", "maven", "rubygems", "docker", "nuget", "container"],
      alias: ["t", "type", "package-tyoe"],
      default: "nuget",
      coerce: ToPackageType,
    })
    .option("token", {
      type: "string",
      describe: "The GitHub API token",
      alias: ["token", "t"],
      string: true,
      default: process.env.GITHUB_TOKEN,
    })
    // .showCompletionScript()
    .showHelpOnFail(true)
    .help().argv;

  console.log(`argv: ${JSON.stringify(argv)}`);

  // const Argsv = await argv.argv;

  // console.log(`argsv: ${JSON.stringify(Argsv)}`);

  const orgId = argv.org;
  const packageId = argv.packageId;
  const version = argv.packageVersion;
  const packageType = argv.type;
  const token = argv.token;

  console.log(
    `Args: org: ${orgId}, packageId: ${packageId}, version: ${version}, packageType: ${packageType}, token: ${token}`,
  );

  await deletePackageVersionAsync(
    orgId,
    packageId,
    version,
    packageType,
    token,
  );
};

console.log(`main: ${main}`);

main();
