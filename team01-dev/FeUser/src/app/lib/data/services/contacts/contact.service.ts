import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { ContactModel } from "../../models/contacts/contact.model";

@Injectable()
export class ContactService {
  private url = "/api/user/contact";

  constructor(private httpClient: HttpClientService) {}

  create(model: ContactModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }
}
