import { Injectable } from '@angular/core';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { PageContentModel } from '../../models/pageContent/pageContent.model';

@Injectable()
export class PageContentService {
  private url = '/api/page-content';

  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  update(model: PageContentModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  create(model: PageContentModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  delete(id: string)
  {
    const url = `${this.url}/?id=${id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }

  saveChange(model: PageContentModel) {
    if (model.id) return this.update(model);
    return this.create(model);
  }
}
