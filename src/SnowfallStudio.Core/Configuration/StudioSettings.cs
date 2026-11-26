namespace SnowfallStudio.Core.Configuration;

/// <summary>
/// Application-level preferences. Effect-specific settings should live in their own
/// versioned settings models instead of expanding this type indefinitely.
/// </summary>
public sealed record StudioSettings
{
    public int SchemaVersion { get; init; } = 1;
    public string Language { get; init; } = "en";
    public string SelectedEffectId { get; init; } = "snow";
}
