import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";

@Injectable()
export class PageContentService {
  private url = "/api/user/page-content";

  constructor(private httpClient: HttpClientService) {}

  getList(request: any = null) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  getById(request: any = null, id: string) {
    const url = this.url + `/${id}`;
    return this.httpClient.getObservable(url, request).toPromise();
  }

  getInfo(request: any = null) {
    const url = "/api/user/info-website";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  postContact(request: any = null) {
    const url = this.url + "/contact";
    return this.httpClient.postObservable(url, request).toPromise();
  }
}
