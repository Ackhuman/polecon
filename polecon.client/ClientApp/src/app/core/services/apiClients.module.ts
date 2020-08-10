
import { NgModule } from '@angular/core'
import { ChartController } from 'api/chart.apiClient.ts'

@NgModule({
  providers: [   
    ChartController
  ]
})
export class ApiClientModule { }
