/* global echarts */
/**
 * Functions the PanoramicData.ECharts samples reference with JavascriptFunction.Registered(name).
 * Load after the PanoramicData.ECharts scripts and before any chart renders.
 */
function registerGraphSamples(charts) {
  charts.registerFunction(
    "samples.graph.forceLayout.afterLoad",
    function (graph) {
      graph.nodes.forEach(function (node) {
        node.symbolSize = 5;
      });
      return graph;
    },
  );
}

function registerHeatmapSamples(charts) {
  charts.registerFunction("samples.heatmap.multiYear.tooltip", function (p) {
    const format = echarts.time.format(p.data[0], "{yyyy}-{MM}-{dd}", false);
    return format + ": " + p.data[1];
  });
}

function registerMultiAxisSamples(charts) {
  charts.registerFunction("samples.multiAxis.ml", function (value) {
    return value + " ml";
  });

  charts.registerFunction("samples.multiAxis.celsius", function (value) {
    return value + " °C";
  });
}

function registerPieSamples(charts) {
  charts.registerFunction("samples.pie.halfDoughnut.label", function (param) {
    return param.name + " (" + param.percent * 2 + "%)";
  });
}

function registerScatterSamples(charts) {
  const hours = Array.from({ length: 24 }, function (_, h) {
    return (h % 12 || 12) + (h < 12 ? "a" : "p");
  });
  const days = "Saturday Friday Thursday Wednesday Tuesday Monday Sunday".split(
    " ",
  );

  charts.registerFunction(
    "samples.scatter.punchCard.tooltip",
    function (params) {
      return (
        params.value[2] +
        " commits in " +
        hours[params.value[0]] +
        " of " +
        days[params.value[1]]
      );
    },
  );

  charts.registerFunction(
    "samples.scatter.punchCard.symbolSize",
    function (val) {
      return val[2] * 2;
    },
  );

  charts.registerFunction(
    "samples.scatter.punchCard.animationDelay",
    function (idx) {
      return idx * 5;
    },
  );
}

function registerAllSamples(charts) {
  registerGraphSamples(charts);
  registerHeatmapSamples(charts);
  registerMultiAxisSamples(charts);
  registerPieSamples(charts);
  registerScatterSamples(charts);
}

registerAllSamples(window.panoramicDataECharts);
