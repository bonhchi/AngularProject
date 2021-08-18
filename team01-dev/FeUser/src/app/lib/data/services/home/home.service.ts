import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { HttpClientService } from "src/app/lib/http/http-client";
import { ProductModel } from "../../models";

const state = {
  products: JSON.parse(localStorage["products"] || "[]"),
  wishlist: JSON.parse(localStorage["wishlistItems"] || "[]"),
  compare: JSON.parse(localStorage["compareItems"] || "[]"),
  cart: JSON.parse(localStorage["cartItems"] || "[]"),
};

@Injectable()
export class HomeService {
  private url = "/api/home";
  public Currency = { name: "Viet Nam dong", currency: "VND", price: 1 }; // Default Currency
  public OpenCart: boolean = false;

  constructor(
    private httpClient: HttpClientService,
    private toastrService: ToastrService
  ) {}

  // Top Collection
  getTopCollectionProducts(request: any = null) {
    const url = this.url + "/top-collection";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  // New Products
  getNewProducts(request: any = null) {
    const url = this.url + "/new-products";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  // Best Seller
  getBestSellerProducts(request: any = null) {
    const url = this.url + "/best-seller";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  // Featured Products
  getFeaturedProducts(request: any = null) {
    const url = this.url + "/featured-products";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  // On Sale
  getOnSaleProducts(request: any = null) {
    const url = this.url + "/on-sale";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  // Get blogs
  getBlogs(request: any = null) {
    const url = this.url + "/blogs";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  getBanners(request: any = null) {
    const url = this.url + "/banners";
    return this.httpClient.getObservable(url, request).toPromise();
  }

  // Add to Wishlist
  public addToWishlist(product): any {
    const wishlistItem = state.wishlist.find((item) => item.id === product.id);
    if (!wishlistItem) {
      state.wishlist.push({
        ...product,
      });
    }
    this.toastrService.success("Sản phẩm đã thêm vào danh sách yêu thích");
    localStorage.setItem("wishlistItems", JSON.stringify(state.wishlist));
    return true;
  }

  // Add to Compare
  public addToCompare(product: ProductModel): any {
    const compareItem = state.compare.find((item) => item.id === product.id);
    if (!compareItem) {
      state.compare.push({
        ...product,
      });
    }
    this.toastrService.success("Sản phẩm đã được thêm vào so sánh");
    localStorage.setItem("compareItems", JSON.stringify(state.compare));
    return true;
  }

  // Add to Cart
  public addToCart(product: ProductModel): any {
    const cartItem = state.cart.find((item) => item.id === product.id);
    const qty = 1;
    const items = cartItem ? cartItem : product;
    const stock = this.calculateStockCounts(items, qty);

    if (!stock) return false;

    if (cartItem) {
      cartItem.quantity += qty;
    } else {
      state.cart.push({
        ...product,
        quantity: qty,
      });
    }

    this.OpenCart = true; // If we use cart variation modal
    localStorage.setItem("cartItems", JSON.stringify(state.cart));
    return true;
  }

  public calculateStockCounts(product, quantity) {
    const qty = product.quantity + quantity;
    const stock = product.stock;
    if (stock < qty || stock == 0) {
      this.toastrService.error(
        "Bạn không thể thêm sản phẩm hơn số lượng hiện có trong cửa hàng " +
          stock +
          " sản phẩm."
      );
      return false;
    }
    return true;
  }

  // Get Cart Items
  public get cartItems(): Observable<ProductModel[]> {
    const itemsStream = new Observable((observer) => {
      observer.next(state.cart);
      observer.complete();
    });
    return <Observable<ProductModel[]>>itemsStream;
  }
}
