import { Component, Input, OnInit } from "@angular/core";
import { ImageModel } from "src/app/lib/data/models/common/image.model";
import {
  ETypeSizeImage,
  TemplateType,
  TypeDisplayImage,
} from "../../data/common/common.model";

@Component({
  selector: "app-image-wrapper",
  templateUrl: "./imageWrapper.component.html",
  styleUrls: ["./imageWrapper.component.scss"],
})
export class ImageWrapperComponent {
  @Input() data: ImageModel;
  templateType = TemplateType.Display;
}
