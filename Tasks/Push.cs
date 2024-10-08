using NuGetPush.Tasks.Extensions;

namespace NuGetPush.Tasks;

public class PushPackage : NuGetPackageTaskBase
{
    public PushPackage(ResourceManager taskResources, string helpKeywordPrefix) : base(taskResources, helpKeywordPrefix) { }
    public PushPackage() : base() { }

    public string PackageId => PackagePath.GetPackageId();

    public string PackageVersion => PackagePath.GetPackageVersion();

    [Required]
    public string PackagePath { get; set; }
    public override bool Execute()
    {
        if (!ValidateParameters())
        {
            return false;
        }

        Logger.LogWarning($"Pushing {PackageId} version {PackageVersion} to source {Source}...");
        // PushRunner.Run(Settings, PackageSourceProvider, [PackagePath], Source, ApiKey, null, null, 10, false, true, false, true, Logger);
        List<string> args = ["nuget", "push", PackagePath, "-s", Source];
        if(!IsNullOrEmpty(ApiKey))
        {
            args.AddRange(["-k", ApiKey]);
        }
        var psi = new ProcessStartInfo("dotnet", args);
        Process.Start(psi).WaitForExit();
        return true;
    }

    protected override bool ValidateParameters()
    {
        if (IsNullOrEmpty(PackagePath))
        {
            Log.LogError("PackagePath is required.");
            return false;
        }

        return base.ValidateParameters();
    }
}
