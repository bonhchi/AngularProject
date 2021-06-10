import { Component, OnInit, PLATFORM_ID, Inject } from "@angular/core";
import { isPlatformBrowser } from "@angular/common";
import { TranslateService } from "@ngx-translate/core";
import { FileService, MessageService } from "src/app/lib/data/services";
import {
  ProductModel,
  ReturnMessage,
  TypeSweetAlertIcon,
} from "src/app/lib/data/models";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import { ActivatedRoute, Router } from "@angular/router";
import { SearchService } from "src/app/lib/data/services/search/search.service";
import { CartModel } from "src/app/lib/data/models/cart/cart.model";
import { TypeDisplayImage } from "../../data";

@Component({
  selector: "app-settings",
  templateUrl: "./settings.component.html",
  styleUrls: ["./settings.component.scss"],
  providers: [CartService],
  styles: [
    `
      .product-link {
        text-decoration: none;
        color: black;
        font-size: 25px;
        cursor: pointer;
      }
    `,
  ],
})
export class SettingsComponent implements OnInit {
  public products: ProductModel[] = [];
  public search: boolean = false;
  public name: string;
  public value: any;
  public data: ProductModel[] = [];
  public userFilter: any = { name: "" };
  public path: any;
  public id: string;
  public cart: CartModel;
  public typeDisPlayImage = TypeDisplayImage;
  public exceed: boolean = false;
  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private translate: TranslateService,
    public cartService: CartService,
    public router: Router,
    private activatedRoute: ActivatedRoute,
    private searchService: SearchService,
    private messageService: MessageService
  ) {
    this.cartService.cartData.subscribe((response: CartModel) => {
      this.products = response.cartDetails;
    });
  }

  ngOnInit(): void {
    this.getRoute();
  }

  searchToggle() {
    this.search = !this.search;
  }

  changeLanguage(code) {
    if (isPlatformBrowser(this.platformId)) {
      this.translate.use(code);
    }
  }

  removeItem(product: any) {
    this.messageService
      .confirm(`Bạn có muốn xóa sản phẩm?`, "Có", "Không", false)
      .then((res) => {
        if (res.isConfirmed) {
          this.cartService.removeCartItem(product);
        }
      });
  }

  getImage(fileName: string) {
    return FileService.getLinkFile(fileName);
  }

  getRoute() {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.name = params["name"];
    });
  }

  onSearch(event: KeyboardEvent) {
    const target = event.target as HTMLInputElement;
    if (event.code == "Enter" || event.code == "NumpadEnter") {
      this.router.navigate(["product"], {
        queryParams: { search: target?.value },
        relativeTo: this.activatedRoute,
      });
      this.search = false;
    }
  }
  getItem(event: KeyboardEvent) {
    var search = "";
    search += (event.target as HTMLInputElement).value;
    this.searchService
      .findByName(null, search)
      .then((res: ReturnMessage<ProductModel[]>) => {
        this.data = res.data;
      });
    this.path = "product-details?id={item.id}`";
  }

  clickRouter(id: String) {
    const url = `/product-details?id=${id}`;
    this.activatedRoute.queryParams.subscribe((params) => {
      id = params["id"];
    });

    this.router.navigateByUrl(url);
    this.searchToggle();
  }
  adjustQuantity(product: any, number: any) {
    this.exceed = false;
    if (product.quantity == 1 && number == -1) {
      return this.removeItem(product);
    }
    if (product.quantity == 10 && number == 1) {
      this.exceed = true;
      this.messageService.notification(
        "Số lượng sản phẩm không được vượt quá 10",
        TypeSweetAlertIcon.WARNING
      );
      return;
    }
    this.cartService.updateCartQuantity(product, number);
  }
}
