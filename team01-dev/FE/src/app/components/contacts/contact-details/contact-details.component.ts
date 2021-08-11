import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as ClassicEditor from 'src/app/lib/customCkeditor/ckeditor5-build-classic';
import Base64UploaderPlugin from 'src/app/lib/@ckeditor/Base64Upload';
import {
  ModalFooterModel,
  ModalHeaderModel,
} from 'src/app/shared/components/modals/models/modal.model';
import { ContactModel } from 'src/app/lib/data/models/contact/contact.model';
import { ContactService } from 'src/app/lib/data/services/contacts/contact.service';
@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.scss'],
  providers: [ContactService],
})
export class ContactDetailComponent implements OnInit {
  public pageContentForm: FormGroup;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public contact: ContactModel;
  @Input() item;

  public editor = ClassicEditor;
  public editorConfig = {
    extraPlugins: [Base64UploaderPlugin],
  };
  public onReady(editor) {
    editor.ui
      .getEditableElement()
      .parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
      );
  }

  constructor(
    private formBuilder: FormBuilder,
    private pageContentService: ContactService,
    private ngbActiveModal: NgbActiveModal
  ) {}

  ngOnInit() {
    this.loadItem();
    this.createModal();
  }

  createModal() {
    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = `Cập nhật liên hệ`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.buttons = [
      {
        color: 'btn btn-primary',
        title: 'đóng',
        onAction: (event: any) => {
          this.ngbActiveModal.dismiss();
        },
      },
    ];
    if (this.item.status == 'Đang chờ') {
      this.modalFooter.buttons = [
        {
          color: 'btn btn-success',
          title: 'Đang xét duyệt',
          onAction: (event: any) => {
            this.progress();
          },
        },
        {
          color: 'btn btn-primary',
          title: 'Hoàn thành',
          onAction: (event: any) => {
            this.complete();
          },
        },
      ];
    }
    if (this.item.status == 'Đang xét duyệt') {
      this.modalFooter.buttons = [
        {
          color: 'btn btn-primary',
          title: 'Hoàn thành',
          onAction: (event: any) => {
            this.complete();
          },
        },
      ];
    }
  }

  loadItem() {
    this.pageContentForm = this.formBuilder.group({
      firstName: [this.item ? this.item.firstName : ''],
      lastName: [this.item ? this.item.lastName : ''],
      email: [this.item ? this.item.email : ''],
      phoneNumber: [this.item ? this.item.phoneNumber : ''],
      message: [this.item ? this.item.message : ''],
      status: [this.item ? this.item.status : ''],
    });
  }

  loadModel() {
    this.contact = {
      id: this.item ? this.item.id : '',
      firstName: this.pageContentForm.value.firstName,
      lastName: this.pageContentForm.value.lastName,
      email: this.pageContentForm.value.email,
      phoneNumber: this.pageContentForm.value.phoneNumber,
      message: this.pageContentForm.value.message,
      status: this.item.status,
    };
  }

  callServiceToSave() {
    this.pageContentService
      .save(this.contact)
      .then(() => {
        this.ngbActiveModal.close();
      })
      .catch((er) => {
        if (er.error.hasError) {
        }
      });
  }

  progress() {
    if (this.pageContentForm.invalid) {
      return;
    }

    this.loadModel();
    this.contact.status = 'Đang xét duyệt';
    this.callServiceToSave();
  }

  complete() {
    if (this.pageContentForm.invalid) {
      return;
    }

    this.loadModel();
    this.contact.status = 'Hoàn thành';
    this.callServiceToSave();
  }

  close(event: any) {
    this.ngbActiveModal.dismiss();
  }
}
