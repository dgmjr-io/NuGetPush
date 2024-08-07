/*
 * GenerateNuGetSources.cs
 *     Created: 2024-07-28T20:17:58-04:00
 *    Modified: 2024-07-28T20:17:58-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Resources;

namespace NuGetPush.Tasks;

public class GetNuGetSources() : NuGetTaskBase
{
    public string? MSBuildProjectDirectory => Path.GetDirectoryName(ProjectFile);

public GetNuGetSources(ResourceManager taskResources)
    : this()
{
    Log.TaskResources = taskResources;
}

public GetNuGetSources(ResourceManager taskResources, string helpKeywordPrefix)
    : this(taskResources)
{
    Log.HelpKeywordPrefix = helpKeywordPrefix;
}

protected PackageSourceCredentialService _credentialService;
protected PackageSourceCredentialService PackageSourceCredentialService =>
    _credentialService ??= new PackageSourceCredentialService(Settings);
private IEnumerable<PackageSource> _packageSources;
protected IEnumerable<PackageSource> PackageSources =>
    _packageSources ??= PackageSourceProvider.LoadPackageSources(Settings);

[Required]
public override string ProjectFile
{
    get => base.ProjectFile;
    set => base.ProjectFile = value;
}

[Output]
public ITaskItem[] NuGetSources { get; set; }

public override bool Execute()
{
    Log.LogWarning("Config file paths: " + Join(", ", Settings.GetConfigFilePaths()));

    NuGetSources = [..PackageSources
        .Select(source =>
        {
            var item = new TaskItem(source.Source);
            item.SetMetadata(Constants.MetadataNames.Name, source.Name);

            // Add API key if it exists
            var apiKey = PackageSourceCredentialService.GetApiKey(source);
            if (apiKey is not null)
            {
                item.SetMetadata(Constants.MetadataNames.ApiKey, apiKey);
            }
            return item;
        })];

    Log.LogWarning("Package sources: " + Join(", ", PackageSources.Select(src => src.Name)));

    // Log.LogWarning(
    //     $"""
    //     MSBuildProjectDirectory: {MSBuildProjectDirectory}
    //     BuildEngine.ProjectFileOfTaskNode: {BuildEngine.ProjectFileOfTaskNode}
    //     """
    // );

    // var sources = new List<ITaskItem>();

    // var packageSources = PackageSourceProvider.LoadPackageSources(Settings);

    // // Retrieve API keys
    // var credentialService = new PackageSourceCredentialService(Settings);

    // foreach (var source in packageSources)
    // {
    //     var item = new TaskItem(source.Source);
    //     item.SetMetadata("Include", source.Source);
    //     item.SetMetadata("Name", source.Name);

    //     // Add API key if it exists
    //     var apiKey = credentialService.GetApiKey(source);
    //     if (apiKey is not null)
    //     {
    //         item.SetMetadata("ApiKey", apiKey);
    //     }

    //     sources.Add(item);
    // }

    // Sources = [..sources];

    return true;
}
}
