import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FileDtoModel, TypeSweetAlertIcon } from 'src/app/lib/data/models';
import { SocialMediaModel } from 'src/app/lib/data/models/social-medias/social-media.model';
import { FileService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { SocialMediaService } from 'src/app/lib/data/services/social-media/social-media.service';
import {
  EntityType,
  ModalFile,
  ModalFooterModel,
  ModalHeaderModel,
  TypeFile,
} from 'src/app/shared/components/modals/models/modal.model';

@Component({
  selector: 'app-social-media-detail',
  templateUrl: './social-media-detail.component.html',
  styleUrls: ['./social-media-detail.component.scss'],
})
export class SocialMediaDetailComponent implements OnInit {
  public socialMediaForm: FormGroup;
  public socialMedia: SocialMediaModel;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public item: any;
  submitted = false;

  public modalFile: ModalFile;
  public fileURL: (String | ArrayBuffer)[];

  constructor(
    private formBuilder: FormBuilder,
    private ngbActiveModal: NgbActiveModal,
    private socialService: SocialMediaService,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.SOCIALMEDIA;
  }

  loadItemForm() {
    this.socialMediaForm = this.formBuilder.group({
      title: [this.item ? this.item.title : '', [Validators.required]],
      link: [this.item ? this.item.link : '', [Validators.required]],
      iconUrl: [this.item ? this.item.iconUrl : '', [Validators.required]],
      displayOrder: [
        this.item ? this.item.displayOrder : '',
        [Validators.required],
      ],
    });
  }

  createModal() {
    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title =
      this.item != null ? `Cập nhật ${this.item.title}` : `Thêm mạng xã hội`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  saveSocialMedia(event: any) {
    this.socialMedia = {
      title: this.socialMediaForm.controls.title.value,
      link: this.socialMediaForm.controls.link.value,
      iconUrl: this.socialMediaForm.controls.iconUrl.value,
      displayOrder: this.socialMediaForm.controls.displayOrder.value,
      id: this.item ? this.item.id : '',
      files: this.modalFile.listFile,
    };

    this.submitted = true;
    console.log(this.item);

    if (this.socialMediaForm.invalid) {
      return;
    }

    this.socialService
      .save(this.socialMedia)
      .then((res) => {
        this.messageService.notification(
          this.item
            ? 'Mạng xã hội đã được cập nhật'
            : 'Mạng xã hội đã được tạo',
          TypeSweetAlertIcon.SUCCESS
        );

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
  get SocialMediaFormControl() {
    return this.socialMediaForm.controls;
  }

  close(event: any) {
    this.ngbActiveModal.dismiss();
  }
  ngOnInit(): void {
    this.loadItemForm();
    this.createModal();

    if (this.item) {
      this.fileURL = [];
      this.fileURL.push(this.item.iconUrl);
    }
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

    this.socialMediaForm.controls.iconUrl.setValue(this.fileURL.join(','));
  }
}
