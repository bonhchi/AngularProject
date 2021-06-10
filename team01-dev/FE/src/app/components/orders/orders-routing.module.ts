import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListOrdersComponent } from './list-order/list-orders.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-orders',
        component: ListOrdersComponent,
        data: {
          title: 'Đơn hàng',
          breadcrumb: 'Quản lý đơn hàng',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrdersRoutingModule {}
