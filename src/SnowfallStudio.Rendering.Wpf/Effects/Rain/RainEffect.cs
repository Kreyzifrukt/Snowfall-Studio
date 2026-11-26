using SnowfallStudio.Core.Effects;

namespace SnowfallStudio.Rendering.Wpf.Effects.Rain;

/// <summary>
/// Placeholder for rain simulation. Intentionally produces no particles or motion yet.
/// Implement only after the shared effect lifecycle and frame timing are validated.
/// </summary>
public sealed class RainEffect : IWeatherEffect
{
    public string Id => WeatherEffectId.Rain;

    public void Initialize(double viewportWidth, double viewportHeight)
    {
        // TODO: Define rain particle state and viewport initialization.
    }

    public void Update(in EffectFrameContext context)
    {
        // TODO: Implement deterministic, delta-time-based rain simulation.
    }

    public void Reset()
    {
        // TODO: Reset rain particle state.
    }

    public void Dispose()
    {
        // TODO: Release simulation resources when resources are introduced.
    }
}
