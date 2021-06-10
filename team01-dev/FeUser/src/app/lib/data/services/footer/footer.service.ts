import { HttpClientService } from "src/app/lib/http/http-client";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class FooterService {
  private url = "/api/footer";
  constructor(private httpClient: HttpClientService) {}

  getFooters(request: any = null) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }
}
