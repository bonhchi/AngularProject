import { Component, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { CategoryModel } from 'src/app/lib/data/models/categories/category.model';
import { FileService } from 'src/app/lib/data/services';
import { CategoryService } from 'src/app/lib/data/services/categories/category.service';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { CategoryDetailComponent } from '../categories-details/categories-details.component';

@Component({
  selector: 'app-list-categories',
  templateUrl: './list-categories.component.html',
  styleUrls: ['./list-categories.component.scss'],
  providers: [CategoryService, FileService],
})
export class ListCategoriesComponent implements OnInit {
  public categories: CategoryModel[];
  closeResult = '';
  public data: PageModel<CategoryModel>;
  params: any = {};
  constructor(
    private modalService: NgbModal,
    private categoryService: CategoryService,
    private messageService: MessageService
  ) {
    this.fetch();
  }

  public settings = {
    mode: 'external',
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
      noDataMessage: 'Không tìm thấy dữ liệu',
    },
    columns: {
      name: {
        title: 'Tên',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      description: {
        title: 'Mô tả',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
    },
  };

  delete(event: any) {
    this.messageService
      .confirm(`Bạn có muốn xóa danh mục?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          let category = event.data as CategoryModel;
          this.categoryService
            .delete(category)
            .then(() => {
              this.messageService.notification(
                'Danh mục đã được xóa',
                TypeSweetAlertIcon.SUCCESS
              );
              this.fetch();
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
      });
  }

  openPopup(item: any) {
    var modalRef = this.modalService.open(CategoryDetailComponent, {
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
    this.categoryService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<CategoryModel>>) => {
        if (!res.hasError) {
          this.categories = res.data.results;
          this.data = res.data;
        }
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            'Server Disconnected',
          TypeSweetAlertIcon.ERROR
        );
        // if (er.error.hasError) {
        //   // console.log(er.error.message)
        // }
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

  ngOnInit() {}

  onPage(event) {
    this.params.pageIndex = event;
    this.fetch();
  }
}
