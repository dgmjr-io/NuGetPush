namespace NuGetPush.Tasks;
using MSBTask = Microsoft.Build.Utilities.Task;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Resources;

using NuGetSettings = NuGet.Configuration.Settings;
using Microsoft.Build.Framework;

using NuGet.Commands;

public class GetNuGetApiKey : NuGetTaskBase
{
    [Output]
    public override string ApiKey => base.ApiKey;

    public override bool Execute()
    {
        return true;
    }
}
