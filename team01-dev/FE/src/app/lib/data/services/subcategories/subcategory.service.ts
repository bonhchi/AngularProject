import { Injectable } from '@angular/core';
import { SubcategoryModel } from 'src/app/lib/data/models/subcategories/subcategories.model';
import { HttpClientService } from 'src/app/lib/http/http-client';

@Injectable()
export class SubcategoryService {
  private url = '/api/subcategory';

  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: SubcategoryModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: SubcategoryModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: SubcategoryModel) {
    const url = `${this.url}?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }

  save(model: SubcategoryModel) {
    if (model.id) {
      return this.update(model);
    }
    return this.create(model);
  }
}
