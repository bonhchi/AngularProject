import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListBlogsComponent } from './list-blogs/list-blogs.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-blogs',
        component: ListBlogsComponent,
        data: {
          title: 'Bài báo',
          breadcrumb: 'Danh sách bài báo',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BlogsRoutingModule {}
