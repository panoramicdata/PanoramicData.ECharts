/* global panoramicDataECharts, echarts */
/**
 * PanoramicData.ECharts - Blazor wrapper for Apache ECharts
 * JavaScript interop layer
 */
window.panoramicDataECharts = {
  charts: new Map(),
  dataSources: new Map(),
  // Kept if this script is loaded again, so registrations made earlier survive.
  functions:
    (window.panoramicDataECharts && window.panoramicDataECharts.functions) ||
    new Map(),
  logging: false,
  changeLogging: function (b) {
    panoramicDataECharts.logging = b;
  },
  getChart: function (id) {
    return panoramicDataECharts.charts.get(id);
  },
  getDataSource: function (fetchId) {
    var data = panoramicDataECharts.dataSources.get(fetchId);

    if (panoramicDataECharts.logging) {
      console.log(`GET CACHED FETCH ${fetchId}`);
      console.log(data);
    }

    return data;
  },
  registerFunction: function (name, fn) {
    if (typeof name !== "string" || name.length === 0) {
      throw new TypeError("registerFunction: name must be a non-empty string");
    }
    if (typeof fn !== "function") {
      throw new TypeError(`registerFunction: '${name}' is not a function`);
    }
    panoramicDataECharts.functions.set(name, fn);
  },
  getFunction: function (name) {
    const fn = panoramicDataECharts.functions.get(name);
    if (fn === undefined) {
      const message = `PanoramicData.ECharts: no function is registered as '${name}'. Call panoramicDataECharts.registerFunction('${name}', fn) before the chart renders.`;
      console.error(message);
      throw new Error(message);
    }
    return fn;
  },
  resolveDataSourceRef: function (fetchId, path) {
    if (!panoramicDataECharts.dataSources.has(fetchId)) {
      console.error(
        `PanoramicData.ECharts: no external data source has been loaded with id '${fetchId}'.`,
      );
      return undefined;
    }
    const data = panoramicDataECharts.getDataSource(fetchId);
    return path == null ? data : panoramicDataECharts.evaluatePath(data, path);
  },
  // Resolved when ECharts calls it, so a function registered after the chart rendered still works.
  functionReference: function (name) {
    return function (...args) {
      return panoramicDataECharts.getFunction(name).apply(this, args);
    };
  },
  isMarker: function (value, markerKey, allowedKeys) {
    if (!Object.prototype.hasOwnProperty.call(value, markerKey)) return false;
    return Object.keys(value).every((k) => allowedKeys.includes(k));
  },
  // JSON.parse reviver: {"$pdFn": name} becomes the registered function, {"$pdDs": id, "path": p} the fetched data.
  reviveMarker: function (key, value) {
    if (value === null || typeof value !== "object" || Array.isArray(value)) {
      return value;
    }
    if (panoramicDataECharts.isMarker(value, "$pdFn", ["$pdFn"])) {
      return panoramicDataECharts.functionReference(value.$pdFn);
    }
    if (panoramicDataECharts.isMarker(value, "$pdDs", ["$pdDs", "path"])) {
      return panoramicDataECharts.resolveDataSourceRef(value.$pdDs, value.path);
    }
    return value;
  },
  parseOptions: function (text) {
    // The reviver is only needed when a marker can be present; it is several times slower on large data.
    return text.indexOf('"$pd') === -1
      ? JSON.parse(text)
      : JSON.parse(text, panoramicDataECharts.reviveMarker);
  },
  evaluatePath: function (data, pathExpression) {
    // Accept a leading '.' and [n] indexes, as paths written for the old raw JavaScript did.
    const normalized = pathExpression
      .replace(/\[(\d+)\]/g, ".$1")
      .replace(/^\.+/, "");
    if (normalized === "") return data;
    const pathSegments = normalized.split(".");
    const blockedKeys = ["__proto__", "constructor", "prototype"];

    // Own properties only, never prototype keys; returns undefined when not found.
    return pathSegments.reduce((obj, segment) => {
      if (
        obj === undefined ||
        blockedKeys.includes(segment) ||
        !Object.prototype.hasOwnProperty.call(obj, segment)
      ) {
        return undefined;
      }
      return obj[segment];
    }, data);
  },
  fetchExternalData: async function (chart, fetchOptions) {
    if (fetchOptions == null) return;

    chart.__dataSources = [];

    for (const item of JSON.parse(fetchOptions)) {
      if (panoramicDataECharts.logging) {
        console.log(`FETCH ${item.id}`);
        console.log(item);
      }

      const response = await fetch(item.url, item.options);
      if (!response.ok) {
        throw new Error("Failed to fetch external chart data: url=" + item.url);
      }

      // parse the response as JSON
      var data = null;
      if (item.fetchAs == "json") {
        data = await response.json();

        // replace the object with the fetched data
        if (item.path != null) {
          try {
            data = panoramicDataECharts.evaluatePath(data, item.path);
          } catch (error) {
            console.log(
              "Failed to evaluate path expression of external data source",
            );
            console.log(error);
          }
        }
      } else if (item.fetchAs == "string") {
        data = await response.text();
      }

      if (panoramicDataECharts.logging) {
        console.log(data);
      }

      // execute the afterLoad function if required
      if (item.afterLoad != null) {
        try {
          data = panoramicDataECharts.getFunction(item.afterLoad)(data);
        } catch (error) {
          console.log(
            "Failed to execute afterLoad function of external data source",
          );
          console.log(error);
        }
      }

      // store in the datasources map for later retrieval
      window.panoramicDataECharts.dataSources.set(item.id, data);

      // add reference inside the chart, so we can cleanup later
      chart.__dataSources.push(item.id);
    }
  },
  registerMaps: function (chart, mapOptions) {
    if (mapOptions == null) return;

    var parsedOptions = panoramicDataECharts.parseOptions(mapOptions);
    for (const item of parsedOptions) {
      if (panoramicDataECharts.logging) {
        console.log("MAP");
        console.log(item);
      }

      if (item.type === "geoJSON") {
        echarts.registerMap(item.mapName, {
          geoJSON: item.geoJSON,
          specialAreas: item.specialAreas,
        });
      } else if (item.type === "svg") {
        echarts.registerMap(item.mapName, { svg: item.svg });
      }
    }
  },
  initChart: async function (
    id,
    theme,
    initOptions,
    chartOptions,
    mapOptions,
    fetchOptions,
  ) {
    var chart = echarts.init(
      document.getElementById(id),
      theme,
      JSON.parse(initOptions),
    );
    panoramicDataECharts.charts.set(id, chart);

    // show loading animation
    chart.showLoading();

    if (chartOptions == null) return;

    // fetch external data if needed
    await panoramicDataECharts.fetchExternalData(chart, fetchOptions);

    // register GEO maps
    await panoramicDataECharts.registerMaps(chart, mapOptions);

    // parse the options, resolving function and data source markers
    var parsedOptions = panoramicDataECharts.parseOptions(chartOptions);

    if (panoramicDataECharts.logging) {
      console.log("CHART");
      console.log(parsedOptions);
    }

    // set the chart options
    chart.setOption(parsedOptions);

    // schedule resize after current call stack so the browser has time to compute layout
    setTimeout(() => {
      var c = panoramicDataECharts.charts.get(id);
      if (c) c.resize();
    }, 0);

    // hide the loading animation immediately
    chart.hideLoading();
  },
  updateChart: async function (id, chartOptions, mapOptions, fetchOptions) {
    var chart = panoramicDataECharts.charts.get(id);
    if (chart == null) {
      console.error("Failed to retrieve chart " + id);
      return;
    }

    // fetch external data if needed
    await panoramicDataECharts.fetchExternalData(chart, fetchOptions);

    // register GEO maps
    await panoramicDataECharts.registerMaps(chart, mapOptions);

    // parse the options, resolving function and data source markers
    var parsedOptions = panoramicDataECharts.parseOptions(chartOptions);

    // resize before setting options so container dimensions are correct when animations play
    chart.resize();

    // set the chart options
    chart.setOption(parsedOptions);

    // hide the loading animation
    chart.hideLoading();
  },
  clearChart: function (id) {
    var chart = panoramicDataECharts.charts.get(id);
    if (chart == null) {
      console.error("Failed to clear chart " + id);
      return;
    }

    chart.clear();
  },
  disposeChart: function (id) {
    var chart = panoramicDataECharts.charts.get(id);
    if (chart == null) {
      console.error("Failed to dispose chart " + id);
      return;
    }

    // dispose of all dataSources linked to the chart
    if (chart.__dataSources && Array.isArray(chart.__dataSources)) {
      chart.__dataSources.forEach((id) => {
        window.panoramicDataECharts.dataSources.delete(id);
      });
    }

    echarts.dispose(chart);
    panoramicDataECharts.charts.delete(id);
  },
};
