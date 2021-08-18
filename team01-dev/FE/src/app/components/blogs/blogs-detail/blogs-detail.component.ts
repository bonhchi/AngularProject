import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BlogModel } from 'src/app/lib/data/models/blogs/blog.model';
import { BlogService } from 'src/app/lib/data/services/blogs/blog.service';
import {
  EntityType,
  ModalFile,
  ModalFooterModel,
  ModalHeaderModel,
  TypeFile,
} from 'src/app/shared/components/modals/models/modal.model';
import * as ClassicEditor from 'src/app/lib/customCkeditor/ckeditor5-build-classic';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { TypeSweetAlertIcon } from 'src/app/lib/data/models';
import Base64UploaderPlugin from 'src/app/lib/@ckeditor/Base64Upload';

@Component({
  selector: 'app-blogs-detail',
  templateUrl: './blogs-detail.component.html',
  styleUrls: ['./blogs-detail.component.scss'],
})
export class BlogsDetailComponent implements OnInit {
  public blogForm: FormGroup;
  public item: any;
  public blog: BlogModel;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  submitted = false;
  public editor = ClassicEditor;
  public editorConfig = {
    extraPlugins: [Base64UploaderPlugin],
  };
  public modalFile: ModalFile;
  public fileURL: (string | ArrayBuffer)[];

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
    private ngbActiveModal: NgbActiveModal,
    private blogService: BlogService,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.BLOG;
  }

  ngOnInit() {
    this.loadFormItem();
    this.createModal();
    if (this.item) {
      this.fileURL = [];
      this.fileURL.push(this.item.imageUrl);
    }
  }
  loadFormItem() {
    this.blogForm = this.formBuilder.group({
      title: [this.item ? this.item.title : '', Validators.required],
      shortDes: [this.item ? this.item.shortDes : '', Validators.required],
      contentHTML: [
        this.item ? this.item.contentHTML : '',
        Validators.required,
      ],
      imageUrl: [this.item ? this.item.imageUrl : '', Validators.required],
    });
  }

  createModal() {
    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = this.item
      ? `Cập nhật ${this.item.title}`
      : `Thêm tin tức`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  saveBlog(event: any) {
    this.blog = {
      title: this.blogForm.controls.title.value.trim(),
      shortDes: this.blogForm.controls.shortDes.value.trim(),
      contentHTML: this.blogForm.controls.contentHTML.value,
      imageUrl: this.blogForm.controls.imageUrl.value,
      id: this.item ? this.item.id : '',
      createdByName: this.item ? this.item.createdByName : '',
    };

    this.submitted = true;

    if (this.blogForm.valid) {
      this.blogService
        .save(this.blog)
        .then(() => {
          this.messageService.notification(
            this.item ? 'Cập nhật thành công' : 'Tạo thành công',
            TypeSweetAlertIcon.SUCCESS
          );
          this.blogForm.reset();
          this.submitted = false;
          this.ngbActiveModal.close();
        })
        .catch((er) => {
          this.messageService.alert(
            er.error.message ??
              JSON.stringify(er.error) ??
              'Mất kết nối với máy chủ',
            TypeSweetAlertIcon.ERROR
          );
        });
    }
  }

  close(event: any) {
    this.ngbActiveModal.dismiss();
  }
  get blogFormControl() {
    return this.blogForm.controls;
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

    this.blogForm.controls.imageUrl.setValue(this.fileURL.join(','));
  }
}
