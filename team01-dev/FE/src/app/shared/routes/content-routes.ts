import { Routes } from '@angular/router';

export const content: Routes = [
  {
    path: 'banners',
    loadChildren: () =>
      import('../../components/banners/banners.module').then(
        (m) => m.BannersModule
      ),
    data: {
      breadcrumb: 'Bảng quảng cáo',
    },
  },
  {
    path: 'profiles',
    loadChildren: () =>
      import('../../components/profiles/profiles.module').then(
        (m) => m.ProfilesModule
      ),
    data: {
      breadcrumb: 'Thông tin cá nhân',
    },
  },
  {
    path: 'social-medias',
    loadChildren: () =>
      import('../../components/social-medias/social-medias.module').then(
        (m) => m.SocialMediasModule
      ),
    data: {
      breadcrumb: 'Mạng xã hội',
    },
  },
  {
    path: 'categories',
    loadChildren: () =>
      import('../../components/categories/categories.module').then(
        (m) => m.CategoriesModule
      ),
    data: {
      breadcrumb: 'Danh mục',
    },
  },
  {
    path: 'files',
    loadChildren: () =>
      import('../../components/files/files.module').then((m) => m.FilesModule),
  },
  {
    path: 'coupons',
    loadChildren: () =>
      import('../../components/coupons/coupons.module').then(
        (m) => m.CouponsModule
      ),
    data: {
      breadcrumb: 'Mã giảm giá',
    },
  },
  {
    path: 'users',
    loadChildren: () =>
      import('../../components/users/users.module').then((m) => m.UsersModule),
    data: {
      breadcrumb: 'Admin',
    },
  },
  {
    path: 'products',
    loadChildren: () =>
      import('../../components/products/products.module').then(
        (m) => m.ProductModule
      ),
    data: {
      breadcrumb: 'Sản phẩm',
    },
  },
  {
    path: 'blogs',
    loadChildren: () =>
      import('../../components/blogs/blogs.module').then((m) => m.BlogsModule),
    data: {
      breadcrumb: 'Bài báo',
    },
  },
  {
    path: 'orders',
    loadChildren: () =>
      import('../../components/orders/orders.module').then(
        (m) => m.OrdersModule
      ),
    data: {
      breadcrumb: 'Đơn hàng',
    },
  },
  {
    path: 'page-content',
    loadChildren: () =>
      import('../../components/pageContents/pageContent.module').then(
        (m) => m.PageContentModule
      ),
    data: {
      breadcrumb: 'Thông tin trang',
    },
  },
  {
    path: 'information-website',
    loadChildren: () =>
      import('../../components/information-website/information.module').then(
        (m) => m.InformationWebsiteModule
      ),
    data: {
      breadcrumb: 'Thông tin website',
    },
  },
  {
    path: 'customers',
    loadChildren: () =>
      import('../../components/customers/customers.module').then(
        (m) => m.CustomerModule
      ),
    data: {
      breadcrumb: 'Khách hàng',
    },
  },
  {
    path: 'contact',
    loadChildren: () =>
      import('../../components/contacts/contact.module').then(
        (m) => m.ContactModule
      ),
    data: {
      breadcrumb: 'Liên hệ',
    },
  },
  {
    path: 'error',
    loadChildren: () =>
      import('../../components/error/error.module').then((m) => m.ErrorModule),
  },
];
