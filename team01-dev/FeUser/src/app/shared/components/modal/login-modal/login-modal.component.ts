import { AfterContentInit, Component, Input, OnDestroy } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NgbActiveModal, NgbTabset } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs";
import {
  AuthLoginModel,
  AuthRegisterModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import { MessageService } from "src/app/lib/data/services";
import { AuthService } from "src/app/lib/data/services/auth/auth.service";

@Component({
  selector: "app-login-modal",
  templateUrl: "./login-modal.component.html",
  styleUrls: ["./login-modal.component.scss"],
})
export class LoginModalComponent implements AfterContentInit, OnDestroy {
  public loginForm: FormGroup;
  submitted = false;
  registForm: FormGroup;
  @Input() isRegister;
  activeIdString = "tab-login";

  subDataUser: Subscription;
  userInfo: UserDataReturnDTOModel;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private router: Router,
    public activeModal: NgbActiveModal
  ) {
    this.createLoginForm();
    this.createRegistForm();
  }

  ngAfterContentInit() {
    this.subDataUser = this.authService.callUserInfo.subscribe((res) => {
      this.userInfo = res;
      if (this.userInfo) {
        this.activeModal.dismiss();
      }
    });
    if (this.isRegister) this.activeIdString = "tab-register";
  }
  ngOnDestroy(): void {
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  createRegistForm() {
    this.registForm = this.formBuilder.group(
      {
        firstName: [null, [Validators.required]],
        lastName: [null, [Validators.required]],
        username: [null, [Validators.required]],
        email: [
          null,
          [
            Validators.required,
            Validators.pattern(
              "[A-Za-z0-9._%-]+@[A-Za-z0-9._%-]+\\.[a-z]{2,3}"
            ),
          ],
        ],
        password: [null, [Validators.required]],
        confirmpassword: [null, [Validators.required]],
      },
      { validators: this.checkValidators }
    );
  }

  get registerF() {
    return this.registForm.controls;
  }

  checkValidators(group: FormGroup) {
    const pass = group.get("password");
    const confirmpass = group.get("confirmpassword");
    if (pass.value !== confirmpass.value) {
      confirmpass.setErrors({ mustMatch: true });
    }
  }

  async onRegist() {
    this.submitted = true;

    if (this.registForm.invalid) {
      return;
    }

    var data: AuthRegisterModel = this.registForm.value;
    data.username = data.username.trim();
    data.lastName = data.lastName;
    data.firstName = data.firstName.trim();
    data.email = data.email.trim();
    data.confirmPassword = undefined;
    await this.authService
      .register(data)
      .then((data: ReturnMessage<UserDataReturnDTOModel>) => {
        localStorage.setItem("token", data.data.token);
        this.authService.changeUserInfo(data.data);
        window.location.reload();
      })
      .catch((er) => {
        this.messageService.alert(
          "Đăng ký thất bại",
          TypeSweetAlertIcon.ERROR,
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Mất kết nối với máy chủ"
        );
      });
  }

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
  }

  callUrl(url: string) {
    this.router.navigateByUrl(url);
  }

  get loginF() {
    return this.loginForm.controls;
  }

  async onLogin() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    var data: AuthLoginModel = this.loginForm.value;
    data.username = data.username.trim();
    await this.authService
      .login(data)
      .then((data: ReturnMessage<UserDataReturnDTOModel>) => {
        localStorage.setItem("token", data.data.token);
        this.authService.changeUserInfo(data.data);
        window.location.reload();
      })
      .catch((er) => {
        this.messageService.alert(
          "Login Fail",
          TypeSweetAlertIcon.ERROR,
          `${er.error.message ?? er.error}`
        );
      });
  }
}
