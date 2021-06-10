import {
  Component,
  ElementRef,
  Input,
  OnDestroy,
  OnInit,
  ViewChild,
} from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { CategoryModel } from "src/app/lib/data/models";
import { BlogModel } from "src/app/lib/data/models/blogs/blog.model";
import { HeaderModel, Menu } from "src/app/lib/data/models/header/header.model";
import { HeaderService } from "src/app/lib/data/services";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.scss"],
})
export class MenuComponent implements OnInit, OnDestroy {
  public menuItems: Menu[] = [];
  public mainMenu: boolean = false;

  public headerModel: HeaderModel;
  public categoriesChildren: Menu[] = [];
  public blogsChildren: Menu[] = [];
  @ViewChild("Containermenu") Containermenu: ElementRef;

  public subHeader: Subscription;

  constructor(public headerService: HeaderService, private router: Router) {}
  ngOnDestroy(): void {
    this.subHeader.unsubscribe();
    this.subHeader = null;
  }

  ngOnInit() {
    this.subHeader = this.headerService.callHeaderModel.subscribe((res) => {
      this.headerModel = res;
      this.loadMenu();
    });
  }

  mainMenuToggle(): void {
    this.mainMenu = !this.mainMenu;
  }

  // Click Toggle menu (Mobile)
  toggletNavActive(item) {
    item.active = !item.active;
  }

  // async loadHeaderModel() {
  //   await this.headerService.getHeader(null).then((res: any) => {
  //     this.headerModel = res.data;
  //   });
  // }

  loadMenu() {
    this.menuItems = [];
    this.blogsChildren = [];
    this.categoriesChildren = [];
    // await this.loadHeaderModel();

    this.headerModel?.categories.slice(0, 5).forEach((item) => {
      this.categoriesChildren.push({
        title: item.name,
        path: `/product?search.categoryName=${item.name}`,
        type: "link",
      });
    });

    this.headerModel?.blogs.forEach((item) => {
      this.blogsChildren.push({
        title:
          item.title.length <= 25
            ? item.title
            : item.title.slice(0, 25) + "...",
        path: `/blog/${item.id}`,
        type: "link",
      });
    });

    this.menuItems.push(
      { title: "Trang chủ", type: "link", path: "/home" },
      {
        title: "Danh mục sản phẩm",
        type: "sub",
        active: false,
        children: this.categoriesChildren,
      },
      { title: "Sản phẩm", type: "link", path: "/product" },
      {
        title: "Bài báo",
        type: "link",
        path: "/blog",
        active: false,
        children: this.blogsChildren,
      }
    );
  }

  loadUrlNavaigate(url: string) {
    this.router.navigateByUrl(url);
  }
}
