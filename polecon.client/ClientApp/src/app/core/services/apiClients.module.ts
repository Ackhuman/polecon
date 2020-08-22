
import { NgModule } from '@angular/core'
import { ReportController } from 'api/report.apiClient.ts'
import { ChartController } from 'api/chart.apiClient.ts';

@NgModule({
  providers: [
    ChartController,
    ReportController
  ]
})
export class ApiClientModule { }
