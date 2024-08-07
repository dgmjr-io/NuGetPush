using System;

namespace NuGetPush.Tasks;

public class DeletePackage : NuGetPackageTaskBase
{
    public DeletePackage(ResourceManager taskResources, string helpKeywordPrefix)
        : base(taskResources, helpKeywordPrefix) { }

    public DeletePackage()
        : base() { }

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
        DeleteRunner.Run(
            Settings,
            PackageSourceProvider,
            PackageId,
            PackageVersion,
            Source,
            ApiKey,
            true,
            false,
            _ => true,
            Logger
        );
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
