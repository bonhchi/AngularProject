import { Injectable } from "@angular/core";
import { HttpClientService } from "../../../http/http-client";
import { BlogModel } from "../../models/blogs/blog.model";

@Injectable({
  providedIn: "root",
})
export class BlogService {
  private url = "/api/blog";
  private urlUser = this.url + "/user";
  private top = this.url + "/topblog";
  private recent = this.url + "/recentblog";

  constructor(private httpClient: HttpClientService) {}

  get(request: any) {
    return this.httpClient.getObservable(this.urlUser, request).toPromise();
  }

  getTop(request: any) {
    return this.httpClient.getObservable(this.top, request).toPromise();
  }

  getRecent(request: any) {
    return this.httpClient.getObservable(this.recent, request).toPromise();
  }

  getBlog(id: string) {
    const urlDetail = `${this.url}/${id}`;
    return this.httpClient.getObservable(urlDetail, id).toPromise();
  }
}
