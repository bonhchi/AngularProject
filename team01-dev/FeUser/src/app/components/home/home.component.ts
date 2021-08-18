import { Component, OnInit } from "@angular/core";
import {
  ProductModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { BannerModel } from "src/app/lib/data/models/banners/banner.model";
import { BlogModel } from "src/app/lib/data/models/blogs/blog.model";
import { MessageService } from "src/app/lib/data/services";
import { HomeService } from "src/app/lib/data/services/home/home.service";
import { ProductSlider } from "src/app/shared/data/slider";

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

  constructor(
    private homeService: HomeService,
    private messageService: MessageService
  ) {}

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
      .catch((er) => {
        this.messageService.alert(
          er.error.message ??
            JSON.stringify(er.error.error) ??
            "Mất kết nối với máy chủ",
          TypeSweetAlertIcon.ERROR
        );
      });
  }

  getNewProducts() {
    this.homeService
      .getNewProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.newProduct = data.data;
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

  getBestSeller() {
    this.homeService
      .getBestSellerProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.bestSeller = data.data;
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

  getFeaturedProducts() {
    this.homeService
      .getFeaturedProducts()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.productKeeper.featuredProduct = data.data;
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

  getBlogs() {
    this.homeService
      .getBlogs()
      .then((data: ReturnMessage<BlogModel[]>) => {
        this.blogs = data.data;
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

  getBanners() {
    this.homeService
      .getBanners()
      .then((data: ReturnMessage<BannerModel[]>) => {
        this.banners = data.data;
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
