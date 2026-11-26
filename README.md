# Snowfall Studio

Snowfall Studio is a Windows desktop application for ambient weather and seasonal effects on the desktop. Its first behavior baseline is the existing Flake snowfall implementation, which is being carried forward and evolved rather than discarded.

## Current baseline

- Windows 10/11, x64.
- C#, WPF, and .NET 10.
- Transparent desktop overlay with click-through behavior.
- Animated snow particles, wind and weather presets.
- Accumulated snow surface and cursor interaction.
- Settings window and notification-area controls.
- Existing Win32 integration.

The legacy code was imported from `Kreyzifrukt/Snow_fall_windows` branch `main`, commit `2954ca1591915f28f251a45388df172f3b766393`. The original repository is left unchanged. See [Baseline provenance](docs/BASELINE.md).

## Architecture foundation

The solution now contains these modules:

- `SnowfallStudio.Core`: platform-neutral models and effect contracts.
- `SnowfallStudio.Application`: effect catalog and application services.
- `SnowfallStudio.Infrastructure`: JSON settings persistence implementation.
- `SnowfallStudio.Platform.Windows`: Windows integration boundaries.
- `SnowfallStudio.Rendering.Wpf`: WPF renderer contracts and future-effect placeholders.
- `Flake.App`: temporary legacy executable host, retained to protect existing behavior during migration.

This is an architecture foundation, not a completed engine migration. The current snow simulation still lives in the legacy WPF project. Rain and leaves are intentionally inert stubs: they do not simulate or render anything yet. Settings persistence is implemented as an infrastructure service but is not yet connected to application startup or the settings UI.

See [Architecture](docs/ARCHITECTURE.md) and [Implementation plan](docs/IMPLEMENTATION-PLAN.md).

## Development priorities

1. Verify the imported baseline builds and behaves correctly on Windows 10/11.
2. Add repeatable tests and manual characterization scenarios.
3. Migrate the snow engine incrementally behind the new effect contracts.
4. Wire settings persistence and application lifecycle services.
5. Harden multi-monitor, mixed-DPI, and fullscreen behavior.
6. Implement rain and leaves only after the Snow pipeline is stable.
7. Complete the Flake-to-SnowfallStudio identity cleanup.

## Build and run

Requirements: Windows 10/11 and the .NET 10 SDK.

```powershell
dotnet restore .\Flake.sln
dotnet build .\Flake.sln --configuration Release
dotnet run --project .\src\Flake.App\Flake.App.csproj
```

The solution and app folder retain their legacy Flake names for now. This is intentional until the migration is verified.

## CI

GitHub Actions builds the WPF solution on a Windows runner for pushes and pull requests.
