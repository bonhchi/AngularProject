import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListCouponComponent } from './list-coupon/list-coupon.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-coupons',
        component: ListCouponComponent,
        data: {
          title: 'Mã giảm giá',
          breadcrumb: 'Danh sách mã giảm giá',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CouponsRoutingModule {}
