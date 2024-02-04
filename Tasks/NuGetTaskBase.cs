namespace NuGetPush.Tasks;
using MSBTask = Microsoft.Build.Utilities.Task;
using Microsoft.Build.Utilities;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Resources;

using NuGetSettings = NuGet.Configuration.Settings;
using Microsoft.Build.Framework;

using NuGet.Commands;

public abstract class NuGetTaskBase : MSBTask
{
    public NuGetTaskBase(ResourceManager taskResources, string helpKeywordPrefix) : base(taskResources, helpKeywordPrefix) { }
    public NuGetTaskBase() : base() { }
    [Required]
    public string Source { get; set; }

    [Required]
    public string MSBuildProjectDirectory { get; set; }

    private NuGet.Configuration.ISettings _settings;
    protected ISettings Settings => _settings ??= NuGetSettings.LoadDefaultSettings(MSBuildProjectDirectory);

    protected PackageSourceProvider PackageSourceProvider => new(Settings);

    private NuGetMSBuildTaskLogger? _logger;
    public virtual NuGetMSBuildTaskLogger Logger { get => _logger ??= new (this); set => _logger = value; }

    private string? _apiKey;
    public virtual string ApiKey
    {
        get => _apiKey ??= SettingsUtility.GetValueForAddItem(Settings, ConfigurationConstants.ApiKeys, PackageSourceProvider.ResolveAndValidateSource(Source));
        set => _apiKey = value;
    }
}
