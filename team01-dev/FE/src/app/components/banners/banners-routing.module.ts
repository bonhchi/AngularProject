import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListBannersComponent } from './list-banners/list-banners.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-banners',
        component: ListBannersComponent,
        data: {
          title: 'Bảng quảng cáo',
          breadcrumb: 'Danh sách bảng quảng cáo',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BannersRoutingModule {}
