import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import {
  ETypeSort,
  PageModel,
  ProductModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { MessageService } from "src/app/lib/data/services";
import { HomeService } from "src/app/lib/data/services/home/home.service";
import { ProductListService } from "src/app/lib/data/services/productlist/productlist.service";

import { ETypeGridLayout } from "src/app/shared/data";

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
  providers: [HomeService, ProductListService],
})
export class ProductListComponent implements OnInit, OnDestroy {
  public grid: string = ETypeGridLayout.NORMAL;
  public layoutView: string = "grid-view";
  public products: ProductModel[];
  public pageModel: PageModel<ProductModel>;
  public minPrice: number = 0;
  public maxPrice: number = 1000000000;
  public sizePrice: string;
  public tags: any[] = [];
  public category: string[] = [];
  public paginate: any = {};
  public sortBy: number = ETypeSort.NULL;
  public mobileSidebar: boolean = false;
  public finished: boolean = false;
  public params;
  public subscribe: Subscription;
  public isEmptyProduct: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public productListService: ProductListService,
    private sweetalertService: MessageService
  ) {}
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
    this.subscribe = null;
  }

  ngOnInit(): void {
    // Get Query params..
    this.subscribe = this.route.queryParams.subscribe((params) => {
      this.products = [];
      this.params = {};

      this.tags = [];
      this.finished = false;
      this.isEmptyProduct = false;
      this.pageModel = null;

      this.minPrice = params.minPrice ? params.minPrice : this.minPrice;
      this.maxPrice = params.maxPrice ? params.maxPrice : this.maxPrice;

      this.category = params["search.categoryName"]
        ? params["search.categoryName"].split(",")
        : [];
      this.sortBy = params.typeSort ? params.typeSort : ETypeSort.NULL;

      if (this.category.length > 0) {
        this.category.forEach((x) => this.tags.push(x));
        this.params["search.categoryName"] = this.category.join(",");
      }

      this.params["search.Name"] = params.search
        ? params.search
        : this.params["search.Name"];
      if (this.params["search.Name"] == null) {
        delete this.params["search.Name"];
      }

      this.minPrice > 0
        ? (this.params.minPrice = this.minPrice)
        : delete this.params.minPrice;
      this.maxPrice < 5000000
        ? (this.params.maxPrice = this.maxPrice)
        : delete this.params.maxPrice;

      this.sortBy != ETypeSort.NULL
        ? (this.params.typeSort = this.sortBy)
        : delete this.params.typeSort;

      this.params.pageSize = 24;
      this.params.loading = true;
      this.addItems();
    });
  }

  addItems() {
    if (this.pageModel?.totalItem == this.products.length) {
      this.finished = true;
      return;
    }
    this.productListService
      .getPageProduct({ params: this.params })
      .then((res: ReturnMessage<PageModel<ProductModel>>) => {
        this.pageModel = res.data;
        this.params.pageIndex = res.data.pageIndex;
        this.params.pageSize = res.data.pageSize;

        this.products = [...this.products, ...res.data.results];
        this.products.length == 0
          ? (this.isEmptyProduct = true)
          : (this.isEmptyProduct = false);
        // console.log(this.products);
      })
      .catch((res) =>
        this.sweetalertService.alert(
          res.error.message ?? res.error,
          TypeSweetAlertIcon.ERROR
        )
      );
  }

  // Infinite scroll
  public onScroll() {
    // add another items
    this.params.pageIndex++;
    this.addItems();
  }

  // Append filter value to Url
  updateFilter(tags: any) {
    this.resetPage();

    this.params.minPrice = tags.minPrice;
    this.params.maxPrice = tags.maxPrice;

    this.addNavigate(this.params);
  }

  // SortBy Filter
  sortByFilter(value) {
    this.sortBy = value;
    this.resetPage();

    this.params.typeSort = value;

    if (value == ETypeSort.NULL) {
      delete this.params.typeSort;
    }

    this.addNavigate(this.params);
  }

  // // Remove Tag
  removeTag(tag) {
    this.resetPage();
    this.tags = this.tags.filter((val) => val !== tag);
    if (this.category.indexOf(tag) > -1) {
      this.category = this.category.filter((val) => val !== tag);
      this.category.length > 0
        ? (this.params["search.categoryName"] = this.category.join(","))
        : delete this.params["search.categoryName"];
    }

    this.addNavigate(this.params);
  }

  // // Clear Tags
  removeAllTags() {
    this.tags = [];
    this.resetPage();
    delete this.params["search.categoryName"];
    this.category = [];

    this.addNavigate(this.params);
  }

  // Change Grid Layout
  updateGridLayout(value: string) {
    this.grid = value;
  }

  // Change Layout View
  updateLayoutView(value: string) {
    this.layoutView = value;
    if (value == "list-view") {
      this.grid = "col-lg-12";
      return;
    }

    // this.grid = "col-xl-3 col-md-6";
    this.grid = ETypeGridLayout.NORMAL;
  }

  // Mobile sidebar
  toggleMobileSidebar() {
    this.mobileSidebar = !this.mobileSidebar;
  }

  onChangeTypeCate(event: string) {
    // this.tags = this.tags.filter((x) => x != this.category);

    if (this.category == null || this.category.length <= 0) {
      this.category = [];
    }

    if (this.category.indexOf(event) > -1) {
      return;
    }

    if (this.category.length == 0 && event == "ALL") {
      return;
    }

    this.category.push(event);

    if (event != "ALL") {
      this.params["search.categoryName"] = this.category.join(",");
      this.tags.indexOf(event) > -1 ? null : this.tags.push(event);
    }

    if (event == "ALL") {
      delete this.params["search.categoryName"];
      this.tags = this.tags.filter((x) => this.category.indexOf(x) < 0);
    }

    this.resetPage();
    this.addNavigate(this.params);
  }

  resetPage() {
    this.pageModel = null;
    this.products = [];
    this.finished = false;

    delete this.params.pageIndex;
    delete this.params.pageSize;
  }

  addNavigate(params = {}) {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: params,
      skipLocationChange: false,
    });
    // .finally(() => {
    //   this.viewScroller.setOffset([120, 120]);
    //   this.viewScroller.scrollToAnchor("products"); // Anchore Link
    // });
  }
}
