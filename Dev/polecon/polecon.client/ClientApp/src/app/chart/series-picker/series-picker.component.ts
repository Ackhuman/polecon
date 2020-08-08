import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import {ApiClientService} from 'chart/api-client.service';

@Component({
  selector: 'series-picker',
  templateUrl: './series-picker.component.html',
  styleUrls: ['./series-picker.component.css']
})
export class SeriesPickerComponent implements OnInit {

  @Output() dataPointSelected: EventEmitter<any> = new EventEmitter<any>();

  getSeries$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  series$: Observable<any> = this.getSeries$.pipe(
      switchMap(() => this.api.getDataPoint())
    );
  constructor(
    private api: ApiClientService
  ) { }

  ngOnInit() {
    this.getSeries$.next(null);
  }

  selectDataPoint(dataPointId: number) {
    this.dataPointSelected.emit(dataPointId);
  }

}
