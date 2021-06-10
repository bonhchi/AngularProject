import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { CategoryModel } from "../../models/categories/category.model";

@Injectable()
export class CategoryService  {

    private url = '/api/category';

    constructor(private httpClient: HttpClientService) { }

    get(request: any) {
      return this.httpClient.getObservable(this.url, request).toPromise();
    }

    create(model: CategoryModel) {
      return this.httpClient.postObservable(this.url, model).toPromise();
    }

    update(model: CategoryModel) {
      return this.httpClient.putObservable(this.url, model).toPromise();
    }

    delete(model: CategoryModel) {
      const url = `${this.url}/?Id=${model?.id}`;
      return this.httpClient.deleteObservable(url).toPromise();
    }
    save(model: CategoryModel) {
      if (model.id) {
        return this.update(model);
      }
      return this.create(model);
    }
  }
