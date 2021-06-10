import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TypeSweetAlertIcon } from 'src/app/lib/data/models';
import { CategoryModel } from 'src/app/lib/data/models/categories/category.model';
import { CategoryService } from 'src/app/lib/data/services/categories/category.service';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';

import {
  EntityType,
  ModalFile,
  ModalFooterModel,
  ModalHeaderModel,
  TypeFile,
} from 'src/app/shared/components/modals/models/modal.model';
@Component({
  selector: 'app-categories-details',
  templateUrl: './categories-details.component.html',
  styleUrls: ['./categories-details.component.scss'],
  providers: [CategoryService],
})
export class CategoryDetailComponent implements OnInit {
  public categoriesForm: FormGroup;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public category: CategoryModel;
  public modalFile: ModalFile;
  public fileURL: (String | ArrayBuffer)[];
  public item: any;
  public regex: string =
    '^[a-z|A-Z|ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ|0-9 ,-.]*$';
  submitted = false;

  ngOnChanges(changes: SimpleChanges): void {}

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private ngbActiveModal: NgbActiveModal,
    private messageService: MessageService
  ) {
    this.modalFile = new ModalFile();
    this.modalFile.typeFile = TypeFile.IMAGE;
    this.modalFile.multiBoolean = false;
    this.modalFile.entityType = EntityType.CATEGORY;
  }

  get categoryFormsControl() {
    return this.categoriesForm.controls;
  }
  save() {
    if (this.categoriesForm.invalid) {
      this.messageService.alert(
        'Dữ liệu không hợp lệ\n chắc chắn bạn phải nhập đúng dữ liệu!',
        TypeSweetAlertIcon.ERROR
      );
      return;
    }
    this.submitted = true;
    this.category = {
      name: this.categoriesForm.value.name,
      description: this.categoriesForm.value.description,
      imageUrl: this.categoriesForm.value.imageUrl,
      createdBy: this.item ? this.item.createdBy : this.item,
      createdByName: this.item ? this.item.createdByName : this.item,
      deletedBy: this.item ? this.item.deletedBy : this.item,
      deletedByName: this.item ? this.item.deletedByName : this.item,
      isActive: this.item ? this.item.isActive : this.item,
      isDeleted: this.item ? this.item.isDeleted : this.item,
      updatedBy: this.item ? this.item.updatedBy : this.item,
      updatedByName: this.item ? this.item.updatedByName : this.item,
      files: this.modalFile.listFile,
      id: this.item ? this.item.id : '',
    };
    return this.categoryService
      .save(this.category)
      .then(() => {
        this.messageService.notification(
          this.item ? 'Cập nhật thành công' : 'Tạo thành công',
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
        console.log(er);
      });
  }

  loadItem() {
    this.categoriesForm = this.formBuilder.group({
      name: [
        this.item ? this.item.name : '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(50),
          // Validators.pattern(this.regex),
        ],
      ],
      description: [
        this.item ? this.item.description : '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
          // Validators.pattern(this.regex),
        ],
      ],
      imageUrl: [this.item ? this.item.imageUrl : ''],
    });

    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = this.item
      ? `Cập nhật ${this.item.name}`
      : `Thêm danh mục`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  close(event: any) {
    this.ngbActiveModal.dismiss();
  }

  ngOnInit() {
    this.loadItem();
    if (this.item) {
      this.fileURL = [];
      this.fileURL.push(this.item.imageUrl);
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

    this.categoriesForm.controls.imageUrl.setValue(this.fileURL.join(','));
  }
}
