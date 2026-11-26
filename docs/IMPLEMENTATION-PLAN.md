# Snowfall Studio implementation plan

**Status:** Proposed execution plan. The architecture skeleton is being introduced separately; feature migration has not started.

## Guardrails

- Keep the original `Snow_fall_windows` repository unchanged.
- Work on `feature/studio-foundation`; do not develop directly on `main`.
- Preserve the existing snowfall behavior until an equivalent implementation is covered by Windows verification.
- Do not implement rain, leaves, or other new weather effects during the foundation phase.
- Do not combine architecture migration, UI redesign, and simulation changes in one step.
- Every migration step must build and have a clear rollback point.

## Phase 0 — Baseline inventory and reproducible build

1. Compare the complete source tree with the original `Snow_fall_windows/main` baseline.
2. Confirm all source files, resources, manifests, and project references are present.
3. Run restore and Release build on a Windows runner.
4. Record known behavior and manual verification cases for snowfall, wind, accumulation, clearing, cursor interaction, snowballs, snowmen, tray actions, and localization.
5. Fix only import/build blockers. Do not refactor the simulation in this phase.

**Exit criteria:** the imported baseline builds, launches, and its current behavior is documented.

## Phase 1 — Architecture foundation

1. Introduce `SnowfallStudio.Core` for platform-neutral identifiers, models, and effect lifecycle contracts.
2. Introduce `SnowfallStudio.Application` for orchestration, effect catalog, and application use cases.
3. Introduce `SnowfallStudio.Infrastructure` for persistence and external resources.
4. Introduce `SnowfallStudio.Platform.Windows` for monitor, cursor, native-window, and desktop-overlay adapters.
5. Introduce `SnowfallStudio.Rendering.Wpf` for WPF-specific drawing adapters.
6. Keep the existing WPF application as the compatibility host and reference the new projects without moving legacy classes yet.
7. Add explicit placeholder effect types for rain and leaves. They must remain inert and clearly marked as not implemented.

**Exit criteria:** all projects are in the solution, dependencies point inward, and the solution builds without changing legacy snowfall behavior.

## Phase 2 — Characterization tests and contracts

1. Add unit tests for settings validation, effect catalog registration, frame timing, and deterministic simulation helpers.
2. Add characterization tests or reproducible manual scenarios for the legacy snow surface and particle behavior.
3. Define immutable render snapshots so simulation code does not expose mutable collections to renderers.
4. Define a single coordinate convention for physical pixels versus WPF device-independent units.
5. Define lifecycle rules for initialize, pause, resize, reset, and dispose.

**Exit criteria:** core contracts and the riskiest legacy behavior have repeatable checks.

## Phase 3 — Extract the snowfall engine

1. Move weather settings and pure simulation models into Core/Application where they do not require WPF.
2. Move particle stepping and collision logic behind the effect lifecycle.
3. Keep WPF drawing in Rendering.Wpf.
4. Move Win32 calls behind Platform.Windows interfaces.
5. Migrate one vertical slice at a time: basic snowfall, wind, accumulation, clear animation, cursor interaction, then snowballs and snowmen.
6. Compare each slice against the legacy behavior before removing its old implementation.

**Exit criteria:** the Snow effect runs through the new effect pipeline with no material regression.

## Phase 4 — Application lifecycle and settings

1. Move tray, pause/resume, and shutdown coordination into application services.
2. Wire the JSON settings store with schema-version handling and safe defaults.
3. Add migration from legacy in-memory `WeatherSettings` to the new settings model.
4. Add reset-to-default behavior and failure handling for invalid settings files.
5. Keep persistence opt-in for transient simulation state; do not persist snow accumulation unless explicitly designed.

**Exit criteria:** preferences survive restarts and invalid configuration cannot prevent startup.

## Phase 5 — Windows platform hardening

1. Implement monitor enumeration and mixed-DPI coordinate conversion.
2. Implement desktop overlay lifecycle and click-through transitions.
3. Verify multi-monitor layouts, display changes, scaling changes, fullscreen applications, and sleep/resume.
4. Add diagnostics for native window state and monitor geometry.

**Exit criteria:** supported Windows display configurations behave predictably.

## Phase 6 — Additional effects

Only after the new Snow pipeline is stable:

1. Implement rain simulation and rendering.
2. Implement falling leaves with rotation and wind response.
3. Add effect-specific settings and sensible performance budgets.
4. Add effect switching without recreating unrelated application services.

**Exit criteria:** each effect is independently testable, can be paused/reset/disposed, and does not regress Snow.

## Phase 7 — Product polish

1. Rename remaining legacy Flake solution, folder, and namespace identities in a dedicated change.
2. Improve settings UI and accessibility.
3. Add packaging, update strategy, crash-safe logging, and performance profiling.
4. Document supported Windows versions and limitations.

## Dependency direction

`Core` has no project dependencies. `Application` depends on `Core`. `Infrastructure`, `Platform.Windows`, and `Rendering.Wpf` implement boundaries needed by the application and depend inward. The WPF app is the composition root and wires implementations together. No Core or Application project may reference WPF or Win32.
