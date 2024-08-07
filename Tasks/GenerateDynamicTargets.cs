/*
 * GenerateDynamicTargets.cs
 *     Created: 2024-07-29T09:50:48-04:00
 *    Modified: 2024-07-29T09:50:48-04:00
 *      Author: David G. Moore, Jr. <david@dgmjr.io>
 *   Copyright: © 2022 - 2024 David G. Moore, Jr., All Rights Reserved
 *     License: MIT (https://opensource.org/licenses/MIT)
 */

namespace NuGetPush.Tasks;

using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

using NuGetPush.Tasks.Extensions;

public partial class GenerateDynamicTargets() : NuGetTaskBase
{
    public string? MSBuildProjectDirectory => Path.GetDirectoryName(ProjectFile);

    private string _packageId;
    public string PackageId
    {
        get => _packageId ??= PackagePath.GetPackageId();
        set => _packageId = value;
    }

    private string _packageVersion;
    public string PackageVersion
    {
        get => _packageVersion ??= PackagePath.GetPackageId();
        set => _packageVersion = value;
    }

    [Required]
    public string PackagePath { get; set; }

    public string DynamicTargetsProjectFile { get; set; }

    protected GenerateDynamicTargets(ResourceManager taskResources)
        : this()
    {
        Log.TaskResources = taskResources;
    }

    protected GenerateDynamicTargets(ResourceManager taskResources, string helpKeywordPrefix)
        : this(taskResources)
    {
        Log.HelpKeywordPrefix = helpKeywordPrefix;
    }

    public override bool Execute()
    {
        if (!ValidateParameters())
        {
            return false;
        }

        DynamicTargetsProjectFile ??= Constants.DefaultDynamicTargetsFilePath;

        Log.LogWarning($"Generating dynamic targets file '{DynamicTargetsProjectFile}'...");

        var sources = new List<ITaskItem>();

        var packageSourceProvider = new PackageSourceProvider(Settings);
        var packageSources = packageSourceProvider
            .LoadPackageSources()
            .Where(source => source.IsEnabled);

        // Retrieve API keys
        var credentialService = new PackageSourceCredentialService(Settings);

        var dynamicTargetsSource = $"""
        <Project>
            {Join("\n", packageSources.Select(source =>
            $"""
            <PropertyGroup>
                <Push{SanitizeName(source.Name)}IsEnabled Condition="'$(Push{SanitizeName(source.Name)}IsEnabled)' == ''">false</Push{SanitizeName(source.Name)}IsEnabled>
            </PropertyGroup>

            <Target Name="Push{SanitizeName(source.Name)}" AfterTargets="Pack" Condition="'$(GeneratePackageOnBuild)' == 'true' And '$(Push{SanitizeName(source.Name)}IsEnabled)' == 'true'">
                <!--<DetermineIfPackageExists PackagePath="{PackagePath}" ProjectFile="{ProjectFile}" Source="{source.Source}">
                    <Output TaskParameter="PackageExists" PropertyName="PackageExists" />
                </DetermineIfPackageExists>-->

                <Exec Command="nuget search -s {source.Name} {PackageId} -PreRelease | grep {PackageId}" ConsoleToMsBuild="true" IgnoreExitCode="true">
                    <Output TaskParameter="ConsoleOutput" ItemName="ConsoleOutput"/>
                </Exec>

                <PropertyGroup>
                    <PackageExists>false</PackageExists> <!-- default it to false -->
                    <PackageExists Condition="'%(Lines.Identity)' != ''">true</PackageExists>
                </PropertyGroup>

                <Warning Text="Package {PackageId} version {PackageVersion} already exists in source {source.Name}." Condition="$(PackageExists)" />
                <Warning Text="Package {PackageId} version {PackageVersion} does not exist in source {source.Name}." Condition="!$(PackageExists)" />

                <DeletePackage
                    PackageId="{PackageId}"
                    PackageVersion="{PackageVersion}"
                    Source="{source.Source}"
                    ApiKey="{credentialService.GetApiKey(source)}"
                    ProjectFile="{ProjectFile}"
                    Condition="$(PackageExists)" />

                <PushPackage
                    Source="{source.Source}"
                    ApiKey="{credentialService.GetApiKey(source)}"
                    PackagePath="{PackagePath}"
                    ProjectFile="{ProjectFile}" />
            </Target>
            """
            ))}
        </Project>
        """;

        File.WriteAllText(DynamicTargetsProjectFile, dynamicTargetsSource);

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

    private const string SanitizationRegexString = @"[^A-Za-z_]";

    [GeneratedRegex(SanitizationRegexString)]
    private static partial Regex SanitizationRegex();

    public static string SanitizeName(string name) => SanitizationRegex().Replace(name, "");
}
