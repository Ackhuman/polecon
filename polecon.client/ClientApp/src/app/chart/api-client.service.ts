import { Injectable, Inject } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class ApiClientService {

  constructor(
    private $http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) {}

  getSeries(id?: number) {
    let queryObj = id ? { id: id } : null;
    return this.$http.get(this.apiMethod('GetSeries', queryObj));
  }
  getDataPoint(id?: number) {
    let queryObj = id ? { id: id } : null;
    return this.$http.get(this.apiMethod('GetDataPoint', queryObj));
  }

  getData(dataPointIds: number[]) {
    return this.$http.get(this.apiMethod('GetData', { dataPointIds: dataPointIds }));
  }

  private apiBase: string = 'api/Chart';
  private apiMethod(name: string, args?: any) {
    let path = this.baseUrl + [this.apiBase, name].join('/');
    let urlParts = [path];
    if (args) {
      urlParts.push(new URLSearchParams(args).toString());
    }
    return urlParts.join('?');
  }


}
