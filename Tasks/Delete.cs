using System;

namespace NuGetPush.Tasks;

public class DeletePackage(ResourceManager taskResources, string? helpKeywordPrefix = default) : NuGetPackageTaskBase(taskResources, helpKeywordPrefix)
{
    public DeletePackage() : this(new ResourceManager("NuGetPush.Tasks", typeof(DeletePackage).Assembly)) { }

    [Required]
    public string PackageId { get; set; }

    [Required]
    public string PackageVersion { get; set; }

    public override bool Execute()
{
    if (!ValidateParameters())
    {
        return false;
    }

    Logger.LogWarning($"Deleting {PackageId} version {PackageVersion} from source {Source}...");
    var psi = new ProcessStartInfo("dotnet", ["nuget", "delete", PackageId, PackageVersion, "-s", Source, "-k", ApiKey]);
    var p = Process.Start(psi);
    p.WaitForExit();
    // DeleteRunner.Run(
    //     Settings,
    //     PackageSourceProvider,
    //     PackageId,
    //     PackageVersion,
    //     Source,
    //     ApiKey,
    //     true,
    //     false,
    //     _ => true,
    //     Logger
    // );
    Logger.LogWarning("Done.");
    return true;
}

protected override bool ValidateParameters()
{
    if (IsNullOrEmpty(PackageId))
    {
        Log.LogError("PackageId is required.");
        return false;
    }

    if (IsNullOrEmpty(PackageVersion))
    {
        Log.LogError("PackageVersion is required.");
        return false;
    }

    return base.ValidateParameters();
}
}
