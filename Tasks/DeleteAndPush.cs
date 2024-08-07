namespace NuGetPush.Tasks;

public class DeleteAndPush : NuGetPackageTaskBase
{
    [Required]
    public string PackageId { get; set; }

    [Required]
    public string PackagePath { get; set; }

    [Required]
    public string PackageVersion { get; set; }

    public override bool Execute()
    {
        if (!ValidateParameters())
        {
            return false;
        }

        var determineIfPackageExists = new DetermineIfPackageExists
        {
            PackagePath = PackagePath,
            ProjectFile = ProjectFile
        };
        determineIfPackageExists.Execute();
        if (determineIfPackageExists.PackageExists)
        {
            new DeletePackage(TaskResources, HelpKeywordPrefix)
            {
                Source = Source,
                ApiKey = ApiKey,
                PackageId = PackageId,
                PackageVersion = PackageVersion
            }.Execute();
        }
        new PushPackage(TaskResources, HelpKeywordPrefix)
        {
            Source = Source,
            ApiKey = ApiKey,
            PackagePath = PackagePath
        }.Execute();
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

        if (IsNullOrEmpty(PackagePath))
        {
            Log.LogError("PackagePath is required.");
            return false;
        }

        return base.ValidateParameters();
    }
}
