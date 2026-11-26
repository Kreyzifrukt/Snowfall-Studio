using SnowfallStudio.Core.Configuration;

namespace SnowfallStudio.Application.Configuration;

/// <summary>Loads and saves application preferences without depending on a storage technology.</summary>
public sealed class SettingsService(ISettingsStore store)
{
    private readonly ISettingsStore _store = store ?? throw new ArgumentNullException(nameof(store));

    public async Task<StudioSettings> LoadAsync(CancellationToken cancellationToken = default)
    {
        var settings = await _store.LoadAsync<StudioSettings>(cancellationToken).ConfigureAwait(false);
        return settings is { SchemaVersion: 1 } ? settings : new StudioSettings();
    }

    public Task SaveAsync(StudioSettings settings, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return _store.SaveAsync(settings, cancellationToken);
    }
}
