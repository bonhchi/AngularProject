import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TypeSweetAlertIcon } from 'src/app/lib/data/models';
import { BlogModel } from 'src/app/lib/data/models/blogs/blog.model';
import { PageModel, ReturnMessage } from 'src/app/lib/data/models/common';
import { BlogService } from 'src/app/lib/data/services/blogs/blog.service';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { CustomViewCellComponent } from 'src/app/shared/components/customViewCell/customViewCell.component';
import { ViewImageCellComponent } from 'src/app/shared/components/viewimagecell/viewimagecell.component';
import { BlogsDetailComponent } from '../blogs-detail/blogs-detail.component';

@Component({
  selector: 'app-list-blogs',
  templateUrl: './list-blogs.component.html',
  styleUrls: ['./list-blogs.component.scss'],
})
export class ListBlogsComponent implements OnInit {
  public blogs: BlogModel[];
  public data: PageModel<BlogModel>;
  params: any = {};
  constructor(
    private modalService: NgbModal,
    private blogService: BlogService,
    private datePipe: DatePipe,
    private messageService: MessageService
  ) {}

  getBlogs() {
    this.blogService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<BlogModel>>) => {
        if (!res.hasError) {
          this.blogs = res.data.results;
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

  public settings = {
    mode: 'external',
    actions: {
      position: 'right',
      columnTitle: 'Chức năng',
    },
    noDataMessage: 'Không tìm thấy dữ liệu',
    columns: {
      imageUrl: {
        title: 'Hình ảnh',
        type: 'custom',
        renderComponent: ViewImageCellComponent,
        filter: false,
      },
      title: {
        title: 'Tiêu đề',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      shortDes: {
        title: 'Giới thiệu ngắn',
      },
      createByDate: {
        filter: false,
        title: 'Ngày tạo',
        valuePrepareFunction: (created) => {
          return this.datePipe.transform(new Date(created), 'dd/MM/yyyy');
        },
        type: 'custom',
        renderComponent: CustomViewCellComponent,
      },
    },
  };

  openDetails(event: any) {
    var modalRef = this.modalService.open(BlogsDetailComponent, {
      size: 'lg',
    });
    modalRef.componentInstance.item = event?.data;
    modalRef.result.then(
      () => this.getBlogs(),
      (dismiss) => {}
    );
  }

  delete(event: any) {
    this.messageService
      .confirm(`Bạn có muốn xóa bài báo?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          let blog = event.data as BlogModel;
          this.blogService
            .delete(blog)
            .then(() => {
              this.messageService.notification(
                'Bài báo đã được xóa',
                TypeSweetAlertIcon.SUCCESS
              );
              this.getBlogs();
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
    this.getBlogs();
  }
  ngOnInit() {
    this.params.pageIndex = 0;
    this.getBlogs();
  }
}
