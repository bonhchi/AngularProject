import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { HeaderModel, Menu } from "src/app/lib/data/models/header/header.model";
import { HeaderService } from "src/app/lib/data/services";

@Component({
  selector: "app-left-menu",
  templateUrl: "./left-menu.component.html",
  styleUrls: ["./left-menu.component.scss"],
})
export class LeftMenuComponent implements OnInit, OnDestroy {
  public menuItems: Menu[] = [];
  public leftMenu: boolean = false;
  public headerModel: HeaderModel;
  event: any = {};
  public subHeader: Subscription;
  constructor(public headerService: HeaderService, private router: Router) {}
  ngOnDestroy(): void {
    this.subHeader.unsubscribe();
    this.subHeader = null;
  }

  ngOnInit(): void {
    this.subHeader = this.headerService.callHeaderModel.subscribe((res) => {
      this.headerModel = res;
      this.loadMenu();
    })
  }

  leftMenuToggle(): void {
    this.leftMenu = !this.leftMenu;
  }


  async loadMenu() {
    this.menuItems = [];
    this.headerModel?.categories.forEach((item) => {
      this.menuItems.push({
        title: item.name,
        path: `/product?search.categoryName=${item.name}`,
        type: "link",
      });
    });
  }
  // Click Toggle menu (Mobile)
  toggletNavActive(item) {
    item.active = !item.active;
  }

  onHover(menuItem) {
    if (window.innerWidth > 1200 && menuItem) {
      document.getElementById("unset").classList.add("sidebar-unset");
    } else {
      document.getElementById("unset").classList.remove("sidebar-unset");
    }
  }
  loadUrlNavaigate(url: string) {
    this.router.navigateByUrl(url);
    this.leftMenuToggle();
  }

  bigImg(event) {
    this.event["font-weight"] = event["font-weight"];

    event["font-weight"] = "bold";
  }

  normalImg(event) {
    if (this.event) {
      event["font-weight"] = this.event["font-weight"];
    }
  }
}
