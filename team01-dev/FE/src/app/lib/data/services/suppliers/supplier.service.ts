import { AppConfig } from 'src/app/lib/environments/config/appConfig';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { SupplierModel } from '../../models';

class SuppliersService  {

    private url = '/api/supplier';

  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  create(model: SupplierModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: SupplierModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: SupplierModel) {
    const url = `${this.url}/${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
}
