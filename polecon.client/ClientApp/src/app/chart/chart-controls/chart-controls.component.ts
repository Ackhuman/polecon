import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Subject, Observable, forkJoin } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ChartClientConfig } from 'chart/chart-client-config';
import { ChartController } from 'api/chart.apiClient';

@Component({
  selector: 'app-chart-controls',
  templateUrl: './chart-controls.component.html',
  styleUrls: ['./chart-controls.component.css']
})
export class ChartControlsComponent implements OnInit {
  @Input() series: any[] = [];
  @Input() selectedSeries = 0;

  @Input() dataSeries: any[] = [];
  @Output() dataSeriesChange = new EventEmitter<any[]>();

  @Input() chartConfig: ChartClientConfig;
  @Output() chartConfigChange = new EventEmitter<ChartClientConfig>();

  render$ = new Subject<any>();
 
  constructor(
    private api: ChartController
  ) { }
  
  chartTypeOptions: any[] = [
    {key:'scatter', label: 'Scatter plot'},
    {key:'bar', label: 'Bar Chart'}
  ];

  ngOnInit() {
  }

  renderChart() {
    let singleSeries = this.series
      .filter(s => s.length === 1)
      .map(s => s[0]);
    let pairedSeries: any[][] = this.series.filter(s => s.length === 2);
    
    //forkJoin(
    //  this.api.getData(singleSeries),
    //  this.api.getPairedData(pairedSeries)
    //).subscribe(series => {
    //  this.dataSeries.push([...series]);
    //  this.dataSeriesChange.emit(this.dataSeries);
    //});
  }

  addSeries() {
    this.series.push([]);
    this.selectedSeries++;
  }

  addDataPointToSeries(dataPointId: number) {
    this.series[this.selectedSeries].push(dataPointId);
  }

  removeSeries(index: number) {
    this.series = this.series.splice(index, 1);
  }

}
