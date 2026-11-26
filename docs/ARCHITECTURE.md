# Snowfall Studio architecture

**Architecture status:** foundation skeleton added; legacy snowfall engine has not yet been migrated into the new layers.

## Solution layout

```text
src/
  SnowfallStudio.Core/
    Configuration/
    Effects/
  SnowfallStudio.Application/
    Configuration/
    Effects/
  SnowfallStudio.Infrastructure/
    Configuration/
  SnowfallStudio.Platform.Windows/
    Displays/
    Overlay/
  SnowfallStudio.Rendering.Wpf/
    Effects/
      Rain/
      Leaves/
  Flake.App/                 # legacy WPF composition host, retained temporarily
```

## Projects and responsibilities

### SnowfallStudio.Core

Platform-neutral contracts and models:
- stable effect identifiers and descriptors;
- effect lifecycle contract (`IWeatherEffect`);
- frame timing and viewport context;
- application preference model.

Core must not reference WPF, WinForms, Win32, or another project in this solution.

### SnowfallStudio.Application

Application orchestration and use cases:
- `WeatherEffectCatalog` describes which effects are known and their implementation status;
- `ISettingsStore` defines the persistence boundary;
- `SettingsService` owns application-level loading and default selection.

The catalog is metadata, not an effect runtime. The current Snow effect is still hosted by the legacy WPF pipeline.

### SnowfallStudio.Infrastructure

External-resource implementations:
- `JsonSettingsStore` stores settings under the current user's LocalApplicationData directory;
- writes use a temporary file before replacing the target file.

The settings service and JSON store are scaffolding at this stage. They are not yet wired into application startup, shutdown, or the settings window.

### SnowfallStudio.Platform.Windows

Windows-specific integration boundaries:
- monitor enumeration and normalized display metadata;
- desktop overlay click-through and pause control.

Only interfaces and data contracts exist initially. The legacy Win32 implementation remains in `Flake.App/Interop` until a behavior-preserving migration is verified.

### SnowfallStudio.Rendering.Wpf

WPF-specific drawing adapters:
- `IWeatherEffectRenderer` separates drawing from simulation;
- Rain and Leaves effect/renderer classes are explicit inert placeholders;
- placeholders intentionally draw nothing and perform no simulation.

The existing `SnowField`, `SnowSurface`, and related snow classes remain in the legacy application for now. They are not falsely represented as migrated.

### Legacy WPF host

`Flake.App` currently remains the executable composition root and owns the old `App`, tray icon, settings window, overlay window, localization, and rendering engine. It references the new projects to keep them in the solution and make the future migration incremental. Namespace and folder renaming are deliberately deferred.

## Dependency direction

```text
                 SnowfallStudio.App (legacy host)
                  /       |       |       \
                 v        v       v        v
          Application  Infrastructure  Platform.Windows  Rendering.Wpf
                 |          |              |                  |
                 v          v              v                  v
                         Core <-------------+------------------+
```

More precisely:
- Application -> Core
- Infrastructure -> Application + Core
- Platform.Windows -> Core
- Rendering.Wpf -> Core
- App -> all modules as the composition root

Dependencies must point inward. Core and Application must never reference WPF or Win32.

## Effect lifecycle

The intended effect pipeline is:

1. The application selects an effect by its stable identifier.
2. An effect implementation is created and initialized for the active viewport.
3. The simulation receives elapsed and delta time through `EffectFrameContext`.
4. The renderer draws the current simulation snapshot through a WPF `DrawingContext`.
5. Pause, resize, reset, and dispose are handled through explicit lifecycle operations.

A future implementation should pass immutable render snapshots from simulation to renderer. Renderers must not mutate simulation state.

## Effect implementation status

| Effect | Current status | Notes |
|---|---|---|
| Snow | LegacyIntegrated | Existing implementation remains in `Flake.App/Rendering`; not migrated |
| Rain | PlannedStub | Lifecycle and renderer placeholders only; no particles or drawing |
| Leaves | PlannedStub | Lifecycle and renderer placeholders only; no particles or drawing |

The status catalog must be updated only when the corresponding implementation and verification are complete.

## Current legacy implementation

The existing `SnowField` still combines particle updates, snow accumulation, cursor interaction, snowball/snowman simulation, WPF drawing, and the animation callback. The old `App` still coordinates the tray icon, settings window, and overlay. This coupling is a known migration target, not a reason to rewrite everything in one change.

See [Implementation plan](IMPLEMENTATION-PLAN.md) for the staged migration and exit criteria.
