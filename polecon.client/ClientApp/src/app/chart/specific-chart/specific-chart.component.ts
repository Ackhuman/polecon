import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { ChartController } from 'api/chart.apiClient';
import { ChartSeries } from 'models/chartSeries.model';
import * as Highcharts from 'highcharts';
import HC_Histogram from 'highcharts/modules/histogram-bellcurve';
HC_Histogram(Highcharts);
import { ChartDataRequest } from "models/chartDataRequest.model";

@Component({
  selector: 'specific-chart',
  templateUrl: './specific-chart.component.html',
  styleUrls: ['./specific-chart.component.css']
})
export class SpecificChartComponent implements OnInit {

  xDataPointIds = [64];
  yDataPointIds = [1, 4, 5, 6, 9, 10, 13, 17, 20, 21, 34, 42, 43, 51];
  movingAveragePeriod: number = null;

  yearMin = 1450;
  yearMax = 1620;
  includeNulls: boolean = true;
  chartTypeOptions = [
    { key: 'line', label: 'Line', fn: this.getLineChart },
    { key: 'scatter', label: 'Scatter', fn: null },
    { key: 'histogram', label: 'Histogram', fn: this.getHistogram }
  ];
  chartType = this.chartTypeOptions[0];
  dataPointList: string = "64, 1, 4, 5, 6, 9, 10, 13, 17, 20, 21, 34, 42, 43, 51";

  Highcharts: typeof Highcharts = Highcharts;
  options: Highcharts.Options = {
    title: {
      text: 'Goods priced in marks vs debasement of the mark'
    },
    chart: {
      //height: 600
    },
    plotOptions: {
      histogram: {
        binsNumber: 10
      }
    }
  };

  constructor(
    private api: ChartController
  ) {
  }

  ngOnInit() {
    this.refreshChart();
  }

  setChartDefaults() {
    this.yearMin = null;
    this.yearMax = null;
    this.includeNulls = true;
    this.chartType = this.chartTypeOptions[0];
    this.movingAveragePeriod = null;
    this.dataPointList = "64, 4, 5";
  }

  getLineChart() {
    let dataPoints = this.xDataPointIds
      .concat(this.yDataPointIds);
    let request: ChartDataRequest = Object.assign(
      {
        dataPointIds: dataPoints,
        yearMin: this.yearMin,
        yearMax: this.yearMax
      },
      this.movingAveragePeriod > 0 ? { movingAveragePeriod: this.movingAveragePeriod } : null
    );
    let dataQueries = forkJoin(
      this.api.getLineSeries(request),
      this.api.getDateCategories(request)
    );
    dataQueries.subscribe((results: any[]) => {
      let options = this.getLineChartOptions(results);
      this.renderChart(null, options);
    });
  }

  getChartRequestObject(): ChartDataRequest {
    let dataPointIds = this.dataPointList.split(',')
      .map(dp => parseInt(dp.trim()));
    let request: ChartDataRequest = {
      dataPointIds: dataPointIds,
      yearMax: this.yearMax,
      yearMin: this.yearMin,
      movingAveragePeriod: this.movingAveragePeriod,
      includeNulls: this.includeNulls
    }
    return request;
  }

  getLineChartOptions(results: any[]): Highcharts.Options {
    let data = results[0];
    let categories = results[1];
    let options = Object.assign({}, this.options);
    options.series = data.map(d => Object.assign(d, { marker: { radius: 2 } }));
    options.xAxis = {
      categories: categories
    };
    options.yAxis = [{
      type: 'logarithmic'
    }];
    return options;
  }

  refreshChart() {
    this.getLineChart();
    //this.getHistogram(64);
    //this.getHistogram(4, 'supplementalChartContainer');
  }

  getHistogram(dataPointId, chartSelector?: string) {
    let dataPointIds = [dataPointId];
    let request: ChartDataRequest = {
      dataPointIds: dataPointIds,
      includeNulls: false
    };
    this.api.getLineSeries(request)
      .subscribe((result: any) => {
        let options = this.createHistogramOptions(result);
        this.renderChart(chartSelector, options);
      });
  }

  createHistogramOptions(result): Highcharts.Options {
    let options = Object.assign({}, this.options);
    options.yAxis = [
      {
        title: { text: 'Data' },
        alignTicks: false
      },
      {
        title: { text: 'Histogram' },
        alignTicks: false,
        opposite: true
      }
    ];
    options.xAxis = [
      {
        title: { text: 'Data' }
      },
      {
        title: { text: 'Histogram' },
        opposite: true
      }
    ];
    let series = result[0];
    options.series = [
      {
        type: 'histogram',
        name: `${series.name}, Frequency`,
        baseSeries: 's1',
        xAxis: 1,
        yAxis: 1
      },
      Object.assign(series, { id: 's1', type: 'scatter' })
    ];
    return options;
  }

  renderChart(selector?: string, options?: Highcharts.Options) {
    Highcharts.chart(selector || "chartContainer", options || this.options);
  }

  zipData(xSeries, ySeries) {
    let series = ySeries.map((y, i) => ({
      name: `Correlation of ${xSeries.name} and ${y.name}`,
      data: xSeries[0].data.map((x, i) => [x, y.data[i]])
    }));
    return series;
  }
}
