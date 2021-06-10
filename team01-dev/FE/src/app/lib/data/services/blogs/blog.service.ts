import { Injectable } from '@angular/core';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { BlogModel } from '../../models/blogs/blog.model';

@Injectable()
export class BlogService {
  private url = '/api/blog';

  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: BlogModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: BlogModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: BlogModel) {
    const url = `${this.url}/?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }

  save(model: BlogModel) {
    if (model.id) {
      return this.update(model);
    }
    return this.create(model);
  }
}
