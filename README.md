# Snowfall Studio

Snowfall Studio is a Windows desktop application for ambient weather and seasonal effects on the desktop. The first development baseline is the existing Flake snowfall implementation, which is being carried forward and evolved rather than rewritten from scratch.

## Current baseline

- Windows 10/11, x64.
- C#, WPF, and .NET 10.
- Transparent desktop overlay with click-through behavior.
- Animated snow particles, wind and weather presets.
- Accumulated snow surface and cursor interaction.
- Settings window and notification-area controls.
- Existing Win32 interop for overlay hit testing.

The code is currently imported from `Kreyzifrukt/Snow_fall_windows` branch `main`, commit `2954ca1591915f28f251a45388df172f3b766393`. The original repository is left unchanged. See [Baseline provenance](docs/BASELINE.md).

## Development priorities

1. Verify the imported baseline builds and behaves correctly on Windows 10/11.
2. Rename the internal Flake identity to Snowfall Studio without destabilizing the rendering engine.
3. Separate the simulation, desktop overlay, settings, and platform integration into maintainable modules.
4. Add persistent settings and reliable startup/shutdown behavior.
5. Expand the effect system beyond snowfall, starting only after the core engine is stable.
6. Improve multi-monitor, mixed-DPI, performance, and accessibility behavior.

The project will grow in tested increments. Experimental behavior from the old codebase is not considered production-ready merely because it exists in source.

## Build and run

Requirements: Windows 10/11 and the .NET 10 SDK.

```powershell
dotnet restore .\Flake.sln
dotnet build .\Flake.sln -c Release
dotnet run --project .\src\Flake.App\Flake.App.csproj
```

The solution and assembly still use their legacy `Flake` names in this initial import. They will be renamed as part of the controlled identity cleanup.

## CI

GitHub Actions builds the WPF solution on a Windows runner for pushes and pull requests.
