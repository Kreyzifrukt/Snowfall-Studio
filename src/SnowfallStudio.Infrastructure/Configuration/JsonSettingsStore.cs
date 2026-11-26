using System.Text.Json;
using SnowfallStudio.Application.Configuration;

namespace SnowfallStudio.Infrastructure.Configuration;

/// <summary>
/// JSON settings storage under the current user's LocalApplicationData directory.
/// Writes use a temporary file followed by a replace.
/// </summary>
public sealed class JsonSettingsStore : ISettingsStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    private readonly string _settingsPath;

    public JsonSettingsStore(string? settingsPath = null)
    {
        _settingsPath = settingsPath ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SnowfallStudio",
            "settings.json");
    }

    public async Task<T?> LoadAsync<T>(CancellationToken cancellationToken = default) where T : class
    {
        if (!File.Exists(_settingsPath))
            return null;

        await using var stream = new FileStream(
            _settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, options: FileOptions.Asynchronous | FileOptions.SequentialScan);

        return await JsonSerializer.DeserializeAsync<T>(stream, SerializerOptions, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task SaveAsync<T>(T settings, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(settings);
        var directory = Path.GetDirectoryName(_settingsPath)
            ?? throw new InvalidOperationException("The settings path must include a directory.");
        Directory.CreateDirectory(directory);

        var temporaryPath = _settingsPath + ".tmp";
        try
        {
            await using (var stream = new FileStream(
                temporaryPath, FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: 4096, options: FileOptions.Asynchronous))
            {
                await JsonSerializer.SerializeAsync(stream, settings, SerializerOptions, cancellationToken)
                    .ConfigureAwait(false);
                await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            }

            File.Move(temporaryPath, _settingsPath, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporaryPath))
                File.Delete(temporaryPath);
        }
    }
}
