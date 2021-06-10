import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/shared/shared.module';
import { ErrorRoutingModule } from './error-routing.module';
import { ErrorComponent } from './error/error.component';

@NgModule({
  declarations: [
      ErrorComponent
  ],
  imports: [
    CommonModule,
    ErrorRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    NgbModalModule,
  ],
})
export class ErrorModule {}
