import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ChartDataRequest } from 'models/chartDataRequest.model';

@Component({
  selector: 'chart-controls',
  templateUrl: './chart-controls.component.html',
  styleUrls: ['./chart-controls.component.css']
})
export class ChartControlsComponent {
  @Output() render = new EventEmitter<ChartDataRequest>();

  movingAveragePeriod: number = null;

  yearMin = 1450;
  yearMax = 1620;
  includeNulls: boolean = true;
  chartTypeOptions = [
    { key: 'line', label: 'Line' },
    { key: 'scatter', label: 'Scatter' },
    { key: 'histogram', label: 'Histogram' }
  ];
  chartType = this.chartTypeOptions[0];
  dataPointIds: number[] = [];

  renderClick() {
    let request = this.getChartRequestObject();
    this.render.emit(request);
  }

  setChartDefaults() {
    this.yearMin = null;
    this.yearMax = null;
    this.includeNulls = true;
    this.chartType = this.chartTypeOptions[0];
    this.movingAveragePeriod = null;
  }

  getChartRequestObject(): ChartDataRequest {
    let request: ChartDataRequest = {
      dataPointIds: this.dataPointIds,
      yearMax: this.yearMax,
      yearMin: this.yearMin,
      movingAveragePeriod: this.movingAveragePeriod,
      includeNulls: this.includeNulls
    }
    return request;
  }

  selectDataPoints(dataPointIds: number[]) {
    this.dataPointIds = dataPointIds.map(dp => dp.id);
  }
}
