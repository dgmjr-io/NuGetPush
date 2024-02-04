namespace NuGetPush.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MSBTask = Microsoft.Build.Utilities.Task;

using NuGet.Common;

using TTask = System.Threading.Tasks.Task;

public class NuGetMSBuildTaskLogger(MSBTask task) : NuGet.Common.ILogger
{
    public void LogDebug(string data) => LogMessage(MessageImportance.Low, data);

    public void LogVerbose(string data) => LogMessage(MessageImportance.Low, data);

    public void LogInformation(string data) => LogMessage(MessageImportance.Low, data);

    public void LogMinimal(string data) => LogMessage(MessageImportance.Low, data);

    public void LogWarning(string data) => LogMessage(MessageImportance.High, data);

    public void LogError(string data) => LogMessage(MessageImportance.High, data);

    public void LogInformationSummary(string data) => LogMessage(MessageImportance.Low, data);

    public void Log(LogLevel level, string data) => LogMessage(data);

    public TTask LogAsync(LogLevel level, string data)
    {
        LogMessage(MessageImportance.Low, data);
        return TTask.CompletedTask;
    }

    public void Log(ILogMessage message) => LogMessage(message.ToString());

    public TTask LogAsync(ILogMessage message)
    {
        LogMessage(message.ToString());
        return TTask.CompletedTask;
    }

    protected MSBTask Task => task;
    protected IBuildEngine BuildEngine=> Task.BuildEngine;

    protected void LogMessage(string message) => LogMessage(MessageImportance.Low, message);

    protected void LogMessage(MessageImportance importance, string message, params object[] messageArgs)
    {
        var e = new BuildMessageEventArgs(
                    message,
                    helpKeyword: null,
                    senderName: Task.GetType().Name,
                    importance,
                    DateTime.UtcNow,
                    messageArgs);
        BuildEngine.LogMessageEvent(e);
    }
}
