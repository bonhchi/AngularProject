import { NavigationEnd, NavigationStart, Route, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { RouterInfoModel } from '../data/models/common/router-info.model';

@Injectable({
  providedIn: 'root',
})
export class RouterHelperService {
  private previousUrl: string = undefined;
  private currentUrl: string = undefined;
  private currentRouter: NavigationEnd;
  public eventRouter = new Subject<RouterInfoModel>();
  private routerInfo: RouterInfoModel = new RouterInfoModel();

  constructor(private router: Router) {
    this.currentUrl = this.router.url;

    router.events.subscribe(event => {

      if (event instanceof NavigationEnd) {
        this.currentRouter = event;
        this.routerInfo.previousUrl = this.currentUrl;
        this.routerInfo.currentUrl = event.url;
        this.eventRouter.next(this.routerInfo);
      }
    });
  }

  get getPreviousUrl() {
    return this.previousUrl;
  }

  checkCurrentPage(path: string) {
    const url = location.pathname;
    if (url.indexOf(path) !== -1) {
      return true;
    }
    return false;
  }

  redirectToLogin() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    let returnUrl = ``;
    if (window.location.pathname.length !== 0) {
      returnUrl = `${window.location.pathname}${encodeURIComponent(window.location.search)}`;
    }

    if (returnUrl) {
      this.router.navigate([`/login`], { queryParams: { returnUrl }, queryParamsHandling: 'merge' });
    } else {
      this.router.navigate([`/login`]);
    }
  }
}
