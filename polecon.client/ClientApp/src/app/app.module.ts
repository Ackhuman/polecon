import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router'
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { } from 'bootstrap'
import { AppRoutingModule, PoleconPreloadingStrategy } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ChartModule } from 'chart/chart.module';
import { CoreModule } from './core/core.module';

@NgModule({
  imports: [
    FormsModule,
    RouterModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CoreModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent
  ],
  providers: [
    PoleconPreloadingStrategy
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
