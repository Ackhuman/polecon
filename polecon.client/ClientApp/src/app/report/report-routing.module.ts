import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SwedenDebasementReportComponent } from 'report/sweden-debasement-report/sweden-debasement-report.component';

const routes: Routes = [
  { path: 'debasement', component: SwedenDebasementReportComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
