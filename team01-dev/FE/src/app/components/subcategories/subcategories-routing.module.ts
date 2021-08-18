import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListSubcategoriesComponent } from './list-subcategories/list-subcategories.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-subcategories',
        component: ListSubcategoriesComponent,
        data: {
          title: 'Danh mục con',
          breadcrumb: 'Danh sách danh mục con',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SubcategoryRoutingModule {}
