import { Injectable, HostListener, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { WINDOW } from './windows.service';
// Menu
export interface Menu {
  path?: string;
  title?: string;
  icon?: string;
  type?: string;
  badgeType?: string;
  badgeValue?: string;
  active?: boolean;
  bookmark?: boolean;
  children?: Menu[];
}

@Injectable({
  providedIn: 'root',
})
export class NavService {
  public screenWidth: any;
  public collapseSidebar: boolean = false;

  constructor(@Inject(WINDOW) private window) {
    this.onResize();
    if (this.screenWidth < 991) {
      this.collapseSidebar = true;
    }
  }

  // Windows width
  @HostListener('window:resize', ['$event'])
  onResize(event?) {
    this.screenWidth = window.innerWidth;
  }

  MENUITEMS: Menu[] = [
    {
      title: 'Quản lý trang',
      icon: 'clipboard',
      type: 'sub',
      active: false,
      children: [
        {
          path: '/blogs/list-blogs',
          title: 'Bài báo',
          type: 'link',
        },
        {
          path: '/page-content/list-page-content',
          title: 'Thông tin trang',
          type: 'link',
        },
      ],
    },
    {
      title: 'Sản phẩm',
      icon: 'package',
      type: 'sub',
      active: false,
      children: [
        {
          path: '/products/list-products',
          title: 'Sản phẩm',
          type: 'link',
        },
        {
          path: '/orders/list-orders',
          title: 'Đơn hàng',
          type: 'link',
        },
        {
          path: '/categories/list-categories',
          title: 'Danh mục',
          type: 'link',
        },
        {
          path: '/coupons/list-coupons',
          title: 'Mã giảm giá',
          type: 'link',
        },
      ],
    },
    {
      title: 'Admin & Khách hàng',
      icon: 'users',
      type: 'sub',
      active: false,
      children: [
        {
          path: '/users/list-users',
          title: 'Admin',
          type: 'link',
        },
        {
          path: '/customers/list-customers',
          title: 'Khách hàng',
          type: 'link',
        },
        {
          path: '/contact/list-contact',
          title: 'Liên hệ',
          type: 'link',
        },
      ],
    },
    {
      title: 'Cài đặt',
      icon: 'settings',
      type: 'sub',
      active: false,
      children: [
        {
          path: '/banners/list-banners',
          title: 'Bảng quảng cáo',
          type: 'link',
        },
        {
          path: '/social-medias/list-social-medias',
          title: 'Mạng xã hội',
          type: 'link',
        },
        {
          path: '/information-website/list-information-website',
          title: 'Thông tin Website',
          type: 'link',
        },
      ],
    },
  ];

  items = new BehaviorSubject<Menu[]>(this.MENUITEMS);
}
