namespace SnowfallStudio.Core.Effects;

/// <summary>Display and lifecycle metadata for a weather effect.</summary>
public sealed record WeatherEffectDescriptor(
    string Id,
    string DisplayName,
    string Description,
    EffectImplementationStatus Status);
