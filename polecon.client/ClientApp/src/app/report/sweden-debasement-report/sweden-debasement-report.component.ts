import { Component, OnInit } from '@angular/core';
import { ReportController } from 'api/report.apiClient';

@Component({
  selector: 'app-sweden-debasement-report',
  templateUrl: './sweden-debasement-report.component.html',
  styleUrls: ['./sweden-debasement-report.component.css']
})
export class SwedenDebasementReportComponent implements OnInit {

  constructor(
    private api: ReportController
  ) { }

  ngOnInit() {
    
  }

}
