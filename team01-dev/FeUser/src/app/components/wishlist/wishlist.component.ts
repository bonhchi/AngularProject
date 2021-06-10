import { Component, OnDestroy, OnInit } from "@angular/core";
import {
  AuthService,
  FileService,
  MessageService,
} from "src/app/lib/data/services";
import { ProductModel, ReturnMessage } from "src/app/lib/data/models";
import { CustomerWishListService } from "src/app/lib/data/services/customerWishLists/customerWishList.service";
import { TypeDisplayImage } from "src/app/shared/data";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import { SaveCustomerWishListModel } from "src/app/lib/data/models/customerWishList/customerWishList.model";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";

@Component({
  selector: "app-wishlist",
  templateUrl: "./wishlist.component.html",
  styleUrls: ["./wishlist.component.scss"],
  providers: [CustomerWishListService, CartService],
})
export class WishlistComponent implements OnInit, OnDestroy {
  products: ProductModel[] = [];
  typeDisplayImage = TypeDisplayImage;
  subscription: Subscription;
  isEmpty: boolean;

  constructor(
    private wishListService: CustomerWishListService,
    public cartService: CartService,
    private messageService: MessageService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getList();
    this.subscription = this.authService.callUserInfo.subscribe((value) => {
      if (!value) this.router.navigate([""]);
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.subscription = null;
  }

  addToCart(product: any) {
    this.cartService.addToCart(product);
  }

  getList() {
    this.wishListService
      .getByCustomer()
      .then((data: ReturnMessage<ProductModel[]>) => {
        this.isEmpty = true;
        this.products = data.data;
        if (data.data.length > 0) {
          this.isEmpty = false;
        }
      });
  }

  removeItem(product: any) {
    this.messageService
      .confirm("Bạn có muốn xóa sản phẩm?", "Có", "Không", false)
      .then((confirm) => {
        if (confirm.isConfirmed) {
          this.cartService.removeWishlistItem(product);
          const model: SaveCustomerWishListModel = {
            productId: product.id,
          };
          this.wishListService.createOrDelete(model).then(() => this.getList());
        }
      });
  }

  getImage(image: string) {
    return FileService.getLinkFile(image);
  }

  checkStock(product: ProductModel) {
    const cartItems: ProductModel[] =
      JSON.parse(localStorage.getItem("cartItems"))?.cartDetails ?? [];

    return cartItems.some((i) => product.id == i.id);
  }
}
