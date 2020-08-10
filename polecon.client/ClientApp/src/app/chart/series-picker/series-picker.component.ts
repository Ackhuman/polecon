import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ChartController } from 'api/chart.apiClient';

@Component({
  selector: 'series-picker',
  templateUrl: './series-picker.component.html',
  styleUrls: ['./series-picker.component.css']
})
export class SeriesPickerComponent implements OnInit {
  unselectedDataPoints: any[];
  selectedDataPoints: any[];

  dataPointSelect = new EventEmitter<any[]>();

  getSeries$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  series$: Observable<any> = this.getSeries$.pipe(
      switchMap(() => this.api.getDataPoint())
    );
  constructor(
    private api: ChartController
  ) { }

  ngOnInit() {
    this.getSeries$.next(null);
  }

  selectDataPoint(dataPointId: number) {
    let index = this.unselectedDataPoints.indexOf(dataPointId);
    this.unselectedDataPoints.splice(index, 1);
    this.selectedDataPoints.push(dataPointId);
    this.dataPointSelect.emit(this.selectedDataPoints);
  }
  removeDataPoint(dataPointId: number) {
    let index = this.selectedDataPoints.indexOf(dataPointId);
    this.selectedDataPoints.splice(index, 1);
    this.unselectedDataPoints.push(dataPointId);
    this.dataPointSelect.emit(this.selectedDataPoints);
  }
  selectAll() {
    this.selectedDataPoints.push(...this.unselectedDataPoints);
    this.unselectedDataPoints = [];
    this.dataPointSelect.emit(this.selectedDataPoints);
  }
}
