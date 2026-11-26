using SnowfallStudio.Core.Effects;

namespace SnowfallStudio.Application.Effects;

/// <summary>
/// Central list of effects exposed by the application. This catalog describes capability;
/// it does not claim that a planned effect is already executable.
/// </summary>
public sealed class WeatherEffectCatalog
{
    private readonly IReadOnlyDictionary<string, WeatherEffectDescriptor> _effects;

    public WeatherEffectCatalog(IEnumerable<WeatherEffectDescriptor> effects)
    {
        ArgumentNullException.ThrowIfNull(effects);
        var map = new Dictionary<string, WeatherEffectDescriptor>(StringComparer.OrdinalIgnoreCase);
        foreach (var effect in effects)
        {
            ArgumentNullException.ThrowIfNull(effect);
            if (string.IsNullOrWhiteSpace(effect.Id))
                throw new ArgumentException("An effect must have a non-empty identifier.", nameof(effects));
            if (!map.TryAdd(effect.Id, effect))
                throw new ArgumentException($"Duplicate weather effect identifier: '{effect.Id}'.", nameof(effects));
        }
        _effects = map;
    }

    public IReadOnlyCollection<WeatherEffectDescriptor> GetAll() => _effects.Values.ToArray();

    public bool TryGet(string id, out WeatherEffectDescriptor? descriptor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return _effects.TryGetValue(id, out descriptor);
    }

    public static WeatherEffectCatalog CreateDefault() =>
        new(
        [
            new WeatherEffectDescriptor(WeatherEffectId.Snow, "Snow",
                "Existing snowfall engine. It remains hosted by the legacy WPF pipeline until migration is verified.",
                EffectImplementationStatus.LegacyIntegrated),
            new WeatherEffectDescriptor(WeatherEffectId.Rain, "Rain",
                "Planned rain effect. Only extension-point stubs exist.",
                EffectImplementationStatus.PlannedStub),
            new WeatherEffectDescriptor(WeatherEffectId.Leaves, "Falling leaves",
                "Planned seasonal leaf effect. Only extension-point stubs exist.",
                EffectImplementationStatus.PlannedStub)
        ]);
}
