import { Component, OnInit, OnDestroy, DoCheck, Input, IterableDiffers, IterableDiffer } from '@angular/core';
import { Observable, Subject, merge } from 'rxjs';
import { switchMap, tap, takeUntil } from 'rxjs/operators';
import { ApiClientService } from 'chart/api-client.service';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'chart-render',
  templateUrl: './chart-render.component.html',
  styleUrls: ['./chart-render.component.css']
})
export class ChartRenderComponent implements OnInit, DoCheck, OnDestroy {
  @Input() dataPointIds: number[] = [];

  private dataPointsChanged$: Subject<number> = new Subject<number>();
  private stop$: Subject<any> = new Subject<any>();
  private iterableDiffer: IterableDiffer<any>;
  private options: Highcharts.Options = {
    chart: {
      type: 'scatter'
    },
    series: []
  };
  
  constructor(
    private api: ApiClientService,
    iterableDiffers: IterableDiffers
  ) {
    this.iterableDiffer = iterableDiffers.find(this.dataPointIds).create();
  }


  getData$: Observable<any> = this.dataPointsChanged$
    .pipe(
      switchMap(() => this.api.getData(this.dataPointIds))
  );

  ngOnInit() {
    this.getData$
      .pipe(
        tap((results: any[]) => {
          this.options.series = results;
        }),
        tap(() => this.render()),
        takeUntil(this.stop$)
      ).subscribe();
  }

  ngOnDestroy(): void {
    this.stop$.next();
  }

  ngDoCheck() {
    if(this.iterableDiffer) {
      let changes = this.iterableDiffer.diff(this.dataPointIds);
      if(changes) {
        this.dataPointsChanged$.next();
      }
    }
  }

  render() {
    Highcharts.chart('chartContainer', this.options);
  }

}
