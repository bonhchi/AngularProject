import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'profile-settings',
        component: ProfileSettingsComponent,
        data: {
          title: 'Thông tin cá nhân',
          breadcrumb: 'Chỉnh sửa thông tin cá nhân',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProfileRoutingModule {}
