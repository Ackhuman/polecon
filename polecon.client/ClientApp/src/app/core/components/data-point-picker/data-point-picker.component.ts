import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ChartController } from 'api/chart.apiClient';

@Component({
  selector: 'data-point-picker',
  templateUrl: './data-point-picker.component.html',
  styleUrls: ['./data-point-picker.component.css']
})
export class DataPointPickerComponent implements OnInit {
  unselectedDataPoints: any[] = [];
  selectedDataPoints: any[] = [];
  isLoading: boolean = false;
  dataPointList = [64, 1, 4, 5, 6, 9, 10, 13, 17, 20, 21, 34, 42, 43, 51];

  @Output() dataPointSelect = new EventEmitter<any[]>();

  constructor(
    private api: ChartController
  ) { }

  ngOnInit() {
    this.isLoading = true;
    this.api.getDataPoint(null)
      .subscribe(dataPoints => {
        this.unselectedDataPoints = dataPoints;
        this.isLoading = false;
      });
  }

  selectDataPoint(dataPointId: number) {
    let index = this.unselectedDataPoints
      .map(dp => dp.id)
      .indexOf(dataPointId);
    let [dataPoint] = this.unselectedDataPoints.splice(index, 1);
    this.selectedDataPoints.push(dataPoint);
    this.dataPointSelect.emit(this.selectedDataPoints);
  }
  removeDataPoint(dataPointId: number) {
    let index = this.selectedDataPoints
      .map(dp => dp.id)
      .indexOf(dataPointId);
    let [dataPoint] = this.selectedDataPoints.splice(index, 1);
    this.unselectedDataPoints.push(dataPoint);
    this.dataPointSelect.emit(this.selectedDataPoints);
  }
  selectAll() {
    this.selectedDataPoints.push(...this.unselectedDataPoints);
    this.unselectedDataPoints = [];
    this.dataPointSelect.emit(this.selectedDataPoints);
  }
}
