import { Component, OnDestroy, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import {
  AuthRegisterModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import { AuthService, MessageService } from "src/app/lib/data/services";
import Swal from "sweetalert2";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"],
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm: FormGroup;
  submitted = false;
  subDataUser: Subscription;
  userInfo: UserDataReturnDTOModel;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private sweetAlertService: MessageService
  ) {
    this.createRegisterForm();
  }
  ngOnDestroy(): void {
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  ngOnInit(): void {
    this.subDataUser = this.authService.callUserInfo.subscribe((res) => {
      this.userInfo = res;
      if (this.userInfo) {
        this.backUrl();
      }
    });
  }
  createRegisterForm() {
    this.registerForm = this.formBuilder.group(
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
        confirmPassword: [null, [Validators.required]],
      },
      { validators: this.checkValidators }
    );
  }

  get f() {
    return this.registerForm.controls;
  }

  checkValidators(group: FormGroup) {
    const pass = group.get("password");
    const confirmPass = group.get("confirmPassword");
    if (pass.value !== confirmPass.value) {
      confirmPass.setErrors({ mustMatch: true });
    }
  }

  async onRegister() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    var data: AuthRegisterModel = this.registerForm.value;
    data.username = data.username.trim();
    data.lastName = data.lastName.trim();
    data.firstName = data.firstName.trim();
    data.email = data.email.trim();
    data.confirmPassword = undefined;
    await this.authService
      .register(data)
      .then((data: ReturnMessage<UserDataReturnDTOModel>) => {
        localStorage.setItem("token", data.data.token);
        this.authService.changeUserInfo(data.data);
        // this.backUrl();
      })
      .catch((er) => {
        this.sweetAlertService.alert(
          "Đăng ký thất bại",
          TypeSweetAlertIcon.ERROR,
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Mất kết nối với máy chủ"
        );
      });
  }

  backUrl() {
    var returnUrl = this.activatedRoute.snapshot.queryParams["returnUrl"] || "/";
    this.callUrl(returnUrl);
  }

  callUrl(url: string) {
    this.router.navigateByUrl(url);
  }
}
