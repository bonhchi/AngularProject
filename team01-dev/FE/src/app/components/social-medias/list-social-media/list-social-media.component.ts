import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { SocialMediaModel } from 'src/app/lib/data/models/social-medias/social-media.model';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { SocialMediaService } from 'src/app/lib/data/services/social-media/social-media.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { ViewImageCellComponent } from 'src/app/shared/components/viewimagecell/viewimagecell.component';
import { SocialMediaDetailComponent } from '../social-media-detail/social-media-detail.component';

@Component({
  selector: 'app-list-social-media',
  templateUrl: './list-social-media.component.html',
  styleUrls: ['./list-social-media.component.scss'],
})
export class ListSocialMediaComponent implements OnInit {
  public socialMedias: SocialMediaModel[];
  public data: PageModel<SocialMediaModel>;
  params: any = {};
  constructor(
    private modalService: NgbModal,
    private socialService: SocialMediaService,
    private messageService: MessageService
  ) {}

  getSocialMedias() {
    this.socialService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<SocialMediaModel>>) => {
        if (!res.hasError) {
          this.socialMedias = res.data.results;
          this.data = res.data;
        }
      })
      .catch((er) => {
        if (er.error.hasError) {
        }
      });
  }

  public settings = {
    mode: 'external',
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
    },
    noDataMessage: 'Không tìm thấy dữ liệu',
    columns: {
      iconUrl: {
        filter: false,
        title: 'Hình ảnh',
        type: 'custom',
        renderComponent: ViewImageCellComponent,
      },
      title: {
        title: 'Tiêu đề',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      link: {
        title: 'Đường dẫn',
        type: 'html',
        valuePrepareFunction: (value) => {
          return `<a href='${value}' target="_blank">${value}</a>`;
        },
      },
      displayOrder: {
        title: 'Thứ tự',
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
    },
  };

  open(event: any) {
    var modalRef = this.modalService.open(SocialMediaDetailComponent, {
      size: 'lg',
    });
    if (event) {
      modalRef.componentInstance.item = event?.data;
    }
    modalRef.result.then(
      () => this.getSocialMedias(),
      (dismiss) => {}
    );
  }

  delete(event: any) {
    this.messageService
      .confirm(`Bạn có muốn xóa mạng xã hội?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          let socialMedia = event.data as SocialMediaModel;
          this.socialService
            .delete(socialMedia)
            .then(() => {
              this.messageService.notification(
                'Mạng xã hội đã được xóa',
                TypeSweetAlertIcon.SUCCESS
              );
              this.getSocialMedias();
            })
            .catch((er) =>
              this.messageService.alert(
                er.error.message ??
                  JSON.stringify(er.error.error) ??
                  'Mất kết nối với máy chủ',
                TypeSweetAlertIcon.ERROR
              )
            );
        }
      });
  }

  onPage(event) {
    this.params.pageIndex = event;
    this.getSocialMedias();
  }
  ngOnInit(): void {
    this.params.pageIndex = 0;
    this.getSocialMedias();
  }
}
