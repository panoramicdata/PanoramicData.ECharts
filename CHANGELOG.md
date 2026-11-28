# Changelog

All notable changes to PanoramicData.ECharts will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Versions are produced by [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning)
from `version.json` plus the Git height, and a release is published by running `Publish.ps1`, which
pushes a tag that triggers the CI publish. Please add an entry here in the same change that will be
released, since nothing in the pipeline generates one.

## [Unreleased]

Version 7.0. This release removes `eval` from the package's JavaScript, which changes how JavaScript
functions and external data references are passed from .NET. The major version moves to 7.0 for these
breaking changes, so it no longer matches the bundled Apache ECharts version (still 6.1.0).

### Breaking changes

- **JavaScript functions are registered in the browser by name. No JavaScript source is sent from .NET
  or evaluated.** The chart options, map definitions and fetch commands are now strict JSON, parsed with
  `JSON.parse`. `eval` is gone from `panoramicdata-echarts.js`, `panoramicdata-echarts-min.js`,
  `panoramicdata-echarts-interop.js` and the PanoramicData part of both bundles, so this package no longer
  needs `'unsafe-eval'` in a Content Security Policy. (The Apache ECharts library inside the bundles still
  contains one `new Function` fallback, which it uses only when `JSON.parse` does not exist.)
- **`JavascriptFunction`:** the public constructor `JavascriptFunction(string function)`, the explicit
  conversion from `string` and the `Function` property are removed, because running source text needs
  `eval`. Use `JavascriptFunction.Registered("name")` and the `Name` property. Every option property typed
  `JavascriptFunction`, `ColorOrFunction`, `NumberOrFunction`, `NumberArrayOrFunction`,
  `NumberOrStringOrFunction`, `ObjectOrFunction`, `StringOrFunction` or `TooltipPosition` keeps its type.
- **`JavascriptFunctionConverter`** writes the marker object `{"$pdFn":"<name>"}` instead of the raw
  function source.
- **`ExternalDataSourceRefConverter`** writes `{"$pdDs":"<fetchId>","path":"<path>"}` instead of the
  JavaScript expression `window.panoramicDataECharts.getDataSource('<fetchId>').<path>`.
  `ExternalDataSourceRef.Path` is now a data path, not JavaScript: dot-separated own properties, with `[n]`
  array indexes and a leading `.` accepted. Method calls or other expressions in a path no longer work, and
  `__proto__`, `constructor` and `prototype` are never followed. `ExternalDataSource.Path`, which was already
  a dotted path, now accepts `[n]` and a leading `.` in the same way.
- **A reference to an external data source that has not been loaded** leaves the value out and logs an error
  to the browser console, where the old JavaScript expression threw a `TypeError`.
- **`ExternalDataSource.AfterLoad`** is sent as the registered function's name.
- **`ColorConverter`** writes gradients as ECharts gradient objects (`{"type":"linear","x":0,...}`) instead
  of `new echarts.graphic.LinearGradient(...)`. Charts render the same: missing colour stops are written as
  `[]` and a missing `global` as `false`, as the gradient classes did. Gradient coordinates no longer depend
  on the server's culture (a decimal comma used to produce invalid JavaScript), and a coordinate that is
  `NaN` or infinite now throws when the options are serialised, because JSON cannot carry it.
- **Custom converters** passed in `JsonConverters` must write valid JSON. A converter that used
  `WriteRawValue` to emit JavaScript will now fail to parse in the browser.
- **Reserved object shapes:** an object in the options whose only key is `$pdFn`, or whose keys are `$pdDs`
  and optionally `path`, is read as a marker. Do not use those keys in chart data.
- **Values that were JavaScript expressions,** such as `Data = new JavascriptFunction("(function () { ... })()")`,
  must be computed in .NET instead.
- **Functions are looked up when ECharts calls them.** Calling a name that has not been registered throws an
  error naming it, which appears in the browser console. Load registration scripts before `blazor.*.js` so
  they are in place before any chart renders.

### Added

- `panoramicDataECharts.registerFunction(name, fn)` in the browser, and `JavascriptFunction.Registered(name)`
  in .NET to reference it. Registrations survive the PanoramicData.ECharts script being loaded again.
- The samples register their functions in `PanoramicData.ECharts.Samples/wwwroot/js/panoramicdata-echarts-samples.js`,
  which the demo loads.

### Migration from 6.x

1. Move each function body out of C# into a static script loaded after the PanoramicData.ECharts scripts
   and before `blazor.*.js`:
   `panoramicDataECharts.registerFunction("myFormatter", function (p) { ... });`
2. Replace `new JavascriptFunction("function (p) { ... }")` (or `(JavascriptFunction)"..."`) with
   `JavascriptFunction.Registered("myFormatter")`. Do the same for `ExternalDataSource.AfterLoad`.
3. If you serialised options yourself and relied on `JavascriptFunctionConverter` or
   `ExternalDataSourceRefConverter` producing JavaScript, expect the marker objects above instead.
4. If you wrote your own converter that emits JavaScript with `WriteRawValue`, make it emit JSON. To pass a
   function, serialise a `JavascriptFunction`: `JsonSerializer.Serialize(writer, JavascriptFunction.Registered("name"), options)`.
5. Check any `ExternalDataSourceRef` or `ExternalDataSource` path that was more than property names and `[n]` indexes.
6. If your Content Security Policy allowed `'unsafe-eval'` only for this package, you can remove it.

### Fixed

- **External data paths only follow the data's own properties.** The path walk used the target object's
  own `hasOwnProperty` and could be pointed at prototype keys; it now uses
  `Object.prototype.hasOwnProperty.call` and refuses `__proto__`, `constructor` and `prototype`.
- **Options changed in place during chart initialisation are drawn again.** 6.1.35 stopped a re-render
  during initialisation from starting a second `initChart`, but only redrew afterwards when `Options`
  was a different instance. Pages that change the same instance in place while the chart initialises
  (for example setting theme text colours in `OnAfterRenderAsync`) had relied on that second
  `initChart` and lost those changes. After initialisation the chart now compares the serialised
  options with what it drew, and redraws once if they differ.

## [6.1.35] - 2026-11-03

### Fixed

- **Options set during chart initialisation are no longer lost.** `EChart` ignored `Options` changes
  until its first `initChart` call completed, so a page that rebuilt its options right after the first
  render (for example after reading the theme in `OnAfterRenderAsync`) kept the stale chart until some
  later parameter change arrived. A re-render during initialisation also started a second `initChart`.
  Initialisation now runs once, records the options it drew, and redraws once with `updateChart` if
  `Options` changed while it was in flight. Covered by a new bUnit test.

## [6.1.4] - 2026-08-18

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
