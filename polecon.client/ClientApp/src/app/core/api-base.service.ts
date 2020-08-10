import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

export class ApiBaseService {

  protected noCacheHeaders: HttpHeaders = new HttpHeaders({
    'Cache-Control': 'no-cache',
    'Pragma': 'no-cache',
    'Expires': 'Sat, 01 Jan 2000 00:00:00 GMT'
  });

  protected getJsonHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });


  constructor(
    protected http: HttpClient,
    @Inject('BASE_URL') protected baseUrl: string,
    protected router: Router) {

  }
  //todo: shouldn't this be handleError(response: HttpErrorResponse) ?
  /**
   * This provides returns a lambda expression that is used by catchError
   * to display an error message along with a high level friendly message.
   * 
   * @param highLevelErrorMessage An error message
   */
  protected handleError(highLevelErrorMessage: string, errorHandlingBehavior?: number) {
    return (err: any) => {
      console.log(highLevelErrorMessage + ': ' + err);

      let toastrMessage: string = highLevelErrorMessage;
      if (typeof (err.error) == 'object' && err.error && err.error.message) {
        toastrMessage += ': ' + err.error.message;
        
      }
      if (errorHandlingBehavior === 3) {
        this.router.navigate(['pagenotfound']);
        return of(null);
      }
      return Observable.throw(err);
    }
  }
}
