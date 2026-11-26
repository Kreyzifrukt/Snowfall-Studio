namespace SnowfallStudio.Platform.Windows.Displays;

/// <summary>Normalized information about one physical display.</summary>
public sealed record MonitorDescriptor(
    string DeviceName,
    double X,
    double Y,
    double Width,
    double Height,
    double ScaleX,
    double ScaleY,
    bool IsPrimary);
