import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerModel, TypeSweetAlertIcon } from 'src/app/lib/data/models';
import { CustomerService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import {
  ModalHeaderModel,
  ModalFooterModel,
  ModalFile,
  TypeFile,
  EntityType,
} from 'src/app/shared/components/modals/models/modal.model';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.scss'],
  providers: [CustomerService],
})
export class CustomerDetailsComponent implements OnInit {
  public usersForm: FormGroup;
  public permissionForm: FormGroup;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public user: CustomerModel;
  @Input() item: CustomerModel;

  public modalFile: ModalFile;
  public fileURL: (String | ArrayBuffer)[];

  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private customerService: CustomerService,
    private ngbActiveModal: NgbActiveModal,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.USER;
  }

  ngOnInit() {
    this.loadItem();
    if (this.item) {
      this.fileURL = [];
      this.fileURL.push(this.item.imageUrl);
    }
  }

  loadItem() {
    this.usersForm = this.formBuilder.group({
      username: [this.item ? this.item.username : '', [Validators.required]],
      password: [this.item ? this.item.password : '', [Validators.required]],
      email: [
        this.item ? this.item.email : '',
        [Validators.required, Validators.email],
      ],
      firstName: [this.item ? this.item.firstName : '', [Validators.required]],
      lastName: [this.item ? this.item.lastName : '', [Validators.required]],
      imageUrl: [this.item ? this.item.imageUrl : ''],
      address: [this.item ? this.item.address : ''],
      phone: [this.item ? this.item.phone : ''],
    });

    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = this.item
      ? `Cập nhật ${this.item.username}`
      : `Thêm`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  get f() {
    return this.usersForm.controls;
  }

  save() {
    this.submitted = true;
    if (this.usersForm.invalid) {
      return;
    }

    this.user = {
      username: this.usersForm.value.username,
      password: this.usersForm.value.password,
      email: this.usersForm.value.email,
      firstName: this.usersForm.value.firstName,
      lastName: this.usersForm.value.lastName,
      imageUrl: this.usersForm.value.imageUrl,
      id: this.item ? this.item.id : '',
      files: this.modalFile.listFile,
      address: this.usersForm.value.address,
      phone: this.usersForm.value.phone,
    };

    this.callServiceToSave();
  }

  callServiceToSave() {
    this.customerService
      .save(this.user)
      .then(() => {
        this.messageService.notification(
          this.item ? 'Thông tin đã được cập nhật' : 'Thông tin đã được tạo',
          TypeSweetAlertIcon.SUCCESS
        );
        this.ngbActiveModal.close();
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

  close(event: any) {
    this.ngbActiveModal.dismiss();
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

    this.usersForm.controls.imageUrl.setValue(this.fileURL.join(','));
  }
}
