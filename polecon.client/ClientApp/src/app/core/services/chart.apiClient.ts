
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
//import { DataSourceRequestState, DataResult, toDataSourceRequestString } from '@progress/kendo-data-query';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject, iif, of } from 'rxjs';
import { map, tap, catchError, share, finalize } from 'rxjs/operators';

import { ChartDataRequest } from '../../core/models/chartDataRequest.model';
@Injectable()
export class ChartController {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.postHeaderOptions = new HttpHeaders();
    this.postHeaderOptions.set("Content-Type", "application/json");
  }
  private postHeaderOptions: HttpHeaders;

  
  // 
  getSeries = (id?: number): Observable<any> => {
    return this.http.get(`${this.baseUrl}api/Chart/getSeries?id=${id}`);
  }
  // 
  getDataPoint = (id?: number): Observable<any> => {
    return this.http.get(`${this.baseUrl}api/Chart/getDataPoint?id=${id}`);
  }
  // 
  getData = (request: ChartDataRequest): Observable<any> => {
    return this.http.post(`${this.baseUrl}api/Chart/getData`, request );
  }
  // 
  getLineSeries = (request: ChartDataRequest): Observable<any> => {
    return this.http.post(`${this.baseUrl}api/Chart/getLineSeries`, request );
  }
  // 
  getDateCategories = (request: ChartDataRequest): Observable<any> => {
    return this.http.post(`${this.baseUrl}api/Chart/getDateCategories`, request );
  }
}
