namespace NuGetPush.Tasks;

public class NuGetMSBuildTaskLogger(MSBTask task) : NuGet.Common.ILogger
{
    public void LogDebug(string data) => LogMessage(MessageImportance.Low, data);

    public void LogVerbose(string data) => LogMessage(MessageImportance.Low, data);

    public void LogInformation(string data) => LogMessage(MessageImportance.Low, data);

    public void LogMinimal(string data) => LogMessage(MessageImportance.Low, data);

    public void LogWarning(string data) => LogMessage(MessageImportance.High, data);

    public void LogError(string data) => LogErrorMessage(message: data);

    public void LogError(
        string data,
        [CallerLineNumber] int lineNumber = -1,
        [CallerFilePath] string filename = default!,
        [CallerMemberName] string memberName = default!
    ) =>
        LogErrorMessage(
            file: filename,
            lineNumber: lineNumber,
            message: data,
            senderName: memberName
        );

    public void LogInformationSummary(string data) => LogMessage(MessageImportance.Low, data);

    public TTask LogAsync(LogLevel level, string data)
    {
        LogMessage(MessageImportance.Low, data);
        return TTask.CompletedTask;
    }

    public TTask LogAsync(ILogMessage message)
    {
        LogMessage(message.ToString());
        return TTask.CompletedTask;
    }

    public void Log(LogLevel level, string data) => LogMessage(data);

    public void Log(ILogMessage message) => LogMessage(message.ToString());

    protected MSBTask Task => task;
    protected IBuildEngine BuildEngine => Task.BuildEngine;

    protected void LogMessage(string message) => LogMessage(MessageImportance.Low, message);

    protected void LogMessage(
        MessageImportance importance,
        string message,
        params object[] messageArgs
    )
    {
        var e = new BuildMessageEventArgs(
            message,
            helpKeyword: null,
            senderName: Task.GetType().Name,
            importance,
            datetime.UtcNow,
            messageArgs
        );
        BuildEngine.LogMessageEvent(e);
    }

    protected void LogErrorMessage(
        string? subcategory = default,
        string? code = default,
        string? file = default,
        int lineNumber = -1,
        int columnNumber = -1,
        int endLineNumber = -1,
        int endColumnNumber = -1,
        string? message = default,
        string? helpKeyword = default,
        string? senderName = default,
        string? helpLink = default,
        DateTime eventTimestamp = default,
        params object[] messageArgs
    )
    {
        var e = new BuildErrorEventArgs(
            subcategory,
            code,
            file,
            lineNumber,
            columnNumber,
            endLineNumber,
            endColumnNumber,
            message,
            helpKeyword,
            senderName,
            helpLink,
            eventTimestamp,
            messageArgs
        );
        BuildEngine.LogErrorEvent(e);
    }
}
