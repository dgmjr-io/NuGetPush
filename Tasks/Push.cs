namespace NuGetPush.Tasks;
using MSBTask = Microsoft.Build.Utilities.Task;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Resources;

using NuGetSettings = NuGet.Configuration.Settings;
using Microsoft.Build.Framework;

using NuGet.Commands;

public class Push : NuGetTaskBase
{
    public Push(ResourceManager taskResources, string helpKeywordPrefix) : base(taskResources, helpKeywordPrefix) { }
    public Push() : base() { }

    [Required]
    public string PackagePath { get; set; }
    public override bool Execute()
    {
        PushRunner.Run(Settings, PackageSourceProvider, [PackagePath], Source, ApiKey, null, null, 10, false, true, false, true, Logger);
        return true;
    }
}
