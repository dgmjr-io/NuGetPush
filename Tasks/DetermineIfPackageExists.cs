/*
 * DetermineIfPackageExists.cs
 *     Created: 2024-08-02T03:42:34-04:00
 *    Modified: 2024-08-02T03:42:35-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace NuGetPush.Tasks;

using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

using NuGetPush.Tasks.Extensions;

public class DetermineIfPackageExists() : NuGetPackageTaskBase
{
    [Required]
public string PackagePath { get; set; }

[Output]
public bool PackageExists { get; private set; }

public string PackageId => PackagePath.GetPackageId();

public string PackageVersion => PackagePath.GetPackageVersion();

public NuGetVersion NuGetVersion => NuGetVersion.Parse(PackageVersion);

public override bool Execute()
{
    try
    {
        return ValidateParameters();
    }
    catch (Exception ex)
    {
        Log.LogErrorFromException(ex);
        return false;
    }
}

public virtual bool DoesPackageExist()
{
    try
    {
        var psi = new ProcessStartInfo("nuget", ["search", "-s", Source, PackageId, "-PreRelease"])
        {
            RedirectStandardOutput = true
        };
        var ps = Process.Start(psi);
        ps.WaitForExit();
        return !ps.StandardOutput.EndOfStream;

        // TODO: Make this work with the NuGet API
        // // Get the package metadata resource from the repository
        // var resource = await Repository.GetResourceAsync<PackageMetadataResource>();

        // // Find all versions of the package
        // var packageMetadata = await resource.GetMetadataAsync(
        //     PackageId,
        //     true,
        //     true,
        //     new SourceCacheContext(),
        //     Logger,
        //     CancellationToken.None
        // );

        // // Check if the specific version exists
        // foreach (var metadata in packageMetadata.Select(metadata => metadata.Identity))
        // {
        //     if (
        //         metadata.Id.Equals(PackageId, OrdinalIgnoreCase)
        //         && metadata.Version == NuGetVersion
        //     )
        //     {
        //         return true;
        //     }
        // }
    }
    catch (Exception ex)
    {
        Log.LogErrorFromException(ex);
    }

    return false;
}
}
