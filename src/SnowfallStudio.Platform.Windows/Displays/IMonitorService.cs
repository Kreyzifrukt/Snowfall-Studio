namespace SnowfallStudio.Platform.Windows.Displays;

/// <summary>Provides monitor bounds and scaling information in a consistent coordinate model.</summary>
public interface IMonitorService
{
    IReadOnlyList<MonitorDescriptor> GetMonitors();
}
