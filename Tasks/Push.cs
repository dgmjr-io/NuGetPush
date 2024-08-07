namespace NuGetPush.Tasks;

public class PushPackage : NuGetPackageTaskBase
{
    public PushPackage(ResourceManager taskResources, string helpKeywordPrefix) : base(taskResources, helpKeywordPrefix) { }
    public PushPackage() : base() { }

    [Required]
    public string PackagePath { get; set; }
    public override bool Execute()
    {
        if (!ValidateParameters())
        {
            return false;
        }

        PushRunner.Run(Settings, PackageSourceProvider, [PackagePath], Source, ApiKey, null, null, 10, false, true, false, true, Logger);
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
