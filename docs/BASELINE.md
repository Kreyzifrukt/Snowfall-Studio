# Baseline provenance

## Source

- Repository: [Kreyzifrukt/Snow_fall_windows](https://github.com/Kreyzifrukt/Snow_fall_windows)
- Branch: `main`
- Source commit: `2954ca1591915f28f251a45388df172f3b766393`
- Imported into: [Kreyzifrukt/Snowfall-Studio](https://github.com/Kreyzifrukt/Snowfall-Studio)
- Import date: 2026-11-26

## Import decision

Snowfall Studio starts from the abandoned `main` branch as explicitly requested. The separate `feature/snowballs-and-snowmen` branch was not substituted for the selected baseline. The legacy repository and its branches were not modified.

The initial import copied the source files and project documentation into the new repository. Selected file blob SHAs were compared between the source and destination after import; the README, solution, SnowField, and SnowSurface blobs matched exactly at that point.

## Important caveats

- The original project identity remains `Flake` in namespaces, assembly name, solution name, and some UI strings. This is intentional for the first import and should be cleaned up in a dedicated change.
- The old documentation is not fully synchronized with every implementation detail. In particular, some files and experimental behavior are present in source without being part of the documented stable feature set.
- Importing source does not prove that it builds or runs. A Windows build and manual GUI verification are still required.
- The new repository has a fresh commit history. The original repository's commit graph and tags were not rewritten or copied.

## Next verification

1. Build the solution in Release on Windows with the .NET 10 SDK.
2. Check startup, tray menu, pause/resume, settings, click-through behavior, and shutdown.
3. Verify snow rendering and accumulation over a sustained run.
4. Test display changes and mixed-DPI/multi-monitor configurations.
5. Triage experimental window-edge snow rendering separately before enabling it by default.
