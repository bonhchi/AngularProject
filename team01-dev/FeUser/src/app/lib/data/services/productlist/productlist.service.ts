import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { ProductModel } from "../../models/products/product.model";

@Injectable()
export class ProductListService {
  private url = "/api/user/productlist";
  private baseProduct = this.url + "/product";
  private baseCategory = this.url + "/category";
  private byCategory = this.url + "/by-category?id=";

  constructor(private httpClient: HttpClientService) {}

  getPageProduct(request: any) {
    return this.httpClient.getObservable(this.baseProduct, request).toPromise();
  }

  getCategory() {
    return this.httpClient.getObservable(this.baseCategory, null).toPromise();
  }

  getByCategory(id: any, request: any) {
    return this.httpClient
      .getObservable(this.byCategory + id, request)
      .toPromise();
  }
}
