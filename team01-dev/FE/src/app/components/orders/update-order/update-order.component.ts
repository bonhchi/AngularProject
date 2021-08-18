import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ReturnMessage, TypeSweetAlertIcon } from 'src/app/lib/data/models';
import {
  OrderDetailModel,
  OrderModel,
} from 'src/app/lib/data/models/orders/order.model';

import { OrderDetailsService } from 'src/app/lib/data/services/orders/order-details.service';
import { OrdersService } from 'src/app/lib/data/services/orders/orders.service';
import { ViewImageCellComponent } from 'src/app/shared/components/viewimagecell/viewimagecell.component';
import {
  ModalFooterModel,
  ModalHeaderModel,
} from 'src/app/shared/components/modals/models/modal.model';
import Swal from 'sweetalert2';
import { CustomViewCellNumberComponent } from 'src/app/shared/components/custom-view-cell-number/custom-view-cell-number.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';

@Component({
  selector: 'app-update-order',
  templateUrl: './update-order.component.html',
  styleUrls: ['./update-order.component.scss'],
  providers: [OrdersService, OrderDetailsService],
})
export class UpdateOrderComponent implements OnInit {
  public orderForm: FormGroup;
  public item: any;
  public order = new OrderModel();
  public modalHeader: ModalHeaderModel;
  public modalFooter: ModalFooterModel;
  submitted = false;
  public orderDetails: OrderDetailModel[];

  constructor(
    private formBuilder: FormBuilder,
    private ngbActiveModal: NgbActiveModal,
    private ordersService: OrdersService,
    private orderDetailsService: OrderDetailsService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.loadFormItem();
    this.createModal();
    this.getOrderDetails();
  }

  public settings = {
    mode: 'external',
    actions: false,
    columns: {
      productImgUrl: {
        title: 'Hình ảnh',
        type: 'custom',
        renderComponent: ViewImageCellComponent,
      },
      productName: {
        title: 'Tên sản phẩm',
      },
      price: {
        title: 'Giá',
        value: 'price',
        type: 'custom',
        renderComponent: CustomViewCellNumberComponent,
      },
      quantity: {
        title: 'Số lượng',
        value: 'quantity',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
      totalAmount: {
        title: 'Tổng cộng',
        value: 'totalAmount',
        type: 'custom',
        renderComponent: CustomViewCellNumberComponent,
      },
    },
  };

  loadFormItem() {
    var check = this.item.status == 'Từ chối';
    this.orderForm = this.formBuilder.group({
      fullName: [
        { value: this.item.fullName, disabled: check },
        Validators.required,
      ],
      address: [
        { value: this.item.address, disabled: check },
        Validators.required,
      ],
      email: [{ value: this.item.email, disabled: check }, Validators.required],
      phone: [{ value: this.item.phone, disabled: check }, Validators.required],
      status: [
        { value: this.item.status, disabled: true },
        Validators.required,
      ],
    });
  }

  createModal() {
    this.modalHeader = new ModalHeaderModel();
    this.modalHeader.title = `Cập nhật đơn hàng`;
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
    if (this.item.status == 'Mới') {
      this.modalFooter.buttons = [
        {
          color: 'btn btn-info',
          title: 'lưu',
          onAction: (event: any) => {
            this.save();
          },
        },
        {
          color: 'btn btn-success',
          title: 'chấp nhận',
          onAction: (event: any) => {
            this.approve();
          },
        },
        {
          color: 'btn btn-primary',
          title: 'từ chối',
          onAction: (event: any) => {
            this.reject();
          },
        },
      ];
    }

    if (this.item.status == 'Chấp nhận') {
      this.modalFooter.buttons = [
        {
          color: 'btn btn-info',
          title: 'lưu',
          onAction: (event: any) => {
            this.save();
          },
        },
        {
          color: 'btn btn-primary',
          title: 'đóng',
          onAction: (event: any) => {
            this.ngbActiveModal.dismiss();
          },
        },
      ];
    }
  }

  get orderFormControl() {
    return this.orderForm.controls;
  }

  loadOrderModel() {
    this.order = {
      fullName: this.orderForm.controls.fullName.value,
      address: this.orderForm.controls.address.value,
      email: this.orderForm.controls.email.value,
      phone: this.orderForm.controls.phone.value,
      status: this.item.status,
      id: this.item.id,
      totalAmount: this.item.totalAmount,
      totalItem: this.item.totalItem,
    };
  }

  save() {
    this.loadOrderModel();

    this.submitted = true;
    if (this.orderForm.valid) {
      this.ordersService
        .update(this.order)
        .then(() => {
          this.messageService.notification(
            'Đơn hàng đã được chỉnh sửa',
            TypeSweetAlertIcon.SUCCESS
          );
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

  approve() {
    this.loadOrderModel();
    this.order.status = 'Chấp nhận';

    this.messageService
      .confirm(`Bạn có muốn chấp nhận đơn hàng?`, 'Có', 'Không', false)
      .then(async (result) => {
        if (result.isConfirmed) {
          this.submitted = true;
          if (this.orderForm.valid) {
            this.ordersService
              .update(this.order)
              .then(() => {
                this.messageService.notification(
                  'Đơn hàng đã được chấp nhận',
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
        }
      });
  }

  reject() {
    this.loadOrderModel();
    this.order.status = 'Từ chối';
    Swal.fire({
      title: `Bạn có muốn từ chối đơn hàng`,
      input: 'text',
      inputPlaceholder: 'Tại sao?',
      showCancelButton: true,
      confirmButtonText: `Xác nhận`,
      icon: 'question',
      cancelButtonText: `Hủy`,
    }).then(async (result) => {
      if (result.isConfirmed) {
        this.submitted = true;
        this.order.note = result.value;
        console.log(result.value);
        if (this.orderForm.valid) {
          this.ordersService
            .update(this.order)
            .then((res) => {
              this.messageService.notification(
                'Đơn hàng đã được từ chối',
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
              // if (er.error.hasError) {
              //   console.log(er.error.message);
              // }
            });
        }
      }
    });
  }

  getOrderDetails() {
    this.orderDetailsService
      .getByOrder(this.item.id, null)
      .then((res: ReturnMessage<OrderDetailModel[]>) => {
        if (!res.hasError) {
          this.orderDetails = res.data;
        }
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            'Mất kết nối với máy chủ',
          TypeSweetAlertIcon.ERROR
        );
        // if (er.error.hasError) {
        //   console.log(er.error.message);
        // }
      });
  }
  close(event: any) {
    this.ngbActiveModal.dismiss();
  }
}
