import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { SaveCustomerWishListModel } from "../../models/customerWishList/customerWishList.model";

@Injectable()
export class CustomerWishListService {
  private url = "/api/customer-wish-list";

  constructor(private httpClient: HttpClientService) {}

  getByCustomer(request: any = null) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  createOrDelete(model: SaveCustomerWishListModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }
}
