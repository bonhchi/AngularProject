import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { InformationWebModel } from "../../models";



@Injectable()
export class InformationWebsiteService {

  private url = '/api/info-website';

  constructor(private httpClient: HttpClientService) { }

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  update(model: InformationWebModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

}
 