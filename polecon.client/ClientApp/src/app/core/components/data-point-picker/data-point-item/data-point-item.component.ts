import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'data-point-item',
  templateUrl: './data-point-item.component.html',
  styleUrls: ['./data-point-item.component.css']
})
export class DataPointItemComponent implements OnInit {
  @Input() dataPointItem: any;
  constructor() { }

  ngOnInit() {
  }

}
