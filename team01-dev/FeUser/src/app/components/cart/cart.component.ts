import { Component, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { ProductModel, TypeSweetAlertIcon } from "src/app/lib/data/models";
import { CartModel } from "src/app/lib/data/models/cart/cart.model";
import { FileService, MessageService } from "src/app/lib/data/services";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import { registerLocaleData } from "@angular/common";
import localeFr from "@angular/common/locales/fr";
import { TypeDisplayImage } from "src/app/shared/data";
registerLocaleData(localeFr, "fr");
@Component({
  selector: "app-cart",
  templateUrl: "./cart.component.html",
  styleUrls: ["./cart.component.scss"],
  providers: [CartService],
})
export class CartComponent implements OnInit, OnChanges {
  public exceed: any = false;
  public products: ProductModel[] = [];
  public cart: CartModel;
  typeDisplayImage = TypeDisplayImage;
  constructor(
    public cartService: CartService,
    private messageService: MessageService
  ) {}
  ngOnChanges(changes: SimpleChanges): void {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    this.cartService.cartData.subscribe((cart) => {
      this.products = cart.cartDetails;
      this.cart = cart;
    });
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

  getImage(fileName: string) {
    return FileService.getLinkFile(fileName);
  }
}
