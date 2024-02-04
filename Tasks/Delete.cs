namespace NuGetPush.Tasks;
using MSBTask = Microsoft.Build.Utilities.Task;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Resources;

using NuGetSettings = NuGet.Configuration.Settings;
using Microsoft.Build.Framework;

using NuGet.Commands;

public class DeletePackage : NuGetTaskBase
{
    public DeletePackage(ResourceManager taskResources, string helpKeywordPrefix) : base(taskResources, helpKeywordPrefix) { }
    public DeletePackage() : base() { }

    [Required]
    public string PackageId { get; set; }

    [Required]
    public string Version { get; set; }

    public override bool Execute()
    {
        // Logger.LogWarning($"Deleting {PackageId} version {Version} from source {Source}...");
        DeleteRunner.Run(Settings, PackageSourceProvider, PackageId, Version, Source, ApiKey, true, false, (string s) => true, Logger);
        // Logger.LogWarning($"Done.");
        return true;
    }
}
