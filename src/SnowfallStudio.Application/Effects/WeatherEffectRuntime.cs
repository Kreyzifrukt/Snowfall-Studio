using SnowfallStudio.Core.Effects;

namespace SnowfallStudio.Application.Effects;

/// <summary>
/// Owns the active simulation and enforces its initialize/update/reset/dispose lifecycle.
/// Call this type from one coordinated thread, normally the application's render loop.
/// </summary>
public sealed class WeatherEffectRuntime : IDisposable
{
    private readonly IReadOnlyDictionary<string, IWeatherEffectFactory> _factories;
    private IWeatherEffect? _activeEffect;
    private bool _disposed;

    public WeatherEffectRuntime(IEnumerable<IWeatherEffectFactory> factories)
    {
        ArgumentNullException.ThrowIfNull(factories);

        var map = new Dictionary<string, IWeatherEffectFactory>(StringComparer.OrdinalIgnoreCase);
        foreach (var factory in factories)
        {
            ArgumentNullException.ThrowIfNull(factory);
            if (string.IsNullOrWhiteSpace(factory.EffectId))
                throw new ArgumentException("An effect factory must have a non-empty identifier.", nameof(factories));
            if (!map.TryAdd(factory.EffectId, factory))
                throw new ArgumentException($"Duplicate effect factory: '{factory.EffectId}'.", nameof(factories));
        }

        _factories = map;
    }

    public string? ActiveEffectId => _activeEffect?.Id;

    public void Activate(string effectId, double viewportWidth, double viewportHeight)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentException.ThrowIfNullOrWhiteSpace(effectId);

        if (!_factories.TryGetValue(effectId, out var factory))
            throw new InvalidOperationException($"No executable factory is registered for effect '{effectId}'.");

        var next = factory.Create()
            ?? throw new InvalidOperationException($"Factory '{effectId}' returned no effect.");

        if (!string.Equals(next.Id, effectId, StringComparison.OrdinalIgnoreCase))
        {
            next.Dispose();
            throw new InvalidOperationException(
                $"Factory '{effectId}' created an effect with mismatched identifier '{next.Id}'.");
        }

        try
        {
            next.Initialize(viewportWidth, viewportHeight);
        }
        catch
        {
            next.Dispose();
            throw;
        }

        var previous = _activeEffect;
        _activeEffect = next;
        previous?.Dispose();
    }

    public void Update(in EffectFrameContext context)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _activeEffect?.Update(context);
    }

    public void Reset()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _activeEffect?.Reset();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _activeEffect?.Dispose();
        _activeEffect = null;
    }
}
