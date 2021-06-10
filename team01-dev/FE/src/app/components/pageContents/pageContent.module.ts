import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListPageContentComponent } from './list-pageContent/list-page-content.component';
import { PageContentDetailComponent } from './pageContent-details/page-content-details.component';
import { PageContentRouting } from './pageContent-routing.module';

@NgModule({
  declarations: [ListPageContentComponent, PageContentDetailComponent],
  imports: [
    CommonModule,
    PageContentRouting,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    NgbModalModule,
    Ng2SmartTableModule,
    CKEditorModule,
  ],
})
export class PageContentModule {}
