import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';

import { SharedModule } from 'src/app/shared/shared.module';
import { ListUsersComponent } from './list-users/list-users.component';
import { UserDetailComponent } from './users-details/users-details.component';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
  declarations: [ListUsersComponent, UserDetailComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    NgbModalModule,
    Ng2SmartTableModule,
  ],
})
export class UsersModule {}
