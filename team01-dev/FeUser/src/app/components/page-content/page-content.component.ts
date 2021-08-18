import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ReturnMessage, TypeSweetAlertIcon } from "src/app/lib/data/models";
import { PageContentModel } from "src/app/lib/data/models/page-content/page-content.model";
import { PageContentService } from "src/app/lib/data/services/pageContent/pageContent.service";
import * as DecoupledEditor from "@ckeditor/ckeditor5-build-decoupled-document";
import { MessageService } from "src/app/lib/data/services";

@Component({
  selector: "app-page-content",
  templateUrl: "./page-content.component.html",
  styleUrls: ["./page-content.component.scss"],
  providers: [PageContentService],
  encapsulation: ViewEncapsulation.None,
})
export class PageContentComponent implements OnInit {
  pageContent: PageContentModel;
  isContactUs: boolean;
  editor = DecoupledEditor;
  config = {
    isReadOnly: true,
  };

  constructor(
    public pageContentService: PageContentService,
    private activatedRoute: ActivatedRoute,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.activatedRoute.params.subscribe(() => {
      this.getCurrentPageContent();
    });
  }

  getCurrentPageContent() {
    const id = this.activatedRoute.snapshot.paramMap.get("id");
    this.isContactUs = id === "00000000-0000-0000-0000-000000000002";

    this.pageContentService
      .getById(null, id)
      .then((data: ReturnMessage<PageContentModel>) => {
        this.pageContent = data.data;
      })
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Mất kết nối với máy chủ",
          TypeSweetAlertIcon.ERROR
        );
      });
  }
}
