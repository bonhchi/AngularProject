import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { ReturnMessage, TypeSweetAlertIcon } from "../lib/data/models";
import { UserDataReturnDTOModel } from "../lib/data/models/users/user.model";
import { AuthService, MessageService } from "../lib/data/services";
import { HeaderOneComponent } from "../shared/header/header-one/header-one.component";

@Component({
  selector: "app-pages",
  templateUrl: "./pages.component.html",
  styleUrls: ["./pages.component.scss"],
  providers: [AuthService],
})
export class PagesComponent implements OnInit {
  public url: any;
  @ViewChild("headerRef") headerRef: HeaderOneComponent;

  constructor(
    private router: Router,
    private authService: AuthService,
    private sweetalertService: MessageService
  ) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.url = event.url;
      }
    });
  }

  getInformationUser() {
    const token = localStorage.getItem("token");
    if (token) {
      this.authService
        .getInformationUser()
        .then((res: ReturnMessage<UserDataReturnDTOModel>) => {
          this.authService.changeUserInfo(res.data);
        })
        .catch((res) => {
          this.sweetalertService.alert(
            "Login Expires",
            TypeSweetAlertIcon.ERROR
          );
          localStorage.removeItem("token");
          this.authService.changeUserInfo(null);
        });
    }
  }

  ngOnInit(): void {
    this.getInformationUser();
    setTimeout(() => {
      localStorage.setItem(
        "header",
        JSON.stringify(this.headerRef.offsetHeight)
      );
    }, 1000);
  }
}
