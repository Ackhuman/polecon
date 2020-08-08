import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'chart-page',
  templateUrl: './chart-page.component.html',
  styleUrls: ['./chart-page.component.css']
})
export class ChartPageComponent implements OnInit {
  dataPointIds: number[] = [];

  constructor() { }
  ngOnInit() {
  }
  addDataPoint(dataPointId) {
    this.dataPointIds.push(dataPointId);
  }
}
