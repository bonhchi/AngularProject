import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import {
  PageModel,
  ProductModel,
  ReturnMessage,
} from "src/app/lib/data/models";
import { BannerModel } from "src/app/lib/data/models/banners/banner.model";
import { BlogModel } from "src/app/lib/data/models/blogs/blog.model";
import { HomeService } from "src/app/lib/data/services/home/home.service";
import { ProductSlider } from "src/app/shared/data/slider";
import { HeaderOneComponent } from "src/app/shared/header/header-one/header-one.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
  providers: [HomeService],
})
export class HomeComponent implements OnInit {
  productKeeper = {
    topProduct: [],
    newProduct: [],
    bestSeller: [],
    featuredProduct: [],
  };
  blogs: BlogModel[] = [];
  banners: BannerModel[] = [];

  constructor(private homeService: HomeService) {}

  public ProductSliderConfig: any = ProductSlider;

  ngOnInit(): void {
    this.callProducts();
    this.getBlogs();
    this.getBanners();
  }

  callProducts() {
    this.getProducts();
    this.getNewProducts();
    this.getBestSeller();
    this.getFeaturedProducts();
  }

  getProducts() {
    this.homeService
      .getTopCollectionProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.topProduct = data.data;
      })
      .catch((e) => {});
  }

  getNewProducts() {
    this.homeService
      .getNewProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.newProduct = data.data;
      })
      .catch((e) => {});
  }

  getBestSeller() {
    this.homeService
      .getBestSellerProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.bestSeller = data.data;
      })
      .catch((e) => {});
  }

  getFeaturedProducts() {
    this.homeService
      .getFeaturedProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.featuredProduct = data.data;
      })
      .catch((e) => {});
  }

  getBlogs() {
    this.homeService
      .getBlogs()
      .then((data: ReturnMessage<BlogModel[]>) => {
        this.blogs = data.data;
      })
      .catch((e) => {});
  }

  getBanners() {
    this.homeService
      .getBanners()
      .then((data: ReturnMessage<BannerModel[]>) => {
        this.banners = data.data;
      })
      .catch((e) => {});
  }
}
