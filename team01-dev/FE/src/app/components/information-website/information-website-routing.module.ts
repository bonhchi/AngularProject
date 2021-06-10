import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListInformationWebsiteComponent } from './list-information-website/list-information-website.component';
const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'list-information-website',
        component: ListInformationWebsiteComponent,
        data: {
          title: 'Thông tin website',
          breadcrumb: 'Chi tiết thông tin',
        },
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InformationWebsiteRoutingModule {}
