import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { content } from './shared/routes/content-routes';
import { ContentLayoutComponent } from './shared/layout/content-layout/content-layout.component';
import { LoginComponent } from './components/auth/login/login.component';
import { AuthGuardsAdminService } from './lib/guards/auth.guard';
import { RouterHelperService } from './lib/helpers';
import { AuthModule } from './components/auth/auth.module';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'profiles/profile-settings',
    pathMatch: 'full',
  },
  {
    path: '',
    component: ContentLayoutComponent,
    children: content,
    canActivate: [AuthGuardsAdminService],
  },
  {path: '**', redirectTo: '/error'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled',
      relativeLinkResolution: 'legacy',
    }),
    AuthModule,
  ],
  exports: [RouterModule],
  providers: [AuthGuardsAdminService, RouterHelperService],
})
export class AppRoutingModule {}
