import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { BannerModel } from "src/app/lib/data/models/banners/banner.model";
import { FileService } from "src/app/lib/data/services";
import { HomeSlider } from "src/app/shared/data/slider";

@Component({
  selector: "app-slider",
  templateUrl: "./slider.component.html",
  styleUrls: ["./slider.component.scss"],
})
export class SliderComponent implements OnInit {
  @Input() sliders: BannerModel[];
  @Input() class: string;
  @Input() textClass: string;
  @Input() category: string;
  @Input() buttonText: string;
  @Input() buttonClass: string;

  constructor(private route: Router) {}

  ngOnInit(): void {}

  public HomeSliderConfig: any = HomeSlider;

  getImage(image: string) {
    return FileService.getLinkFile(image);
  }

  BannerClick(slider: BannerModel) {
    if (slider?.link && slider?.link.search("http") > -1) {
      window.open(slider.link, "_blank").focus();
      return;
    }
    if(slider?.link)
    {
      this.route.navigateByUrl(slider.link);
      return;
    }
    this.route.navigateByUrl('/product');
  }
}
