import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListFilesComponent } from './list-files/list-files.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-files',
        component: ListFilesComponent,
        data: {
          title: 'Thư mục',
          breadcrumb: 'Danh sách thư mục',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FilesRoutingModule {}
