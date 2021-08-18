import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { OrderModel } from 'src/app/lib/data/models/orders/order.model';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { OrdersService } from 'src/app/lib/data/services/orders/orders.service';
import { CustomViewCellNumberComponent } from 'src/app/shared/components/custom-view-cell-number/custom-view-cell-number.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { UpdateOrderComponent } from '../update-order/update-order.component';

@Component({
  selector: 'app-list-orders',
  templateUrl: './list-orders.component.html',
  styleUrls: ['./list-orders.component.scss'],
  providers: [OrdersService, DatePipe],
})
export class ListOrdersComponent implements OnInit {
  public orders: OrderModel[];
  public filter: string = '';
  params: any = {};
  public data: PageModel<OrderModel>;

  isGetOrders: boolean = false;

  constructor(
    private modalService: NgbModal,
    private ordersService: OrdersService,
    private datePipe: DatePipe,
    private messageService: MessageService
  ) {
    this.params.pageIndex = 0;
  }
  ngOnInit() {
    this.actionFilter('Mới');
  }

  public settings = {
    mode: 'external',
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
      add: false,
      delete: false,
    },
    noDataMessage: 'Không tìm thấy dữ liệu',
    columns: {
      code: {
        title: 'Mã',
      },
      createByDate: {
        filter: false,
        title: 'Ngày tạo',
        valuePrepareFunction: (date) => {
          return this.datePipe.transform(new Date(date), 'dd/MM/yyyy');
        },
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
      fullName: {
        title: 'Tên đầy đủ',
      },
      address: {
        title: 'Địa chỉ',
      },
      phone: {
        title: 'Số điện thoại',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
      email: {
        title: 'Email',
      },
      status: {
        filter: false,
        title: 'Trạng thái',
      },
      note: {
        filter: false,
        title: 'Ghi chú',
      },
      hasCoupon: {
        filter: false,
        title: 'Áp dụng coupon',
        valuePrepareFunction: (cell, row) => {
          return cell ? row.couponName : '';
        },
      },
      totalAmount: {
        filter: false,
        title: 'Tổng cộng',
        value: 'totalAmount',
        type: 'custom',
        renderComponent: CustomViewCellNumberComponent,
      },
    },
  };

  getOrders() {
    this.isGetOrders = false;
    this.ordersService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<OrderModel>>) => {
        if (!res.hasError) {
          this.data = res.data;
          this.orders = res.data.results;
          this.orders.forEach((order) => {
            order.hasCoupon = false;
            if (order.couponCode) {
              order.hasCoupon = true;
            }
          });
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

  openUpdate(event: any) {
    var modalRef = this.modalService.open(UpdateOrderComponent, {
      size: 'lg',
    });
    modalRef.componentInstance.item = event?.data;
    modalRef.result.then(
      () => this.getOrders(),
      (dismiss) => {}
    );
  }

  statusFilter(button: HTMLElement) {
    this.isGetOrders = true;
    let status =
      button.innerText.toUpperCase() !== 'Tất cả'.toUpperCase()
        ? button.innerText
        : '';
    button.classList.add('active');

    if (this.filter == status || status == '') {
      this.filter = '';
      this.params.pageIndex = 0;
      return this.getOrders();
    }
    this.filter = status;

    return this.actionFilter(status);
  }

  removeAllAction(...left: HTMLElement[]) {
    left.forEach((i) => i.classList.remove('active'));
  }

  actionFilter(status: string) {
    this.ordersService
      .getByStatus(null, status)
      .then((response) => {
        this.orders = response.data ? response.data : [];
        this.orders.forEach((order) => {
          order.hasCoupon = false;
          if (order.couponCode) {
            order.hasCoupon = true;
          }
        });
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            'Mất kết nối với máy chủ',
          TypeSweetAlertIcon.ERROR
        );
        // if (er.error.hasError) {
        // }
      });
  }
  onPage(event) {
    this.params.pageIndex = event;
    this.getOrders();
  }
}
