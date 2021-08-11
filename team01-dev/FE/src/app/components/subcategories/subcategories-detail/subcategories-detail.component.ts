import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CategoryModel,
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { SubcategoryTypeModel } from 'src/app/lib/data/models/subcategories/subcategories-type.model';
import { SubcategoryModel } from 'src/app/lib/data/models/subcategories/subcategories.model';
import { CategoryService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { SubcategoryService } from 'src/app/lib/data/services/subcategories/subcategory.service';
import {
  ModalFooterModel,
  ModalHeaderModel,
} from 'src/app/shared/components/modals/models/modal.model';

@Component({
  selector: 'app-subcategories-detail',
  templateUrl: './subcategories-detail.component.html',
  styleUrls: ['./subcategories-detail.component.scss'],
})
export class SubcategoriesDetailComponent implements OnInit {
  public subcategoryForm: FormGroup;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public subcategory: SubcategoryModel;
  public categories: CategoryModel[];
  public item: SubcategoryModel;
  public subcategoryType: SubcategoryTypeModel[];
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private subcategoryService: SubcategoryService,
    private ngbActiveModal: NgbActiveModal,
    private categoryService: CategoryService,
    private messageService: MessageService
  ) {}

  getCategory() {
    this.categoryService
      .get(null)
      .then((res: ReturnMessage<PageModel<CategoryModel>>) => {
        if (!res.hasError) {
          this.categories = res.data.results.filter(
            (r) => r.isDeleted == false
          );
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

  getSubcategoryType() {
    this.subcategoryService
      .getSubcategoryType(null)
      .then((res: ReturnMessage<SubcategoryTypeModel[]>) => {
        if (!res.hasError) {
          this.subcategoryType = res.data.filter((r) => r.isDeleted == false);
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

  save() {
    if (this.subcategoryForm.invalid) {
      this.messageService.alert(
        'Dữ liệu không hợp lệ\n chắc chắn bạn phải nhập đúng dữ liệu!',
        TypeSweetAlertIcon.ERROR
      );
      this.submitted = true;
      return;
    }
    this.submitted = true;
    this.subcategory = {
      name: this.subcategoryForm.value.name,
      categoryName: this.categories.filter(
        (it) => it.id == this.subcategoryForm.value.category
      )[0].name,
      subcategoryTypeName: this.subcategoryType.filter(
        (it) => it.id == this.subcategoryForm.value.subcategoryType
      )[0].name,
      categoryId: this.subcategoryForm.value.category,
      subcategoryTypeId: this.subcategoryForm.value.subcategoryType,
      id: this.item ? this.item.id : '',
    };
    return this.subcategoryService
      .save(this.subcategory)
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
      });
  }

  loadForm() {
    this.subcategoryForm = this.formBuilder.group({
      name: [
        this.item ? this.item.name : '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(150),
        ],
      ],
      subcategoryType: [
        this.item ? this.item.subcategoryTypeId : '',
        [Validators.required],
      ],
      category: [this.item ? this.item.categoryId : '', [Validators.required]],
    });

    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = this.item
      ? `Cập nhật ${this.item.name}`
      : `Thêm danh mục con`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  close(event: any) {
    this.ngbActiveModal.dismiss();
  }
  ngOnInit() {
    this.getCategory();
    this.getSubcategoryType();
    this.loadForm();
  }
}
