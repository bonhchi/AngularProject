import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfig } from '../environments/config/appConfig';

@Injectable({
  providedIn: 'root',
})
export class ApiPrefixInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (
      request.url.includes('assets/env/config') ||
      request.url.includes('assets/i18n')
    ) {
      return next.handle(request);
    }

    if (!/^(http|https):/i.test(request.url) &&
      !request.url.includes(AppConfig.settings.API_URL)
    ) {
      request = request.clone({ url: AppConfig.settings.API_URL + request.url });
    }

    return next.handle(request);
  }
}
