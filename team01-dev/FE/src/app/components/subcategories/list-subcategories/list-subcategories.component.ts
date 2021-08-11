import { Component, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { SubcategoryService } from 'src/app/lib/data/services/subcategories/subcategory.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { SubcategoryModel } from '../../../lib/data/models/subcategories/subcategories.model';
import { SubcategoriesDetailComponent } from '../subcategories-detail/subcategories-detail.component';

@Component({
  selector: 'app-list-subcategories',
  templateUrl: './list-subcategories.component.html',
  styleUrls: ['./list-subcategories.component.scss'],
  providers: [SubcategoryService],
})
export class ListSubcategoriesComponent implements OnInit {
  public subcategories: SubcategoryModel[];
  public data: PageModel<SubcategoryModel>;
  params: any = {};
  closeResult = '';

  constructor(
    private modalService: NgbModal,
    private subcategoryService: SubcategoryService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.params.pageIndex = 0;
    this.getSubcategories();
  }

  public settings = {
    mode: 'external',
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
    },
    columns: {
      name: {
        title: 'Tên danh mục con',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      categoryName: {
        title: 'Tên danh mục',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      subcategoryTypeName: {
        title: 'Loại danh mục con',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
    },
  };

  delete(event: any) {
    let subcategory = event.data as SubcategoryModel;
    this.messageService
      .confirm(`Bạn có muốn xóa sản phẩm?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          this.subcategoryService
            .delete(subcategory)
            .then(() => {
              this.messageService.notification(
                'Danh mục con xóa thành công',
                TypeSweetAlertIcon.SUCCESS
              );
              this.getSubcategories();
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

  getSubcategories() {
    this.subcategoryService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<SubcategoryModel>>) => {
        if (!res.hasError) {
          this.subcategories = res.data.results;
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

  openPopup(item: any) {
    var modalRef = this.modalService.open(SubcategoriesDetailComponent, {
      size: 'lg',
    });
    modalRef.componentInstance.item = item?.data;
    return modalRef.result.then(
      () => {
        this.getSubcategories();
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
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

  onPage(event) {
    this.params.pageIndex = event;
    this.getSubcategories();
  }
}
