namespace NuGetPush.Tasks.Extensions;

public static partial class NuGetStringExtensions
{
    public const string NupkgRegexString =
        @"^(?:.*/)?(?<PackageId>.+)\.(?<Version>(?<Major>\d+)\.(?<Minor>\d+)\.(?<Build>\d+)(?:-(?<Prerelease>[\w.-]+))?)\.nupkg$";

    [GeneratedRegex(NupkgRegexString)]
    private static partial Regex NupkgRegex();

    public static string GetPackageId(this string packagePath) =>
        NupkgRegex().IsMatch(packagePath)
            ? NupkgRegex().Match(packagePath).Groups["PackageId"].Value
            : string.Empty;

    public static string GetPackageVersion(this string packagePath) =>
        NupkgRegex().IsMatch(packagePath)
            ? NupkgRegex().Match(packagePath).Groups["Version"].Value
            : string.Empty;
}
