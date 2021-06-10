import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { CKEditorModule } from "@ckeditor/ckeditor5-angular";
import { NgbModalModule, NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { Ng2SmartTableModule } from "ng2-smart-table";
import { SharedModule } from "src/app/shared/shared.module";
import { InformationWebsiteRoutingModule } from "./information-website-routing.module";
import { ListInformationWebsiteComponent } from "./list-information-website/list-information-website.component";


@NgModule({
    declarations: [ListInformationWebsiteComponent],
    imports: [
      CommonModule,
      InformationWebsiteRoutingModule,
      ReactiveFormsModule,
      NgbModule,
      SharedModule,
      NgbModalModule,
      Ng2SmartTableModule,
      CKEditorModule
    ]
  })
  export class InformationWebsiteModule { }