import { Injectable } from '@angular/core';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { ContactModel } from '../../models/contact/contact.model';

@Injectable()
export class ContactService {
  private url = '/api/contact';

  constructor(private httpClient: HttpClientService) {}

  getList(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: ContactModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: ContactModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: ContactModel) {
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
