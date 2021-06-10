import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CouponModel } from 'src/app/lib/data/models/coupons/coupon.model';
import { CouponService } from 'src/app/lib/data/services/coupons/coupon.service';
import {
  ModalFooterModel,
  ModalHeaderModel,
} from 'src/app/shared/components/modals/models/modal.model';
import { formatDate } from '@angular/common';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { TypeSweetAlertIcon } from 'src/app/lib/data/models';
@Component({
  selector: 'app-coupon-detail',
  templateUrl: './coupon-detail.component.html',
  styleUrls: ['./coupon-detail.component.scss'],
})
export class CouponDetailComponent implements OnInit {
  public couponForm: FormGroup;
  public coupon: CouponModel;
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  public item: any;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private ngbActiveModal: NgbActiveModal,
    private couponService: CouponService,
    private messageService: MessageService
  ) {}
  loadItemForm() {
    this.couponForm = this.formBuilder.group(
      {
        code: [this.item ? this.item.code : '', Validators.required],
        name: [this.item ? this.item.name : '', Validators.required],
        hasPercent: [this.item?.hasPercent ? true : false],
        value: [this.item ? this.item.value : '', Validators.required],
        startDate: [
          this.item ? formatDate(this.item.startDate, 'yyyy-MM-dd', 'en') : '',
          [Validators.required, this.checkCurrentDay()],
        ],
        endDate: [
          this.item ? formatDate(this.item.endDate, 'yyyy-MM-dd', 'en') : '',
          [Validators.required, this.compareDate('startDate')],
        ],
      },
      { validators: this.checkPercent('hasPercent', 'value') }
    );
  }

  compareDate(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value > control?.parent?.controls[matchTo].value
        ? null
        : { compared: true };
    };
  }

  checkPercent(firstControl: string, secondControl: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const hasPercent = control.get(firstControl)?.value;
      const confirm = control.get(secondControl)?.value;
      if (hasPercent == true) {
        return confirm < 1 || confirm > 100 ? { percent: true } : null;
      }
      return null;
    };
  }

  checkCurrentDay(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const startDate = this.item?.startDate;
      console.log(control?.value);
      const convertDate = new Date(control?.value).getDate();
      const convertMonth = new Date(control?.value).getMonth();
      const convertYear = new Date(control?.value).getFullYear();
      if (startDate == null) {
        return convertDate < new Date().getDate() ||
          convertMonth < new Date().getMonth() ||
          convertYear < new Date().getFullYear()
          ? { currentDate: true }
          : null;
      }
    };
  }

  createModal() {
    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title =
      this.item != null ? `Cập nhật ${this.item.name}` : `Thêm mã giảm giá`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.title = 'Lưu';
  }

  saveCoupon(event: any) {
    this.coupon = {
      code: this.couponForm.controls.code?.value.trim(),
      name: this.couponForm.controls.name?.value.trim(),
      hasPercent: this.couponForm.controls.hasPercent?.value,
      value: this.couponForm.controls.value?.value,
      startDate: this.couponForm.controls.startDate?.value,
      endDate: this.couponForm.controls.endDate?.value,
      id: this.item ? this.item.id : '',
    };

    this.submitted = true;
    if (this.couponForm.valid) {
      this.couponService
        .save(this.coupon)
        .then(() => {
          this.messageService.notification(
            this.item ? 'Cập nhật thành công' : 'Tạo thành công',
            TypeSweetAlertIcon.SUCCESS
          );
          this.couponForm.reset();
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
  get CouponFormControl() {
    return this.couponForm.controls;
  }

  close(event: any) {
    this.ngbActiveModal.dismiss();
  }
  ngOnInit(): void {
    this.loadItemForm();
    this.createModal();
  }
}
