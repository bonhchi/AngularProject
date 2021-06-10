import { HttpClientService } from 'src/app/lib/http/http-client';
import { Injectable, HostListener } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HeaderModel } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  private url = '/api/header';
  public screenWidth: any;
  public leftMenuToggle: boolean = false;
  public mainMenuToggle: boolean = false;

  // Windows width
  @HostListener('window:resize', ['$event'])
  onResize(event?) {
    this.screenWidth = window.innerWidth;
  }
  constructor(private httpClient: HttpClientService) {
  }
  getHeader(request: any = null){
    return  this.httpClient.getObservable(this.url, request).toPromise();
  }

  private static headerModel = new BehaviorSubject<HeaderModel>(null);
  callHeaderModel = HeaderService.headerModel.asObservable();

  changeHeaderModel(headerModel: HeaderModel) {
    HeaderService.headerModel.next(headerModel);
  }
}
