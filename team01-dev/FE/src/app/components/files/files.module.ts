import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { FileService } from 'src/app/lib/data/services';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateImageComponent } from './create-image/create-image.component';
import { FilesRoutingModule } from './files-routing.module';
import { ListFilesComponent } from './list-files/list-files.component';
@NgModule({
  declarations: [ListFilesComponent, CreateImageComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FilesRoutingModule,
    NgbModule,
    Ng2SmartTableModule,
    SharedModule,
  ],
  providers: [FileService],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
})
export class FilesModule {}
