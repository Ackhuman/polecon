import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SwedenDebasementReportComponent } from './sweden-debasement-report/sweden-debasement-report.component';

const routes: Routes = [
  { path: 'debasement', component: SwedenDebasementReportComponent }
];

@NgModule({
  declarations: [SwedenDebasementReportComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ReportModule { }
