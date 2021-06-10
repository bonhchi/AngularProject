import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";

@Injectable()
export class SearchService {
  private url = "/api/user/productlist/product";
  private urlRelevant = "/api/user/productlist";
  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  findByName(request: any, name: string) {
    const urlSearch = `${this.urlRelevant}/relevant?loading=true&name=` + name;
    return this.httpClient.getObservable(urlSearch, request).toPromise();
  }
}
