import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';
import { ListCustomersComponent } from './list-customers/list-customers.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-customers',
        component: ListCustomersComponent,
        data: {
          title: 'Khách hàng',
          breadcrumb: 'Danh sách khách hàng',
        },
      },
      {
        path: 'create-customers',
        component: CustomerDetailsComponent,
        data: {
          title: 'Tạo khách hàng',
          breadcrumb: 'Tạo khách hàng',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
