# Changelog

All notable changes to PanoramicData.ECharts will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Versions are produced by [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning)
from `version.json` plus the Git height, and a release is published by running `Publish.ps1`, which
pushes a tag that triggers the CI publish. Please add an entry here in the same change that will be
released, since nothing in the pipeline generates one.

## [Unreleased]

### Fixed

- **Gauge sample no longer terminates the demo host.** `TempGaugeChart` passed an async lambda to
  `Timer`. Because `TimerCallback` returns `void`, that compiled as `async void`, so once the Blazor
  circuit was disposed the next tick called JS interop on a dead circuit and the resulting
  `TaskCanceledException` was rethrown on a ThreadPool thread with nothing observing it, terminating
  the process. It is now a `PeriodicTimer` loop with a `CancellationTokenSource`, so there is no
  `async void`, cancellation and disconnection are handled in one place, and `DisposeAsync` waits for
  a tick that is already running. The chart still updates every two seconds while the page is open.

### Changed

- Package copyright no longer carries a hardcoded year. It now inherits
  `Copyright © Panoramic Data Limited` from `Directory.Build.props`, so it cannot go stale again.
- `PackageReleaseNotes` now points at this changelog, so the published package links to release notes
  instead of carrying none.

## [6.1.1] - 2026-08-15

### Changed

- Version scheme now tracks the bundled Apache ECharts minor version, moving `version.json` from
  `6.0` to `6.1` so the package version reflects the ECharts version it bundles.

## [6.0.27] - 2026-08-15

### Security

- **Upgraded bundled Apache ECharts from 6.0.0 to 6.1.0**, addressing CVE-2026-45249.

### Changed

- GitHub Actions upgraded for Node.js 24 compatibility: `checkout` v4 to v6, `setup-dotnet` v4 to v5,
  `upload-artifact` v4 to v7 and `download-artifact` v4 to v8, since the Node.js 20 based actions
  were deprecated.
- `Publish.ps1` now resolves the version through the project's MSBuild targets rather than the global
  `nbgv` CLI, so publishing no longer depends on that tool being installed or on the PATH.
- Applied packaging and versioning governance remediations VER-03, PKG-05 and PKG-06.

## [6.0.21] - 2026-06-08

### Fixed

- Intermittent blank chart on initialisation, and update animations restored.

## [6.0.20] - 2026-06-08

### Fixed

- Removed `chart.resize()` from the update path, which had been suppressing series animations.

## [6.0.19] - 2026-06-08

### Fixed

- Chart failing to render on first load, and SVG dimension errors.

## [6.0.18] - 2026-06-08

### Fixed

- Null SVG dimensions on initialisation, and the chart now updates automatically when `Options`
  changes.

## [6.0.17] - 2026-04-16

### Changed

- Removed the test step from CI.

## [6.0.16] - 2026-04-16

Earliest tagged release in this repository. Entries below describe the 6.0 line as a whole rather
than individual patch releases, which were not tagged.

## [6.0.0] - 2024-12-27

### Added

- **Symbol packages (.snupkg)**: debugging symbols are now published.
- **Source Link support**: step through library source during debugging, via
  Microsoft.SourceLink.GitHub.
- **Nerdbank.GitVersioning**: consistent version management.
- **.NET 10 support**: updated to target .NET 10.0.

### Changed

- **Apache ECharts upgraded** from 5.4.3 to 6.0.0, improving rendering performance and memory
  management.
- **JavaScript files renamed** to the `panoramicdata-echarts-*` convention:
  - `vizor-echarts.js` becomes `panoramicdata-echarts.js`
  - `vizor-echarts-min.js` becomes `panoramicdata-echarts-min.js`
  - `vizor-echarts-bundle.js` becomes `panoramicdata-echarts-bundle.js`
  - `vizor-echarts-bundle-min.js` becomes `panoramicdata-echarts-bundle-min.js`
- **JavaScript global object** changed from `window.vizorECharts` to `window.panoramicDataECharts`.

### Fixed

- **External data sources**: corrected the reference to the global object in
  `ExternalDataSourceRef.cs`.
- **Debug builds**: `GeneratePackageOnBuild` now only runs in the Release configuration.

### Breaking changes

**JavaScript file names changed.** Update the script tags in your `_Host.cshtml`, `_Layout.cshtml`
or `App.razor`:

```html
<!-- OLD (v5.x) -->
<script src="_content/PanoramicData.ECharts/js/vizor-echarts-bundle-min.js"></script>

<!-- NEW (v6.x) -->
<script src="_content/PanoramicData.ECharts/js/panoramicdata-echarts-bundle-min.js"></script>
```

**Custom JavaScript interop.** If you have custom JavaScript using the global object:

```javascript
// OLD (v5.x)
window.vizorECharts.getDataSource(...)

// NEW (v6.0+)
window.panoramicDataECharts.getDataSource(...)
```

### Migration notes

- **C# API**: no changes required, all existing C# code remains fully compatible.
- **Chart options**: no changes, all option structures unchanged.
- **Component properties**: no changes, all `EChart` component properties work as before.
- **Data loading**: all `DataLoader`, `ExternalDataSource` and `Dataset` features unchanged.
- **JavaScript functions**: all `JavascriptFunction` usage unchanged.

### Technical details

- **Package size**: bundle increased by roughly 82 KB (8%) due to ECharts 6.0 enhancements.
- **Tests**: all 51 tests passing (47 chart samples and 4 functional tests).
- **Browser compatibility**: Chrome, Edge, Firefox and Safari, latest versions.

### Credits

- The Apache ECharts team for the 6.0.0 release.
- Original Vizor.ECharts by DataHint BV.
- Maintained by Panoramic Data Limited.

---

## [5.x.x] - Previous releases

See [GitHub Releases](https://github.com/panoramicdata/PanoramicData.ECharts/releases) for earlier
versions.

---

[6.1.1]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.27...6.1.1
[6.0.27]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.21...6.0.27
[6.0.21]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.20...6.0.21
[6.0.20]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.19...6.0.20
[6.0.19]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.18...6.0.19
[6.0.18]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.17...6.0.18
[6.0.17]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/6.0.16...6.0.17
[6.0.0]: https://github.com/panoramicdata/PanoramicData.ECharts/compare/v5.x.x...v6.0.0
