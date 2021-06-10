import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PageModel, TypeSweetAlertIcon } from 'src/app/lib/data/models';
import { ReturnMessage } from 'src/app/lib/data/models/common/return-message.model';
import { ContactModel } from 'src/app/lib/data/models/contact/contact.model';
import { PageContentModel } from 'src/app/lib/data/models/pageContent/pageContent.model';
import { ContactService } from 'src/app/lib/data/services/contacts/contact.service';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { PageContentService } from 'src/app/lib/data/services/pageContents/pageContent.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { ContactDetailComponent } from '../contact-details/contact-details.component';

@Component({
  selector: 'app-list-page-content',
  templateUrl: './list-contact.component.html',
  styleUrls: ['./list-contact.component.scss'],
  providers: [ContactService, DatePipe],
})
export class ListContactComponent {
  public contacts: ContactModel[];
  public data: PageModel<ContactModel>;
  params: any = {};

  constructor(
    private modalService: NgbModal,
    private contactService: ContactService,
    private messageService: MessageService,
    private datePipe: DatePipe
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
      add: false,
      delete: false,
      columnTitle: 'Chức năng',
    },
    noDataMessage: 'Không tìm thấy dữ liệu',
    columns: {
      firstName: {
        title: 'Tên',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      lastName: {
        title: 'Họ',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      phoneNumber: {
        title: 'Số điện thoại',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
      email: { title: 'Email' },
      status: { title: 'Trạng thái' },
      createByDate: {
        title: 'Ngày gửi',
        valuePrepareFunction: (date) => {
          return this.datePipe.transform(new Date(date), 'dd/MM/yyyy');
        },
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
    },
  };

  openPopup(item: any) {
    var modalRef = this.modalService.open(ContactDetailComponent, {
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

  delete(event: any) {
    this.messageService
      .confirm(`Bạn có muốn xóa liên hệ?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          let contact = event.data as ContactModel;
          this.contactService.delete(contact).then(() => {
            this.messageService.notification(
              'Contact has been deleted',
              TypeSweetAlertIcon.SUCCESS
            );
            this.getList();
          });
        }
      });
  }

  getList() {
    this.contactService
      .getList(null)
      .then((res: ReturnMessage<ContactModel[]>) => {
        if (!res.hasError) {
          this.contacts = res.data;
          // console.log('contact', res.data);
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

  onPage(event) {
    this.params.pageIndex = event;
    this.getList();
  }
}
