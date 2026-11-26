namespace SnowfallStudio.Application.Configuration;

/// <summary>Persistence boundary for versioned application settings.</summary>
public interface ISettingsStore
{
    Task<T?> LoadAsync<T>(CancellationToken cancellationToken = default) where T : class;
    Task SaveAsync<T>(T settings, CancellationToken cancellationToken = default) where T : class;
}
