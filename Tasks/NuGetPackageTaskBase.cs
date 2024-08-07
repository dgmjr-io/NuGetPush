using System;

using NuGet.Protocol.Core.Types;

namespace NuGetPush.Tasks;

public abstract class NuGetPackageTaskBase() : NuGetTaskBase
{
    protected NuGetPackageTaskBase(ResourceManager taskResources)
        : this()
    {
        Log.TaskResources = taskResources;
    }

    protected NuGetPackageTaskBase(ResourceManager taskResources, string helpKeywordPrefix)
        : this(taskResources)
    {
        Log.HelpKeywordPrefix = helpKeywordPrefix;
    }

    private PackageSource _packageSource;
    public virtual PackageSource PackageSource =>
        _packageSource ??= PackageSourceProvider.GetPackageSourceByName(Source);

    private SourceRepository _repository;

    public virtual SourceRepository Repository =>
        _repository ??= NuGet.Protocol.Core.Types.Repository.Factory.GetCoreV3(SourceUrl);

    public virtual string SourceUrl => PackageSource.Source;

    [Required]
    public string Source { get; set; }

    public string? MSBuildProjectDirectory =>
        Path.GetDirectoryName(BuildEngine.ProjectFileOfTaskNode);

    private string? _apiKey;
    public virtual string ApiKey
    {
        get =>
            _apiKey ??= SettingsUtility.GetValueForAddItem(
                Settings,
                ConfigurationConstants.ApiKeys,
                PackageSourceProvider.ResolveAndValidateSource(Source)
            );
        set => _apiKey = value;
    }

    public override bool Execute()
    {
        return ValidateParameters();
    }

    protected override bool ValidateParameters()
    {
        if (IsNullOrEmpty(Source))
        {
            Log.LogError("Source is required.");
            return false;
        }

        return base.ValidateParameters();
    }
}
