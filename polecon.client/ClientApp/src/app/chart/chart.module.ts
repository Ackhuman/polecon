import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ChartRenderComponent } from './chart-render/chart-render.component';
import { SeriesPickerComponent } from './series-picker/series-picker.component';
import { ChartControlsComponent } from './chart-controls/chart-controls.component';
import { ChartPageComponent } from './chart-page/chart-page.component';
import { ApiClientService } from './api-client.service';

const moduleRoutes: Routes = [
  { path: '', component: ChartPageComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(moduleRoutes)
  ],
  declarations: [ChartRenderComponent, SeriesPickerComponent, ChartControlsComponent, ChartPageComponent],
  providers: [ApiClientService]
})
export class ChartModule { }
