import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { CategoryModel } from 'src/app/lib/data/models/categories/category.model';
import { PageContentModel } from 'src/app/lib/data/models/pageContent/pageContent.model';
import { UserModel } from 'src/app/lib/data/models/users/user.model';
import { FileService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { PageContentService } from 'src/app/lib/data/services/pageContents/pageContent.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { UserService } from '../../../lib/data/services/users/user.service';
import { PageContentDetailComponent } from '../pageContent-details/page-content-details.component';

@Component({
  selector: 'app-list-page-content',
  templateUrl: './list-page-content.component.html',
  styleUrls: ['./list-page-content.component.scss'],
  providers: [PageContentService],
})
export class ListPageContentComponent {
  public pageContents: PageContentModel[];

  constructor(
    private modalService: NgbModal,
    private service: PageContentService,
    private messageService: MessageService
  ) {
    this.getList();
  }

  public settings = {
    mode: 'external',
    pager: {
      display: true,
      perPage: 10,
    },
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
    },
    columns: {
      title: {
        title: 'Tiêu đề',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      shortDes: {
        title: 'Giới thiệu ngắn',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      order: {
        title: 'Thứ tự',
        value: 'order',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
    },
  };

  openPopup(item: any) {
    var modalRef = this.modalService.open(PageContentDetailComponent, {
      size: 'lg',
    });
    if (item) modalRef.componentInstance.item = item.data;

    if (!item) modalRef.componentInstance.item = item as PageContentModel;

    modalRef.result.then(
      (close) => {
        this.getList();
      },
      (dismiss) => {}
    );
  }

  getList() {
    this.service
      .get(null)
      .then((res: ReturnMessage<PageContentModel[]>) => {
        if (!res.hasError) {
          this.pageContents = res.data;
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
        //   // console.log(er.error.message);
        // }
      });
  }

  delete(event: any) {
    if (
      event.data.id == '00000000-0000-0000-0000-000000000001' ||
      event.data.id == '00000000-0000-0000-0000-000000000002' ||
      event.data.id == '00000000-0000-0000-0000-000000000003'
    ) {
      this.messageService.alert(
        'Bạn không được phép thay đổi dữ liệu trên',
        TypeSweetAlertIcon.INFO
      );
      return;
    }
    this.messageService
      .confirm(`Bạn có muốn xóa thông tin trang?`)
      .then((res) => {
        if (res.isConfirmed) {
          let id = event.data.id;
          this.service
            .delete(id)
            .then(() => {
              this.messageService.notification(
                'Thông tin trang đã được xóa',
                TypeSweetAlertIcon.SUCCESS
              );
              this.getList();
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
}
