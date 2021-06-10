import { Component, OnInit } from "@angular/core";
import { ReturnMessage } from "src/app/lib/data/models";
import { PageContentModel } from "src/app/lib/data/models/pageContent/pageContent.model";
import { PageContentService } from "src/app/lib/data/services/pageContent/pageContent.service";
import { TypeDisplayImage } from "src/app/shared/data";

@Component({
  selector: "app-services",
  templateUrl: "./services.component.html",
  styleUrls: ["./services.component.scss"],
  providers: [PageContentService],
})
export class ServicesComponent implements OnInit {
  pageContents: PageContentModel[] = [];
  typeDisplayImage = TypeDisplayImage;

  constructor(private pageContentService: PageContentService) {}

  ngOnInit(): void {
    this.getPageContents();
  }

  getPageContents() {
    this.pageContentService
      .getList()
      .then((data: ReturnMessage<PageContentModel[]>) => {
        this.pageContents = data.data;
      });
  }
}
