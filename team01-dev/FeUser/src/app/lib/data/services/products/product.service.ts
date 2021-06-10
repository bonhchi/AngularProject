import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { ProductModel } from "../../models/products/product.model";

@Injectable()
export class ProductService {
  public Currency = { name: "Vietnamese", currency: "VND", price: 1 }; // Default Currency
  private url = "/api/user/productlist";
  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  getByCategory(id: any, request: any) {
    return this.httpClient
      .getObservable(this.url + "/category?id=" + id, request)
      .toPromise();
  }

  create(model: ProductModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: ProductModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: ProductModel) {
    const url = `${this.url}/?Id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
}
