
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
  public dataSets(): Observable<any> {
    
    return this.http.get(`${this.baseUrl}api/Chart/dataSets`);
  }
  // 
  public getSeries(id?: (number|'')): Observable<any> {
    if(!id) {
      id = '';
    }
    return this.http.get(`${this.baseUrl}api/Chart/getSeries?id=${id}`);
  }
  // 
  public getDataPoint(dataSetId?: (number|'')): Observable<any> {
    if(!dataSetId) {
      dataSetId = '';
    }
    return this.http.get(`${this.baseUrl}api/Chart/getDataPoint?dataSetId=${dataSetId}`);
  }
  // 
  public getData(request: (ChartDataRequest|'')): Observable<any> {
    if(!request) {
      request = '';
    }
    return this.http.post(`${this.baseUrl}api/Chart/getData`, request );
  }
  // 
  public getLineSeries(request: (ChartDataRequest|'')): Observable<any> {
    if(!request) {
      request = '';
    }
    return this.http.post(`${this.baseUrl}api/Chart/getLineSeries`, request );
  }
  // 
  public getDateCategories(request: (ChartDataRequest|'')): Observable<any> {
    if(!request) {
      request = '';
    }
    return this.http.post(`${this.baseUrl}api/Chart/getDateCategories`, request );
  }
}
