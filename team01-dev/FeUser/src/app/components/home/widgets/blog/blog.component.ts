import { Component, OnInit, Input } from "@angular/core";
import { BlogModel } from "src/app/lib/data/models/blogs/blog.model";
import { ImageModel } from "src/app/lib/data/models/common/image.model";
import { FileService } from "src/app/lib/data/services";
import { TypeDisplayImage } from "src/app/shared/data/common/common.model";
import { BlogSlider } from "src/app/shared/data/slider";

@Component({
  selector: "app-blog",
  templateUrl: "./blog.component.html",
  styleUrls: ["./blog.component.scss"],
})
export class BlogComponent implements OnInit {
  @Input() blogs: BlogModel[] = [];
  typeDisplayImage = TypeDisplayImage;

  constructor() {}

  ngOnInit(): void {}

  public BlogSliderConfig: any = BlogSlider;

  getImage(image: string) {
    return FileService.getLinkFile(image);
  }

  getDate(date: string) {
    return new Date(date).toDateString();
  }
}
