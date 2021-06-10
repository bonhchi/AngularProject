import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SharedModule } from 'src/app/shared/shared.module';
import { ContactDetailComponent } from './contact-details/contact-details.component';
import { ContactRouting } from './contact-routing.module';
import { ListContactComponent } from './list-contact/list-contact.component';

@NgModule({
  declarations: [ListContactComponent, ContactDetailComponent],
  imports: [
    CommonModule,
    ContactRouting,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    NgbModalModule,
    Ng2SmartTableModule,
    CKEditorModule,
  ],
})
export class ContactModule {}
