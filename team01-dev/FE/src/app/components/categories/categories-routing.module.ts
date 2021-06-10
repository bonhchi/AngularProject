import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryDetailComponent } from './categories-details/categories-details.component';
import { ListCategoriesComponent } from './list-categories/list-categories.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-categories',
        component: ListCategoriesComponent,
        data: {
          title: 'Danh mục sản phẩm',
          breadcrumb: 'Danh sách danh mục',
        },
      },
      {
        path: 'create-categories',
        component: CategoryDetailComponent,
        data: {
          title: 'Tạo danh mục',
          breadcrumb: 'Tạo danh mục',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoriesRoutingModule {}
