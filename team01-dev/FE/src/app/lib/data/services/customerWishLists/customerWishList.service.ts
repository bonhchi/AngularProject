import { Injectable } from '@angular/core';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { CustomerWishListModel } from '../../models/customerWishList/customerWishList.model';

@Injectable()
export class CustomerWishListService {
  private url = '/api/customer-wish-list';

  constructor(private httpClient: HttpClientService) {}

  getAll(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: CustomerWishListModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  delete(model: CustomerWishListModel) {
    const url = `${this.url}/?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
}
