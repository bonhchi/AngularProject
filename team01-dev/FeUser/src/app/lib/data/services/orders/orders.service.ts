import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { HttpClientService } from "src/app/lib/http/http-client";
import { OrderModel } from "../../models/orders/order.model";


@Injectable()
export class OrdersService {

  private url = '/api/order';

  constructor(private httpClient: HttpClientService) { }

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }
  getDetails(request: any) {
    return this.httpClient.getObservable(this.url+'-detail', request).toPromise();
  }

  create(model:OrderModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: OrderModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: OrderModel) {
    const url = `${this.url}?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
}

