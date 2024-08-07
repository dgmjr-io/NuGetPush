namespace NuGet.Configuration;

public class PackageSourceCredentialService(ISettings settings)
{
    private ISettings Settings => settings;

public string? GetApiKey(PackageSource source)
{
    var apiKeys = Settings.GetSection(ConfigurationConstants.ApiKeys).Items.OfType<AddItem>();

    var apiKey = apiKeys
        .FirstOrDefault(key => key.Key.Equals(source.Source, OrdinalIgnoreCase))
        ?.Value;

    return apiKey;
}
}
