import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import {
  ChangePasswordProfileModel,
  ProfileModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import {
  AuthService,
  FileService,
  MessageService,
} from "src/app/lib/data/services";
import { ProfileService } from "src/app/lib/data/services/profiles/profile.service";

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.scss"],
  providers: [ProfileService, AuthService],
})
export class ProfileComponent implements OnInit, OnDestroy {
  updateProfile: boolean = true;
  updatePassword: boolean = true;
  changePasswordForm: FormGroup;
  profileForm: FormGroup;
  user: UserDataReturnDTOModel;

  subDataUser: Subscription;

  submittedProfile = false;
  submittedPassword = false;


  constructor(
    private formBuilder: FormBuilder,
    private profileService: ProfileService,
    private authService: AuthService,
    private messageService: MessageService,
    private router: Router
  ) {
  }
  ngOnDestroy(): void {
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  ngOnInit(): void {
    this.subDataUser = this.authService.callUserInfo.subscribe((it) => {
      this.user = it;
      this.createProfileForm();
      if (!it) this.router.navigate([""]);
    });
    this.createChangePasswordForm();
  }

  profileSwitch() {
    this.updateProfile = !this.updateProfile;
    this.submittedProfile = false;
    this.createProfileForm();
  }

  passwordSwitch() {
    this.updatePassword = !this.updatePassword;
    this.submittedPassword = false;
    this.createChangePasswordForm();
  }

  get fProfile() {
    return this.profileForm.controls;
  }

  get fPassword() {
    return this.changePasswordForm.controls;
  }

  createProfileForm() {
    this.profileForm = this.formBuilder.group({
      firstName: [
        this.user ? this.user.firstName : "",
        [Validators.required, Validators.maxLength(35)],
      ],
      lastName: [
        this.user ? this.user.lastName : "",
        [Validators.required, Validators.maxLength(35)],
      ],
      phone: [
        this.user ? this.user.phone : "",
        [Validators.pattern("[0-9]{10,11}"), Validators.maxLength(11)],
      ],
      address: [this.user ? this.user.address : "", [Validators.maxLength(90)]],
      email: [
        this.user ? this.user.email : "",
        [
          Validators.required,
          Validators.pattern("[A-Za-z0-9._%-]+@[A-Za-z0-9._%-]+\\.[a-z]{2,3}"),
          Validators.maxLength(90),
        ],
      ],
    });
  }

  createChangePasswordForm() {
    this.changePasswordForm = this.formBuilder.group(
      {
        password: [null, [Validators.required]],
        newPassword: [null, [Validators.required]],
        confirmPassword: [null, [Validators.required]],
      },
      { validators: this.checkValidatorsPassword }
    );
  }

  checkValidatorsPassword(group: FormGroup) {
    const pass = group.get("newPassword");
    const confirmPass = group.get("confirmPassword");
    if (pass.value !== confirmPass.value) {
      confirmPass.setErrors({ mustMatch: true });
    }
  }

  async onChangePassword() {
    this.submittedPassword = true;

    if (this.changePasswordForm.invalid) {
      return;
    }
    var data: ChangePasswordProfileModel = this.changePasswordForm.value;

    await this.profileService
      .changePassword(data)
      .then((res: ReturnMessage<null>) => {
        this.messageService.notification(
          "Thay đổi mật khẩu thành công",
          TypeSweetAlertIcon.SUCCESS
        );
        this.passwordSwitch();
      })
      .catch((er) => {
        this.messageService.alert(
          "Thay đổi mật khẩu thất bại",
          TypeSweetAlertIcon.ERROR,
          er.error.message ?? er.error.error ?? "Mất kết nối với máy chủ"
        );
      });
  }

  async onUpdateProfile() {
    this.submittedProfile = true;

    if (this.profileForm.invalid) {
      // console.log(this.profileForm.invalid);
      return;
    }

    var dataProfileForm: ProfileModel = this.profileForm.value;
    var data: ProfileModel = {
      imageUrl: null,
      email: dataProfileForm.email,
      address: dataProfileForm.address,
      firstName: dataProfileForm.firstName,
      lastName: dataProfileForm.lastName,
      files: null,
      phone: dataProfileForm.phone,
    };

    await this.profileService
      .update(data)
      .then((res: ReturnMessage<UserDataReturnDTOModel>) => {
        this.authService.changeUserInfo(res.data);
        this.messageService.notification(
          "Update Profile Success",
          TypeSweetAlertIcon.SUCCESS
        );
        this.profileSwitch();
      })
      .catch((er) => {
        this.messageService.alert(
          "Update Profile Fail",
          TypeSweetAlertIcon.ERROR,
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Server Disconnected"
        );
      });
  }

  getImage(fileName: string) {
    return FileService.getLinkFile(fileName);
  }

  // onChangeData(event: { add: string[]; remove: string; removeAll: boolean }) {
  //   if (event == null) {
  //     return;
  //   }

  //   if (!this.fileURL) {
  //     this.fileURL = [];
  //   }

  //   if (event.add) {
  //     this.fileURL = [...this.fileURL, ...event.add];
  //   }

  //   if (event.remove) {
  //     this.fileURL.forEach((e : string, i) => {
  //       if (e.includes(event.remove)) {
  //         this.fileURL.splice(i, 1);
  //       }
  //     });
  //   }

  //   if (event.removeAll) {
  //     this.fileURL = [];
  //   }

  //   this.profileForm.controls.imageUrl.setValue(this.fileURL.join(','));
  // }
}
