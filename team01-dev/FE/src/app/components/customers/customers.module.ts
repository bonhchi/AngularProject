import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SharedModule } from 'src/app/shared/shared.module';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';
import { CustomerRoutingModule } from './customers-routing.module';
import { ListCustomersComponent } from './list-customers/list-customers.component';

@NgModule({
  declarations: [
    ListCustomersComponent,
    CustomerDetailsComponent,
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    NgbModalModule,
    Ng2SmartTableModule,
    CKEditorModule,
  ],
})
export class CustomerModule {}
