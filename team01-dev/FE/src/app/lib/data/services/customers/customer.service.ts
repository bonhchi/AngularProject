import { Injectable } from '@angular/core';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { CustomerModel } from '../../models';

@Injectable()
export class CustomerService {
  private url = '/api/customer';

  constructor(private httpClient: HttpClientService) { }

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: CustomerModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: CustomerModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: CustomerModel) {
    const url = `${this.url}/?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }

  save(model) {
    if (model.id) {
      return this.update(model);
    }
    return this.create(model);
  }
}
