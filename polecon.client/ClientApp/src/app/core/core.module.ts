import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OcticonDirective } from '../core/octicon.directive';
import { DataPointPickerComponent } from './components/data-point-picker/data-point-picker.component';
import { DataPointItemComponent } from './components/data-point-picker/data-point-item/data-point-item.component';



@NgModule({
  declarations: [
    OcticonDirective,
    DataPointPickerComponent,
    DataPointItemComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    OcticonDirective,
    DataPointPickerComponent,
    DataPointItemComponent
  ]
})
export class CoreModule { }
