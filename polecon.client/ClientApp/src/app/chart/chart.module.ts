import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HighchartsChartModule } from 'highcharts-angular';
import { Routes, RouterModule } from '@angular/router';
import { ChartRenderComponent } from './chart-render/chart-render.component';
import { SeriesPickerComponent } from './series-picker/series-picker.component';
import { ChartControlsComponent } from './chart-controls/chart-controls.component';
import { ChartPageComponent } from './chart-page/chart-page.component';
import { ApiClientModule } from 'api/apiClients.module';
import { SpecificChartComponent } from './specific-chart/specific-chart.component';

const moduleRoutes: Routes = [
  { path: '', component: ChartPageComponent },
  { path: 'sweden', component: SpecificChartComponent }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ApiClientModule,
    HighchartsChartModule,
    RouterModule.forChild(moduleRoutes)
  ],
  declarations: [
    ChartRenderComponent,
    SeriesPickerComponent,
    ChartControlsComponent,
    ChartPageComponent,
    SpecificChartComponent
  ],
  providers: [
  ]
})
export class ChartModule { }
