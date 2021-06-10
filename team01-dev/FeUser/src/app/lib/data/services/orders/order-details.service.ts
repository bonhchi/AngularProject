import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { OrderDetailModel } from "../../models/orders/order.model";


@Injectable()
export class OrderDetailsService {

  private url = '/api/order-detail';

  constructor(private httpClient: HttpClientService) { }

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model:OrderDetailModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: OrderDetailModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: OrderDetailModel) {
    const url = `${this.url}?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
  save(model: OrderDetailModel) {
    if (model.id) {
      return this.update(model);
    }
    return this.create(model);
  }
}

