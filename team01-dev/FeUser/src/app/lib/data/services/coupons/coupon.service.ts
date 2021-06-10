import { Injectable } from '@angular/core';
import { HttpClientService } from 'src/app/lib/http/http-client';
import { CouponModel } from '../../models/coupons/coupon.model';

@Injectable()
export class CouponService {
  private url = '/api/coupon';

  constructor(private httpClient: HttpClientService) { }

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  getByCode(request: any, code: string) {
    return this.httpClient.getObservable(this.url + '/by-code?code=' + code, request).toPromise();
  }

  create(model: CouponModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: CouponModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: CouponModel) {
    const url = `${this.url}/?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }
}
