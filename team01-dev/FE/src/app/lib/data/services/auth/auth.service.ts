import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/app/lib/environments/environment';
import { HttpClientService } from 'src/app/lib/http/http-client';
import {
  AuthLoginModel,
  ReturnMessage,
  UserDataReturnDTOModel,
} from '../../models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClientService) {}

  private url = '/api/auth';

  ngOnInit(): void {}

  login(body: AuthLoginModel) {
    return this.http.postObservable(`${this.url}/login`, body).toPromise();
  }

  getInformationUser() {
    return this.http.getObservable(this.url).toPromise();
  }

  private static userInfo = new BehaviorSubject<UserDataReturnDTOModel>(
    JSON.parse(localStorage['user'] || 'null')
  );
  callUserInfo = AuthService.userInfo.asObservable();

  changeUserInfo(userInfo: UserDataReturnDTOModel) {
    localStorage['user'] = JSON.stringify(userInfo);
    AuthService.userInfo.next(userInfo);
  }
}
