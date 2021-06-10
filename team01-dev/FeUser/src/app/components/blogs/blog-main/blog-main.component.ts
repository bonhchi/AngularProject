import { Component, HostListener, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { BlogModel } from "src/app/lib/data/models/blogs/blog.model";
import { FileService, MessageService } from "src/app/lib/data/services";
import { BlogService } from "src/app/lib/data/services/blogs/blog.service";

@Component({
  selector: "app-blog-main",
  templateUrl: "./blog-main.component.html",
  styleUrls: ["./blog-main.component.scss"],
})
export class BlogMainComponent implements OnInit {
  public blogs: BlogModel[];
  public topBlogs: BlogModel[];
  public recentBlogs: BlogModel[];
  public selectedId: string;

  constructor(
    private blogService: BlogService,
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}

  getBlogs() {
    this.blogService
      .get(null)
      .then((res: ReturnMessage<PageModel<BlogModel>>) => {
        if (!res.hasError) {
          this.blogs = res.data.results;
        }
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Server Disconnected",
          TypeSweetAlertIcon.ERROR
        );
      });
  }
  getTopBlogs() {
    this.blogService
      .getTop(null)
      .then((res: ReturnMessage<BlogModel[]>) => {
        if (!res.hasError) {
          this.topBlogs = res.data;
        }
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Server Disconnected",
          TypeSweetAlertIcon.ERROR
        );
      });
  }

  getRecentBlogs() {
    this.blogService
      .getRecent(null)
      .then((res: ReturnMessage<BlogModel[]>) => {
        if (!res.hasError) {
          this.recentBlogs = res.data;
        }
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Server Disconnected",
          TypeSweetAlertIcon.ERROR
        );
      });
  }
  ngOnInit() {
    this.getRoute();
    this.getBlogs();
    this.getTopBlogs();
    this.getRecentBlogs();
  }

  getRoute() {
    this.route.queryParams.subscribe((params) => {
      this.selectedId = params["id"];
    });
  }

  getImage(image) {
    return FileService.getLinkFile(image);
  }

  detail() {}
}
