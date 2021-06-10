import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";


@Injectable()
export class ProductDetailsService  {

    constructor(private httpClient: HttpClientService) { }

    private url = '/api/user/product-details';

    get(id: string) {
        const url = `${this.url}?id=${id}`;
      return this.httpClient.getObservable(url).toPromise();
    }
  }