import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { HttpClientService } from "src/app/lib/http/http-client";
import { AuthLoginModel, AuthRegisterModel } from "../../models";
import { UserDataReturnDTOModel } from "../../models/users/user.model";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private http: HttpClientService) {}

  private url = "/api/user/auth";
  private urlLogin = this.url + "/login";
  private urlRegister = this.url + "/regist";

  ngOnInit(): void {}

  login(body: AuthLoginModel) {
    return this.http.postObservable(this.urlLogin, body).toPromise();
  }

  register(body: AuthRegisterModel) {
    return this.http.postObservable(this.urlRegister, body).toPromise();
  }

  getInformationUser() {
    return this.http.getObservable(this.url).toPromise();
  }

  private static userInfo = new BehaviorSubject<UserDataReturnDTOModel>(
    JSON.parse(localStorage["user"] || "null")
  );
  callUserInfo = AuthService.userInfo.asObservable();

  changeUserInfo(userInfo: UserDataReturnDTOModel) {
    localStorage["user"] = JSON.stringify(userInfo);
    AuthService.userInfo.next(userInfo);
  }
}
