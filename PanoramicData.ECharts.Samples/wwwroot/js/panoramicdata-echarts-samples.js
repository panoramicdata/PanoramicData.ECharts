/* global echarts */
/**
 * Functions the PanoramicData.ECharts samples reference with JavascriptFunction.Registered(name).
 * Load after the PanoramicData.ECharts scripts and before any chart renders.
 */
(function (charts) {
  const hours = [
    "12a",
    "1a",
    "2a",
    "3a",
    "4a",
    "5a",
    "6a",
    "7a",
    "8a",
    "9a",
    "10a",
    "11a",
    "12p",
    "1p",
    "2p",
    "3p",
    "4p",
    "5p",
    "6p",
    "7p",
    "8p",
    "9p",
    "10p",
    "11p",
  ];
  const days = [
    "Saturday",
    "Friday",
    "Thursday",
    "Wednesday",
    "Tuesday",
    "Monday",
    "Sunday",
  ];

  charts.registerFunction(
    "samples.graph.forceLayout.afterLoad",
    function (graph) {
      graph.nodes.forEach(function (node) {
        node.symbolSize = 5;
      });
      return graph;
    },
  );

  charts.registerFunction("samples.heatmap.multiYear.tooltip", function (p) {
    const format = echarts.time.format(p.data[0], "{yyyy}-{MM}-{dd}", false);
    return format + ": " + p.data[1];
  });

  charts.registerFunction("samples.multiAxis.ml", function (value) {
    return value + " ml";
  });

  charts.registerFunction("samples.multiAxis.celsius", function (value) {
    return value + " °C";
  });

  charts.registerFunction("samples.pie.halfDoughnut.label", function (param) {
    return param.name + " (" + param.percent * 2 + "%)";
  });

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
})(window.panoramicDataECharts);
