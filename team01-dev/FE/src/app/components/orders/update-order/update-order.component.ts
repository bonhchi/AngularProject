import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
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
        title: 'H??nh ???nh',
        type: 'custom',
        renderComponent: ViewImageCellComponent,
      },
      productName: {
        title: 'T??n s???n ph???m',
      },
      price: {
        title: 'Gi??',
        value: 'price',
        type: 'custom',
        renderComponent: CustomViewCellNumberComponent,
      },
      quantity: {
        title: 'S??? l?????ng',
        value: 'quantity',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
      totalAmount: {
        title: 'T???ng c???ng',
        value: 'totalAmount',
        type: 'custom',
        renderComponent: CustomViewCellNumberComponent,
      },
    },
  };

  loadFormItem() {
    var check = this.item.status == 'T??? ch???i';
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
    this.modalHeader.title = `C???p nh???t ????n h??ng`;
    this.modalFooter = new ModalFooterModel();
    this.modalFooter.buttons = [
      {
        color: 'btn btn-primary',
        title: '????ng',
        onAction: (event: any) => {
          this.ngbActiveModal.dismiss();
        },
      },
    ];
    if (this.item.status == 'M???i') {
      this.modalFooter.buttons = [
        {
          color: 'btn btn-info',
          title: 'l??u',
          onAction: (event: any) => {
            this.save();
          },
        },
        {
          color: 'btn btn-success',
          title: 'ch???p nh???n',
          onAction: (event: any) => {
            this.approve();
          },
        },
        {
          color: 'btn btn-primary',
          title: 't??? ch???i',
          onAction: (event: any) => {
            this.reject();
          },
        },
      ];
    }

    if (this.item.status == 'Ch???p nh???n') {
      this.modalFooter.buttons = [
        {
          color: 'btn btn-info',
          title: 'l??u',
          onAction: (event: any) => {
            this.save();
          },
        },
        {
          color: 'btn btn-primary',
          title: '????ng',
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
            '????n h??ng ???? ???????c ch???nh s???a',
            TypeSweetAlertIcon.SUCCESS
          );
        })
        .catch((er) => {
          this.messageService.alert(
            er.error.message ??
              JSON.stringify(er.error.error) ??
              'M???t k???t n???i v???i m??y ch???',
            TypeSweetAlertIcon.ERROR
          );
        });
    }
  }

  approve() {
    this.loadOrderModel();
    this.order.status = 'Ch???p nh???n';

    this.messageService
      .confirm(`B???n c?? mu???n ch???p nh???n ????n h??ng?`, 'C??', 'Kh??ng', false)
      .then(async (result) => {
        if (result.isConfirmed) {
          this.submitted = true;
          if (this.orderForm.valid) {
            this.ordersService
              .update(this.order)
              .then(() => {
                this.messageService.notification(
                  '????n h??ng ???? ???????c ch???p nh???n',
                  TypeSweetAlertIcon.SUCCESS
                );
                this.ngbActiveModal.close();
              })
              .catch((er) => {
                this.messageService.alert(
                  er.error.message ??
                    JSON.stringify(er.error.error) ??
                    'M???t k???t n???i v???i m??y ch???',
                  TypeSweetAlertIcon.ERROR
                );
              });
          }
        }
      });
  }

  reject() {
    this.loadOrderModel();
    this.order.status = 'T??? ch???i';
    Swal.fire({
      title: `B???n c?? mu???n t??? ch???i ????n h??ng`,
      input: 'text',
      inputPlaceholder: 'T???i sao?',
      showCancelButton: true,
      confirmButtonText: `X??c nh???n`,
      icon: 'question',
      cancelButtonText: `H???y`,
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
                '????n h??ng ???? ???????c t??? ch???i',
                TypeSweetAlertIcon.SUCCESS
              );
              this.ngbActiveModal.close();
            })
            .catch((er) => {
              this.messageService.alert(
                er.error.message ??
                  JSON.stringify(er.error.error) ??
                  'M???t k???t n???i v???i m??y ch???',
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
            'M???t k???t n???i v???i m??y ch???',
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
