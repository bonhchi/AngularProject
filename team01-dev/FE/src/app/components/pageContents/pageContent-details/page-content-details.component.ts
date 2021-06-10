import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PageContentModel } from 'src/app/lib/data/models/pageContent/pageContent.model';
import { PageContentService } from 'src/app/lib/data/services/pageContents/pageContent.service';
// import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import {
  EntityType,
  ModalFile,
  ModalFooterModel,
  ModalHeaderModel,
  TypeFile,
} from 'src/app/shared/components/modals/models/modal.model';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { TypeSweetAlertIcon } from 'src/app/lib/data/models';
import * as ClassicEditor from 'src/app/lib/customCkeditor/ckeditor5-build-classic';
// import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import Base64UploaderPlugin from 'src/app/lib/@ckeditor/Base64Upload';

@Component({
  selector: 'app-page-content-details',
  templateUrl: './page-content-details.component.html',
  styleUrls: ['./page-content-details.component.scss'],
  providers: [PageContentService],
})
export class PageContentDetailComponent implements OnInit {
  public pageContentForm: FormGroup;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public pageContent: PageContentModel;
  public modalFile: ModalFile;
  public fileURL: (string | ArrayBuffer)[];
  submitted = false;
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
    private pageContentService: PageContentService,
    private ngbActiveModal: NgbActiveModal,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.BANNER;
  }

  ngOnInit() {
    this.loadItem();
    if (this.item?.imageUrl) {
      this.fileURL = [];
      this.item.imageUrl.split(',').forEach((it) => {
        this.fileURL.push(it);
      });
    }
  }

  loadItem() {
    this.pageContentForm = this.formBuilder.group({
      title: [this.item ? this.item.title : '', Validators.required],
      shortDes: [this.item ? this.item.shortDes : ''],
      imageUrl: [this.item ? this.item.imageUrl : ''],
      order: [this.item ? this.item.order : 0, Validators.required],
      description: [
        this.item ? this.item.description : '',
        Validators.required,
      ],
    });

    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = this.item ? `Cập nhật` : `Thêm`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  get pageContentFormControl() {
    return this.pageContentForm.controls;
  }

  save() {
    this.submitted = true;
    if (this.pageContentForm.invalid) {
      return;
    }

    this.pageContent = {
      id: this.item ? this.item.id : '',
      title: this.pageContentForm.value.title.trim(),
      description: this.pageContentForm.value.description,
      imageUrl: this.pageContentForm.controls.imageUrl.value,
      shortDes: this.pageContentForm.value.shortDes.trim(),
      order: this.pageContentForm.value.order,
      files: this.modalFile.listFile,
    };

    this.callServiceToSave();
  }

  callServiceToSave() {
    this.pageContentService
      .saveChange(this.pageContent)
      .then(() => {
        this.messageService.notification(
          this.item ? 'Cập nhật thành công' : 'Tạo thành công',
          TypeSweetAlertIcon.SUCCESS
        );
        this.pageContentForm.reset();
        this.submitted = false;
        this.ngbActiveModal.close();
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            'Server Disconnected',
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

    this.pageContentForm.controls.imageUrl.setValue(this.fileURL.join(','));
  }
}
