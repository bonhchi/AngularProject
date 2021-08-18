import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  ChangePasswordProfileModel,
  ProfileModel,
  ReturnMessage,
  TypeSweetAlertIcon,
  UserDataReturnDTOModel,
} from 'src/app/lib/data/models';

import { AuthService, ProfileService } from 'src/app/lib/data/services';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  ModalFile,
  TypeFile,
  EntityType,
} from 'src/app/shared/components/modals/models/modal.model';
import { Subscription } from 'rxjs';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss'],
  providers: [AuthService],
})
export class ProfileSettingsComponent implements OnInit, OnDestroy {
  public userInfo: UserDataReturnDTOModel;
  public profileForm: FormGroup;
  public passwordForm: FormGroup;
  submitted = false;
  public updateProfile: ProfileModel;
  public passwordProfile: ChangePasswordProfileModel;
  update = false;

  public modalFile: ModalFile;
  public fileURL: (String | ArrayBuffer)[];

  entrySub: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.USER;
  }
  ngOnDestroy(): void {
    if (this.entrySub) {
      this.entrySub.unsubscribe();
      this.entrySub = null;
    }
  }

  ngOnInit() {
    this.entrySub = this.authService.callUserInfo.subscribe((it) => {
      this.userInfo = it;
      this.loadFormItem();
      if (this.userInfo) {
        this.fileURL = [];
        this.fileURL.push(this.userInfo.imageUrl);
      }
    });
  }

  get profileFormControl() {
    return this.profileForm.controls;
  }
  get passwordFormControl() {
    return this.passwordForm.controls;
  }

  loadFormItem() {
    this.profileForm = this.formBuilder.group({
      firstName: [
        this.userInfo ? this.userInfo.firstName : '',
        Validators.required,
      ],
      lastName: [
        this.userInfo ? this.userInfo.lastName : '',
        Validators.required,
      ],
      email: [this.userInfo ? this.userInfo.email : '', Validators.required],
      imageUrl: [
        this.userInfo ? this.userInfo.imageUrl : '',
        Validators.required,
      ],
    });
    this.passwordForm = this.formBuilder.group({
      password: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmNewPassword: ['', Validators.required],
    });
  }

  updateSwitch() {
    this.modalFile.listFile = [];
    this.update = this.update == true ? false : true;
  }
  updateDetails() {
    if (this.profileForm.invalid) {
      return;
    }
    this.updateProfile = {
      firstName: this.profileForm.controls.firstName.value,
      lastName: this.profileForm.controls.lastName.value,
      email: this.profileForm.controls.email.value,
      imageUrl: this.profileForm.controls.imageUrl.value,
      id: this.userInfo.id,
      files: this.modalFile.listFile,
    };

    this.profileService
      .update(this.updateProfile)
      .then((resp: ReturnMessage<UserDataReturnDTOModel>) => {
        this.authService.changeUserInfo(resp.data);
        this.route.snapshot.data.user = resp.data;
        if (!resp.hasError) {
          this.messageService.notification(
            'Thông tin đã được cập nhật',
            TypeSweetAlertIcon.SUCCESS
          );
          this.updateSwitch();
        }
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            'Mất kết nối với máy chủ',
          TypeSweetAlertIcon.ERROR
        );
      });
  }
  changePassword() {
    if (
      this.passwordForm.invalid ||
      this.passwordFormControl.confirmNewPassword.value !==
        this.passwordFormControl.newPassword.value
    ) {
      this.submitted = true;
      return;
    }
    this.passwordProfile = {
      password: this.passwordForm.controls.password.value,
      newPassword: this.passwordForm.controls.newPassword.value,
      confirmNewPassword: this.passwordForm.controls.confirmNewPassword.value,
      userName: this.userInfo.username,
    };
    this.profileService
      .changePassword(this.passwordProfile)
      .then((resp) => {
        this.passwordForm.reset();
        this.messageService.notification(
          'Mật khẩu đã được thay đổi',
          TypeSweetAlertIcon.SUCCESS
        );
        this.submitted = false;
      })
      .catch((er) => {
        this.messageService.notification(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            'Mất kết nối với máy chủ',
          TypeSweetAlertIcon.ERROR
        );
      });
  }

  onChangeData(event: { add: string[]; remove: string; removeAll: boolean }) {
    if (event == null) {
      return;
    }

    if (!this.fileURL) {
      this.fileURL = [];
    }

    if (event.add) {
      this.fileURL = [...this.fileURL, ...event.add];
    }

    if (event.remove) {
      this.fileURL.forEach((e: string, i) => {
        if (e.includes(event.remove)) {
          this.fileURL.splice(i, 1);
        }
      });
    }

    if (event.removeAll) {
      this.fileURL = [];
    }

    this.profileForm.controls.imageUrl.setValue(this.fileURL.join(','));
  }

  get getImage() {
    return this.userInfo?.imageUrl;
  }
}
