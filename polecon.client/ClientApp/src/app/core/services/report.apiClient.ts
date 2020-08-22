
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
//import { DataSourceRequestState, DataResult, toDataSourceRequestString } from '@progress/kendo-data-query';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject, iif, of } from 'rxjs';
import { map, tap, catchError, share, finalize } from 'rxjs/operators';


@Injectable()
export class ReportController {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.postHeaderOptions = new HttpHeaders();
    this.postHeaderOptions.set("Content-Type", "application/json");
  }
  private postHeaderOptions: HttpHeaders;

  
  // 
  public debasementReport(): Observable<any> {
    
    return this.http.get(`${this.baseUrl}debasementReport`);
  }
}
