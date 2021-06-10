import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { BannerModel } from "../../models/banners/banner.model";


@Injectable()
export class BannersService {

  private url = '/api/banner';

  constructor(private httpClient: HttpClientService) { }

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: BannerModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: BannerModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: BannerModel) {
    const url = `${this.url}?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
  save(model: BannerModel) {
    if (model.id) {
      return this.update(model);
    }
    return this.create(model);
  }
}

