import { Component, Input, OnChanges } from '@angular/core';
import { Observable, Subject, merge } from 'rxjs';
import { switchMap, tap, takeUntil } from 'rxjs/operators';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'chart-render',
  templateUrl: './chart-render.component.html',
  styleUrls: ['./chart-render.component.css']
})
export class ChartRenderComponent implements OnChanges {
  @Input() options: Highcharts.Options;
  
  constructor(
  ) {
  }
  ngOnChanges() {
    if(this.options) {
      this.render();
    }
  }

  render() {
    Highcharts.chart('chartContainer', this.options);
  }

}
