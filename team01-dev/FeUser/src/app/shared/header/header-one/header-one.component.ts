import {
  Component,
  OnInit,
  Input,
  HostListener,
  OnDestroy,
  ViewChild,
  ElementRef,
} from "@angular/core";
import { Router } from "@angular/router";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import { HeaderModel, ReturnMessage } from "src/app/lib/data/models";
import { AuthService, HeaderService } from "src/app/lib/data/services";
import { TypeDisplayImage } from "../../data";
import { Subscription } from "rxjs";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { LoginModalComponent } from "../../components/modal/login-modal/login-modal.component";
import { Title } from "@angular/platform-browser";

@Component({
  selector: "app-header-one",
  templateUrl: "./header-one.component.html",
  styleUrls: ["./header-one.component.scss"],
  providers: [AuthService],
})
export class HeaderOneComponent implements OnInit, OnDestroy {
  @Input() class: string;
  @Input() themeLogo: string = "assets/images/icon/logo.png"; // Default Logo
  @Input() topbar: boolean = true; // Default True
  @Input() sticky: boolean = false; // Default false
  typeDisplayImage = TypeDisplayImage;
  user: UserDataReturnDTOModel;
  subDataUser: Subscription;
  @ViewChild("headerRef") headerRef: ElementRef;
  offsetHeight: number = 0;

  public headerModel: HeaderModel;

  public stick: boolean = false;
  loadUrlNavaigate(url: string) {
    this.router.navigateByUrl(url);
  }

  constructor(
    private router: Router,
    public headerService: HeaderService,
    private authService: AuthService,
    private modalService: NgbModal,
    private titleService: Title
  ) {}
  ngOnDestroy(): void {
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  ngOnInit(): void {
    this.subDataUser = this.authService.callUserInfo.subscribe(
      (it) => (this.user = it)
    );
    this.loadHeaderModel();

    setTimeout(() => {
      this.offsetHeight = this.headerRef.nativeElement.offsetHeight;
    }, 0);
  }

  // @HostListener Decorator
  @HostListener("window:scroll", [])
  onWindowScroll() {
    let number =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
    if (number >= 150 && window.innerWidth > 400) {
      this.stick = true;
    } else {
      this.stick = false;
    }
  }

  onLogout() {
    this.authService.changeUserInfo(null);
    localStorage.removeItem("token");
  }
  async loadHeaderModel() {
    await this.headerService
      .getHeader()
      .then((res: ReturnMessage<HeaderModel>) => {
        this.headerModel = res.data;
        this.headerService.changeHeaderModel(res.data);
        this.titleService.setTitle(res.data.informationWeb.title);
      });
  }

  GotoWishList() {
    if (!this.user) {
      const modalRef = this.modalService.open(LoginModalComponent, {
      });
      return;
    }
    this.loadUrlNavaigate("wishlist");
  }

  loadModal() {
    if (!this.user) {
      const modalRef = this.modalService.open(LoginModalComponent, {
      });
      return;
    }
  }

  OpenLoginModal() {
    const modalRef = this.modalService.open(LoginModalComponent, {
    });
  }

  OpenRegisterModal() {
    const modalRef = this.modalService.open(LoginModalComponent, {
    });
    modalRef.componentInstance.isRegister = true;
  }
}
