import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Data, Router } from '@angular/router';
import {
  AuthLoginModel,
  ReturnMessage,
  TypeSweetAlertIcon,
  UserDataReturnDTOModel,
} from 'src/app/lib/data/models';
import { AuthService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  public registerForm: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private activedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private messageService: MessageService,
  ) {
    this.createLoginForm();
    this.createRegisterForm();
    if (localStorage.getItem('token')) {
      this.backUrl();
    }
  }

  get f() {
    return this.loginForm.controls;
  }

  owlcarousel = [
    {
      title: 'Welcome to Clothing Store',

    },
    {
      title: 'This is the management page',
    },
  ];
  owlcarouselOptions = {
    loop: true,
    items: 1,
    dots: true,
  };

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
  }
  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
    });
  }

  ngOnInit() { }

  async onLogin() {
    this.submitted = true;
    if (!this.loginForm.valid) {
      return;
    }

    var data: AuthLoginModel = this.loginForm.value;
    await this.authService
      .login(data)
      .then((data: ReturnMessage<UserDataReturnDTOModel>) => {
        localStorage.setItem('token', data.data.token);
        localStorage.setItem('user', JSON.stringify(data.data));
        this.backUrl();
      })
      .catch((er) => {
        this.messageService.alert(
          'Login Fail',
          TypeSweetAlertIcon.ERROR,
          er.error.message ?? JSON.stringify(er.error.error) ?? "Server Disconnected",
        );
      });
  }

  backUrl() {
    var returnUrl = this.activedRoute.snapshot.queryParams['returnUrl'] || '/';
    this.router.navigateByUrl(returnUrl);
  }
}
