import { NgModule } from '@angular/core';
import { Route, Routes, RouterModule, PreloadingStrategy } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { HomeComponent } from 'home/home.component';
import { ChartModule } from 'chart/chart.module';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'chart', loadChildren: () => ChartModule }
];

export class PoleconPreloadingStrategy implements PreloadingStrategy {
  preload(route: Route, load: Function): Observable<any> {
    return load();
  }
}

export const AppRoutingModule =
  RouterModule.forRoot(
    appRoutes,
    {
      preloadingStrategy: PoleconPreloadingStrategy
    }
  );
