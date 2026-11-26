namespace SnowfallStudio.Core.Effects;

/// <summary>
/// Platform-neutral lifecycle for a simulated weather effect.
/// Implementations must not depend on WPF windows, Win32 handles, or UI controls.
/// </summary>
public interface IWeatherEffect : IDisposable
{
    string Id { get; }
    void Initialize(double viewportWidth, double viewportHeight);
    void Update(in EffectFrameContext context);
    void Reset();
}
