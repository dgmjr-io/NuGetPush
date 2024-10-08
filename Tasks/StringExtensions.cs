namespace NuGetPush.Tasks.Extensions;

using static Constants;

public static partial class NuGetStringExtensions
{
    [GeneratedRegex(NupkgRegexString)]
    private static partial Regx NupkgRegex();

    public static string GetPackageId(this string packagePath) =>
        NupkgRegex().IsMatch(packagePath)
            ? NupkgRegex().Match(packagePath).Groups["PackageId"].Value
            : string.Empty;

    public static string GetPackageVersion(this string packagePath) =>
        NupkgRegex().IsMatch(packagePath)
            ? NupkgRegex().Match(packagePath).Groups["Version"].Value
            : string.Empty;
}
