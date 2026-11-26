namespace SnowfallStudio.Platform.Windows.Overlay;

/// <summary>
/// Boundary for native desktop-overlay behavior. The existing Win32 implementation
/// remains in the legacy app until DPI, monitor and click-through behavior is migrated
/// and verified on Windows.
/// </summary>
public interface IDesktopOverlayService
{
    void SetClickThrough(bool enabled);
    void SetPaused(bool paused);
}
