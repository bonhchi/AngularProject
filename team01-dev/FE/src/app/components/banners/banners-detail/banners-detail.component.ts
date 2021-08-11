import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TypeSweetAlertIcon } from 'src/app/lib/data/models';
import { BannerModel } from 'src/app/lib/data/models/banners/banner.model';
import { BannersService } from 'src/app/lib/data/services/banners/banners.service';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import {
  EntityType,
  ModalFile,
  ModalFooterModel,
  ModalHeaderModel,
  TypeFile,
} from 'src/app/shared/components/modals/models/modal.model';

@Component({
  selector: 'app-banners-detail',
  templateUrl: './banners-detail.component.html',
  styleUrls: ['./banners-detail.component.scss'],
  providers: [],
})
export class BannersDetailComponent implements OnInit {
  public bannersForm: FormGroup;
  public item: any;
  public banner: BannerModel;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  submitted = false;

  public modalFile: ModalFile;
  public fileURL: (string | ArrayBuffer)[];
  constructor(
    private formBuilder: FormBuilder,
    private ngbActiveModal: NgbActiveModal,
    private bannersService: BannersService,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.BANNER;
  }

  ngOnInit() {
    this.loadFormItem();
    this.createModal();
    if (this.item) {
      this.fileURL = [];
      this.item.imageUrl.split(',').forEach((it) => {
        this.fileURL.push(it);
      });
    }
  }
  loadFormItem() {
    this.bannersForm = this.formBuilder.group({
      title: [this.item ? this.item.title : '', Validators.required],
      description: [this.item ? this.item.description : ''],
      link: [this.item ? this.item.link : ''],
      imageUrl: [this.item ? this.item.imageUrl : '', Validators.required],
      displayOrder: [
        this.item ? this.item.displayOrder : '',
        Validators.required,
      ],
    });
  }

  createModal() {
    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title =
      this.item != null ? `Cập nhật ${this.item.title}` : `Thêm bảng quảng cáo`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  get bannersFormControl() {
    return this.bannersForm.controls;
  }

  saveBanner(event: any) {
    this.banner = {
      title: this.bannersForm.controls.title.value,
      description: this.bannersForm.controls.description.value,
      link: this.bannersForm.controls.link.value,
      imageUrl: this.bannersForm.controls.imageUrl.value,
      displayOrder: this.bannersForm.controls.displayOrder.value,
      id: this.item ? this.item.id : '',
      files: this.modalFile.listFile,
    };

    this.submitted = true;

    if (this.bannersForm.valid) {
      this.bannersService
        .save(this.banner)
        .then((res) => {
          this.messageService.notification(
            this.item
              ? 'Bảng quảng cáo đã được chỉnh sửa'
              : 'Bảng quảng cáo đã được thêm',
            TypeSweetAlertIcon.SUCCESS
          );
          this.bannersForm.reset();
          this.submitted = false;
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

    this.bannersForm.controls.imageUrl.setValue(this.fileURL.join(','));
    console.log(this.fileURL);
  }
}
