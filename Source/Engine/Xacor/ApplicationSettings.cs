using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Xacor;

[UsedImplicitly]
public class ApplicationSettings
{
    public ApplicationSettings(IConfiguration configuration)
    {
        VSync = !bool.TryParse(configuration[nameof(VSync)], out var vsync) || vsync;
        UpdatesPerSecond = int.TryParse(configuration[nameof(UpdatesPerSecond)], out var ups) ? ups : 60;
        FramesPerSecond = int.TryParse(configuration[nameof(FramesPerSecond)], out var fps) ? fps : 144;
        IsDebugModeEnabled = !bool.TryParse(configuration[nameof(IsDebugModeEnabled)], out var isDebugModeEnabled) || isDebugModeEnabled;
    }

    public bool VSync { get; }
    public bool IsDebugModeEnabled { get; }
    public int UpdatesPerSecond { get; }
    public int FramesPerSecond { get; }
}