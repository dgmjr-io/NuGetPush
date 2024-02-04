namespace NuGetPush.Tasks;
using MSBTask = Microsoft.Build.Utilities.Task;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Resources;

using NuGetSettings = NuGet.Configuration.Settings;
using Microsoft.Build.Framework;

using NuGet.Commands;

public class DeleteAndPush : NuGetTaskBase
{
    [Required]
    public string PackageId { get; set; }
    [Required]
    public string PackagePath { get; set; }

    [Required]
    public string Version { get; set; }

    public override bool Execute()
    {
        new DeletePackage(TaskResources, HelpKeywordPrefix) { Source = Source, ApiKey = this.ApiKey, PackageId = PackageId, Version = Version, MSBuildProjectDirectory = MSBuildProjectDirectory }.Execute();
        new Push(TaskResources, HelpKeywordPrefix) { Source = Source, ApiKey = ApiKey, PackagePath = PackagePath, MSBuildProjectDirectory = MSBuildProjectDirectory }.Execute();
        return true;
    }
}
