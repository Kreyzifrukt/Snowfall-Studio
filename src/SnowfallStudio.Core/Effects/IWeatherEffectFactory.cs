namespace SnowfallStudio.Core.Effects;

/// <summary>Creates an independent simulation instance for one effect.</summary>
public interface IWeatherEffectFactory
{
    string EffectId { get; }
    IWeatherEffect Create();
}
