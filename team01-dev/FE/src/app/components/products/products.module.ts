import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { CKEditorModule } from "@ckeditor/ckeditor5-angular";
import { NgbModalModule, NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { Ng2SmartTableModule } from "ng2-smart-table";
import { SharedModule } from "src/app/shared/shared.module";
import { ListProductsComponent } from "./list-products/list-products.component";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ProductRoutingModule } from "./products-routing.module";

@NgModule({
    declarations: [ListProductsComponent, ProductDetailsComponent],
    imports: [
      CommonModule,
      ProductRoutingModule,
      ReactiveFormsModule,
      NgbModule,
      SharedModule,
      NgbModalModule,
      Ng2SmartTableModule,
      CKEditorModule
    ]
  })
  export class ProductModule { }