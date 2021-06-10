import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { ProductModel } from "../../models";

const state = {
  products: JSON.parse(localStorage["products"] || "[]"),
  wishlist: JSON.parse(localStorage["wishlistItems"] || "[]"),
  compare: JSON.parse(localStorage["compareItems"] || "[]"),
  cart: JSON.parse(
    localStorage["cartItems"] || '{"totalAmount":0,"cartDetails":[]}'
  ),
};
@Injectable()
export class CartService {
  public OpenCart: boolean = false;

  constructor(private toastrService: ToastrService) {}

  public get cartData(): Observable<any> {
    const itemsStream = new Observable((observer) => {
      observer.next(state.cart);
      observer.complete();
    });
    return <Observable<any>>itemsStream;
  }

  public processCart(cart: any): any {
    let totalAmount = 0;
    cart.cartDetails.forEach((item) => {
      totalAmount += item.price * item.quantity;
    });
    cart.totalAmount = totalAmount;
    return cart;
  }
  // Add to Cart
  public addToCart(product): any {
    if (!state.cart.cartDetails) {
      state.cart.cartDetails = [];
    }
    const cartItem = state.cart.cartDetails.find(
      (item) => item.id === product.id
    );
    const qty = product.quantity ? product.quantity : 1;
    const items = cartItem ? cartItem : product;
    const stock = this.calculateStockCounts(items, qty);

    if (!stock) return false;

    if (cartItem) {
      if (cartItem.quantity < 10) {
        cartItem.quantity += qty;
      }
    } else {
      state.cart.cartDetails.push({
        ...product,
        quantity: qty,
      });
    }
    state.cart = this.processCart(state.cart);
    localStorage.setItem("cartItems", JSON.stringify(state.cart));
    return true;
  }

  // Update Cart Quantity
  public updateCartQuantity(
    product: ProductModel,
    quantity: number
  ): ProductModel | boolean {
    return state.cart.cartDetails.find((items, index) => {
      if (items.id === product.id) {
        const qty = state.cart.cartDetails[index].quantity + quantity;
        if (qty > 0 && qty < 11) {
          state.cart.cartDetails[index].quantity = qty;
        }
        state.cart = this.processCart(state.cart);
        localStorage.setItem("cartItems", JSON.stringify(state.cart));
        return true;
      }
    });
  }

  // Calculate Stock Counts
  public calculateStockCounts(product, quantity) {
    const qty = product.quantity + quantity;
    const stock = product.stock;
    if (stock < qty || stock == 0) {
      this.toastrService.error(
        "You can not add more items than available. In stock " +
          stock +
          " items."
      );
      return false;
    }
    return true;
  }

  // Remove Cart items
  public removeCartItem(product: ProductModel): any {
    const index = state.cart.cartDetails.indexOf(product);
    state.cart.cartDetails.splice(index, 1);
    state.cart = this.processCart(state.cart);
    localStorage.setItem("cartItems", JSON.stringify(state.cart));
    return true;
  }

  public removeAll(): any {
    state.cart.cartDetails.splice(0);
    state.cart = this.processCart(state.cart);
    localStorage.setItem("cartItems", JSON.stringify(state.cart));
    return true;
  }

  // Total amount

  /*
     ---------------------------------------------
     ---------------  Wish List  -----------------
     ---------------------------------------------
   */

  // Get Wishlist Items
  public get wishlistItems(): Observable<ProductModel[]> {
    const itemsStream = new Observable((observer) => {
      observer.next(state.wishlist);
      observer.complete();
    });
    return <Observable<ProductModel[]>>itemsStream;
  }

  // Add to Wishlist
  public addToWishlist(product): any {
    const wishlistItem = state.wishlist.find((item) => item.id === product.id);
    if (!wishlistItem) {
      state.wishlist.push({
        ...product,
      });
      this.toastrService.success(
        "Sản phẩm đã được thêm vào danh sách yêu thích"
      );
      localStorage.setItem("wishlistItems", JSON.stringify(state.wishlist));
      return true;
    }
    return false;
  }

  // Remove Wishlist items
  public removeWishlistItem(product: ProductModel): any {
    const index = state.wishlist.indexOf(product);
    state.wishlist.splice(index, 1);
    this.toastrService.success("Sản phẩm đã được xóa khỏi danh sách yêu thích");
    localStorage.setItem("wishlistItems", JSON.stringify(state.wishlist));
    return true;
  }

  /*
    ---------------------------------------------
    -------------  Compare Product  -------------
    ---------------------------------------------
  */

  // Get Compare Items
  public get compareItems(): Observable<ProductModel[]> {
    const itemsStream = new Observable((observer) => {
      observer.next(state.compare);
      observer.complete();
    });
    return <Observable<ProductModel[]>>itemsStream;
  }

  // Add to Compare
  public addToCompare(product): any {
    const compareItem = state.compare.find((item) => item.id === product.id);
    if (!compareItem) {
      state.compare.push({
        ...product,
      });
    }
    this.toastrService.success("Product has been added in compare.");
    localStorage.setItem("compareItems", JSON.stringify(state.compare));
    return true;
  }

  // Remove Compare items
  public removeCompareItem(product: ProductModel): any {
    const index = state.compare.indexOf(product);
    state.compare.splice(index, 1);
    localStorage.setItem("compareItems", JSON.stringify(state.compare));
    return true;
  }
}
