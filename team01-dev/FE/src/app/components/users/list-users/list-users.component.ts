import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import {
  PageModel,
  ReturnMessage,
  TypeSweetAlertIcon,
  UserDataReturnDTOModel,
} from 'src/app/lib/data/models';
import { UserModel } from 'src/app/lib/data/models/users/user.model';
import { AuthService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { CustomViewCellStringComponent } from 'src/app/shared/components/custom-view-cell-string/custom-view-cell-string.component';
import { ViewImageCellComponent } from 'src/app/shared/components/viewimagecell/viewimagecell.component';
import { UserDetailComponent } from '../users-details/users-details.component';
import { UserService } from './../../../lib/data/services/users/user.service';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss'],
  providers: [UserService],
})
export class ListUsersComponent implements OnInit, OnDestroy {
  public users: UserModel[];
  closeResult = '';
  public data: PageModel<UserModel>;
  params: any = {};
  userInfo: UserDataReturnDTOModel;
  subUser: Subscription;
  constructor(
    private modalService: NgbModal,
    private userService: UserService,
    private messageService: MessageService,
    private authService: AuthService
  ) {
    this.getList();
  }
  ngOnDestroy(): void {
    this.subUser.unsubscribe();
    this.subUser = null;
  }
  ngOnInit(): void {
    this.subUser = this.authService.callUserInfo.subscribe(
      (res) => (this.userInfo = res)
    );
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
    noDataMessage: 'Không tìm thấy dữ liệu',
    columns: {
      imageUrl: {
        filter: false,
        title: 'Hình ảnh',
        type: 'custom',
        renderComponent: ViewImageCellComponent,
      },
      username: {
        title: 'Tên đăng nhập',
        type: 'custom',
        renderComponent: CustomViewCellStringComponent,
      },
      email: {
        title: 'Email',
      },
      firstName: {
        title: 'Tên',
      },
      lastName: {
        title: 'Họ',
      },
    },
  };

  delete(event: any) {
    if (
      event.data.id == '10000000-0000-0000-0000-000000000000' ||
      event.data.id == this.userInfo.id
    ) {
      this.messageService.alert(
        'Bạn không được phép thay đổi dữ liệu trên',
        TypeSweetAlertIcon.INFO
      );
      return;
    }
    this.messageService
      .confirm(`Bạn có muốn xóa admin?`, 'Có', 'Không')
      .then((res) => {
        if (res.isConfirmed) {
          let user = event.data as UserModel;
          this.userService
            .delete(user)
            .then(() => {
              this.messageService.notification(
                'Admin đã được xóa',
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

  openPopup(item: any) {
    if (
      '10000000-0000-0000-0000-000000000000' != this.userInfo.id &&
      item?.data.id == '10000000-0000-0000-0000-000000000000'
    ) {
      this.messageService.alert(
        'Bạn không được phép thay đổi dữ liệu trên',
        TypeSweetAlertIcon.INFO
      );
      return;
    }
    var modalRef = this.modalService.open(UserDetailComponent, {
      size: 'lg',
    });
    if (item) modalRef.componentInstance.item = item.data;

    modalRef.result.then(
      (close) => {
        this.getList();
      },
      (dismiss) => {}
    );
  }

  getList() {
    this.userService
      .get({ params: this.params })
      .then((res: ReturnMessage<PageModel<UserModel>>) => {
        if (!res.hasError) {
          this.users = res.data.results;
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
