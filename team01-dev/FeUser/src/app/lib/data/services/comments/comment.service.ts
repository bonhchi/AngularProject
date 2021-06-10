import { Injectable } from "@angular/core";
import { HttpClientService } from "src/app/lib/http/http-client";
import { SearchPaganationDTO } from "../../models";
import {
  CommentModel,
  SearchCommentModel,
} from "../../models/comments/comment.model";

@Injectable()
export class CommentService {
  private url = "/api/comment";

  constructor(private httpClient: HttpClientService) {}

  getBlogComments(request: any) {
    const url = this.url + `/blog`;
    return this.httpClient.getObservable(url, { params: request }).toPromise();
  }

  getProductComments(request: any) {
    const url = this.url + `/product`;
    return this.httpClient.getObservable(url, { params: request }).toPromise();
  }

  create(model: CommentModel) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  update(model: CommentModel) {
    return this.httpClient.putObservable(this.url, model).toPromise();
  }
}
