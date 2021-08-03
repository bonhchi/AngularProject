import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListProductsComponent } from './list-products/list-products.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-products',
        component: ListProductsComponent,
        data: {
          title: 'Sản phẩm',
          breadcrumb: 'Danh sách sản phẩm',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductRoutingModule {}
