import { Component, OnInit, Input } from "@angular/core";
import { ReturnMessage } from "src/app/lib/data/models";
import { FooterModel } from "src/app/lib/data/models/footer/footer.model";
import { PageContentModel } from "src/app/lib/data/models/pageContent/pageContent.model";
import { FileService } from "src/app/lib/data/services/files/file.service";
import { FooterService } from "src/app/lib/data/services/footer/footer.service";
import { TypeDisplayImage } from "../../data";

@Component({
  selector: "app-footer-one",
  templateUrl: "./footer-one.component.html",
  styleUrls: ["./footer-one.component.scss"],
})
export class FooterOneComponent implements OnInit {
  @Input() class: string = "footer-light"; // Default class
  @Input() themeLogo: string = "assets/images/icon/logo.png"; // Default Logo
  @Input() newsletter: boolean = true; // Default True
  typeDisplayImage = TypeDisplayImage;
  public footerModel: FooterModel;

  constructor(
    public footerService: FooterService,
  ) {}

  ngOnInit(): void {
    this.loadFooterModel();
  }

  async loadFooterModel() {
    await this.footerService.getFooters().then((res: ReturnMessage<FooterModel>) => {
      this.footerModel = res.data;
    })
  }
}
