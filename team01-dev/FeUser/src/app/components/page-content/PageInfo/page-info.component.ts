import { HttpParams } from "@angular/common/http";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";
import { Subscription } from "rxjs";
import { ReturnMessage, TypeSweetAlertIcon } from "src/app/lib/data/models";
import { PageContentInfoModel } from "src/app/lib/data/models/pageContent/pageContent.model";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import { AuthService, MessageService } from "src/app/lib/data/services";
import { ContactService } from "src/app/lib/data/services/contacts/contact.service";
import { PageContentService } from "src/app/lib/data/services/pageContent/pageContent.service";

@Component({
  selector: "app-page-info",
  styleUrls: ["./page-info.component.scss"],
  templateUrl: "./page-info.component.html",
  providers: [PageContentService, ContactService, AuthService],
})
export class PageContentInfoComponent implements OnInit, OnDestroy {
  pageContentInfo: PageContentInfoModel;
  contactForm: FormGroup;
  userInfo: UserDataReturnDTOModel;
  subDataUser: Subscription;
  submitted = false;
  src = this.sanitizer.bypassSecurityTrustResourceUrl("");
  constructor(
    private pageContentService: PageContentService,
    private contactService: ContactService,
    private messageService: MessageService,
    private authservice: AuthService,
    private fb: FormBuilder,
    private sanitizer: DomSanitizer
  ) {}
  ngOnDestroy(): void {
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  ngOnInit() {
    this.subDataUser = this.authservice.callUserInfo.subscribe((it) => {
      this.userInfo = it;
      this.initForm();
    });
    this.getInfo();
  }

  initForm() {
    this.submitted = false;

    this.contactForm = this.fb.group({
      firstName: [
        this.userInfo ? this.userInfo.firstName : "",
        [Validators.required],
      ],
      lastName: [
        this.userInfo ? this.userInfo.lastName : "",
        [Validators.required],
      ],
      phoneNumber: [
        this.userInfo ? this.userInfo.phone : "",
        [Validators.pattern("[0-9]{10,11}"), Validators.required],
      ],
      email: [
        this.userInfo ? this.userInfo.email : "",
        [Validators.required, Validators.email],
      ],
      message: ["", [Validators.required, Validators.maxLength(500)]],
    });
  }

  getInfo() {
    this.pageContentService
      .getInfo()
      .then((data: ReturnMessage<PageContentInfoModel>) => {
        this.pageContentInfo = data.data;
        var params = new HttpParams();
        params = params.set("q", data.data.address);
        this.src = this.sanitizer.bypassSecurityTrustResourceUrl(
          `https://maps.google.com/maps?${params.toString()}&t=&z=20&ie=UTF8&iwloc=&output=embed`
        );
      });
  }

  Submit() {
    this.submitted = true;

    if (this.contactForm.invalid) {
      return;
    }
    return this.contactService
      .create(this.contactForm.value)
      .then(() => {
        this.initForm();
        this.messageService.notification(
          "Góp ý của bạn đã được gửi",
          TypeSweetAlertIcon.SUCCESS
        );
      })
      .catch((e) => {
        this.messageService.alert(
          e.error ? e.error.message : "Mất kết nối với máy chủ",
          TypeSweetAlertIcon.ERROR
        );
      });
  }
}
