import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListSubcategoriesComponent } from './list-subcategories/list-subcategories.component';
import { SubcategoriesDetailComponent } from './subcategories-detail/subcategories-detail.component';
import { SubcategoryRoutingModule } from './subcategories-routing.module';

@NgModule({
  declarations: [ListSubcategoriesComponent, SubcategoriesDetailComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    NgbModalModule,
    Ng2SmartTableModule,
    SubcategoryRoutingModule,
  ],
})
export class SubcategoryModule {}
