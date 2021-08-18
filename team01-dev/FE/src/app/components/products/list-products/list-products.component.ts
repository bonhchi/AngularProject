import { Component, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { ProductModel } from 'src/app/lib/data/models/products/product.model';
import { ProductService } from 'src/app/lib/data/services/products/product.service';
import { ViewImageCellComponent } from 'src/app/shared/components/viewimagecell/viewimagecell.component';
import { CustomViewCellNumberComponent } from 'src/app/shared/components/custom-view-cell-number/custom-view-cell-number.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { ProductDetailsComponent } from '../product-details/product-details.component';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.scss'],
  providers: [ProductService],
})
export class ListProductsComponent implements OnInit {
  public products: ProductModel[];
  public data: PageModel<ProductModel>;
  params: any = {};
  closeResult = '';

  constructor(
    private modalService: NgbModal,
    private productService: ProductService,
    private messageService: MessageService
  ) {}

  public settings = {
    mode: 'external',
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
    },
    columns: {
      imageUrl: {
        title: 'Hình ảnh',
        type: 'custom',
        renderComponent: ViewImageCellComponent,
        filter: false,
      },
      name: {
        title: 'Sản phẩm',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      description: {
        title: 'Chi tiết sản phẩm',
        filter: false,
      },
      categoryName: {
        title: 'Danh mục',
        filter: false,
      },
      price: {
        type: 'custom',
        title: 'Giá',
        renderComponent: CustomViewCellNumberComponent,
        filter: false,
      },
      displayOrder: {
        title: 'Thứ tự',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
        filter: false,
      },
    },
  };

  delete(event: any) {
    let product = event.data as ProductModel;
    this.messageService
      .confirm(`Bạn có muốn xóa sản phẩm?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          this.productService
            .delete(product)
            .then(() => {
              this.messageService.notification(
                'Sản phẩm xóa thành công',
                TypeSweetAlertIcon.SUCCESS
              );
              this.fetch();
            })
            .catch((er) =>
              this.messageService.alert(
                er.error.message ??
                  JSON.stringify(er.error.error) ??
                  'Mất kết nói với máy chủ',
                TypeSweetAlertIcon.ERROR
              )
            );
        }
      });
  }

  openPopup(item: any) {
    var modalRef = this.modalService.open(ProductDetailsComponent, {
      size: 'xl',
    });
    modalRef.componentInstance.item = item?.data;
    return modalRef.result.then(
      () => {
        this.fetch();
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }

  fetch() {
    this.productService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<ProductModel>>) => {
        if (!res.hasError) {
          this.products = res.data.results;
          this.data = res.data;
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

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  ngOnInit(): void {
    this.params.pageIndex = 0;
    this.fetch();
  }

  onPage(event) {
    this.params.pageIndex = event;
    this.fetch();
  }
}
