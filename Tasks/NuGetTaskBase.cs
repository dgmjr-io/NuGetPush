namespace NuGetPush.Tasks;

public abstract class NuGetTaskBase() : MSBTask
{
    protected static readonly ResourceManager DefaultResourceManager =
        new("NuGetPush.Tasks", typeof(NuGetTaskBase).Assembly);

    protected NuGetTaskBase(ResourceManager taskResources)
        : this()
    {
        Log.TaskResources = taskResources;
    }

    protected NuGetTaskBase(ResourceManager taskResources, string helpKeywordPrefix)
        : this(taskResources)
    {
        Log.HelpKeywordPrefix = helpKeywordPrefix;
    }

    [Required]
    public virtual string ProjectFile { get; set; }

    private string _projectDirectory;
    public string? ProjectDirectory => _projectDirectory ??= Path.GetDirectoryName(ProjectFile);

    private ISettings _settings;
    protected ISettings Settings =>
        _settings ??= NuGetSettings.LoadDefaultSettings(ProjectDirectory);

    private PackageSourceProvider _packageSourceProvider;
    protected PackageSourceProvider PackageSourceProvider =>
        _packageSourceProvider ??= new(Settings);

    private NuGetMSBuildTaskLogger? _logger;
    public virtual NuGetMSBuildTaskLogger Logger
    {
        get => _logger ??= new(this);
        set => _logger = value;
    }

    protected virtual bool ValidateParameters()
    {
        if (IsNullOrEmpty(ProjectFile))
        {
            Log.LogError("ProjectFile is required.");
            return false;
        }

        return true;
    }
}
