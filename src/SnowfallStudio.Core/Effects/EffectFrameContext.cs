namespace SnowfallStudio.Core.Effects;

/// <summary>Frame timing and viewport dimensions supplied to an effect simulation.</summary>
public readonly record struct EffectFrameContext(
    TimeSpan ElapsedTime,
    TimeSpan DeltaTime,
    double ViewportWidth,
    double ViewportHeight);
