import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { UserModel } from "../../models/users/user.model";

@Injectable()
export class UserService {
  private url = "/api/user";

  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  getUserById(id: string) {
    const url = `${this.url}/${id}`;
    return this.httpClient.getObservable(url).toPromise();
  }

  create(model: UserModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: UserModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }

  delete(model: UserModel) {
    const url = `${this.url}/?id=${model?.id}`;
    return this.httpClient.deleteObservable(url).toPromise();
  }

  save(model: UserModel) {
    if (model.id) return this.update(model);
    return this.create(model);
  }
}
