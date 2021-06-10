import { Component, OnDestroy, OnInit } from "@angular/core";
import { Route, Router } from "@angular/router";
import { Subscription } from "rxjs";
import {
  OrderDetailModel,
  OrderModel,
  ProductModel,
} from "src/app/lib/data/models";
import { CartModel } from "src/app/lib/data/models/cart/cart.model";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import { AuthService } from "src/app/lib/data/services";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import { CouponService } from "src/app/lib/data/services/coupons/coupon.service";
import { OrdersService } from "src/app/lib/data/services/orders/orders.service";

@Component({
  selector: "app-checkout",
  templateUrl: "./checkout.component.html",
  styleUrls: ["./checkout.component.scss"],
  providers: [CartService, OrdersService, CouponService, AuthService],
})
export class CheckoutComponent implements OnInit, OnDestroy {
  public cart: CartModel;
  public products: ProductModel[] = [];
  public subTotal: any;
  public totalAmount: any;
  public totalItem: any;

  public couponValue = 0;
  public couponInvalid: boolean;
  public couponDisplay: boolean = false;
  public couponName: string;
  public couponPercent: string;
  public couponCode: string;
  public order: OrderModel = new OrderModel();

  subDataUser: Subscription;

  constructor(
    public cartService: CartService,
    public orderService: OrdersService,
    public couponService: CouponService,
    public routerService: Router,
    public authService: AuthService,
  ) { }
  ngOnDestroy(): void {
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  ngOnInit(): void {
    this.loadCartItems();
    this.subDataUser = this.authService.callUserInfo.subscribe((it) => {
      this.order.firstName = it?.firstName;
      this.order.lastName = it?.lastName;
      this.order.email = it?.email;
      this.order.phone = it?.phone;
      this.order.address = it?.address;
    });
  }

  loadCartItems() {
    this.cartService.cartData.subscribe((cart: CartModel) => {
      this.products = cart.cartDetails;
      this.cart = cart;
      this.subTotal = cart.totalAmount;
    });
  }

  onSubmit() {
    if(this.products.length == 0)
    {
      return;
    }
    this.loadModel();
    // console.log(this.order);
    this.orderService
      .create(this.order)
      .then((resp) => {
        this.cartService.removeAll();
        this.routerService.navigate(["checkout/success"], { state: resp });
      })
      .catch((er) => {
        // console.log(er);
      });
  }

  loadModel() {
    this.order.orderDetails = [];
    this.order.fullName = `${this.order.firstName} ${this.order.lastName}`;
    this.order.totalAmount = this.totalAmount;
    this.order.totalItem = this.totalItem;

    this.products.forEach((product) => {
      var orderDetail = new OrderDetailModel();
      orderDetail.productId = product.id;
      orderDetail.productName = product.name;
      orderDetail.price = product.price;
      orderDetail.quantity = product.quantity;
      orderDetail.totalAmount = orderDetail.price * orderDetail.quantity;
      this.order.orderDetails.push(orderDetail);
    });
  }

  applyCoupon() {
    this.order.couponCode = this.couponCode;
    this.couponService
      .getByCode(null, this.order.couponCode)
      .then((resp) => {
        this.couponInvalid = false;
        this.couponDisplay = true;

        this.order.couponId = resp.data.id;
        this.order.couponName = resp.data.name;
        this.couponName = this.order.couponName;

        if (resp.data.hasPercent) {
          this.order.couponPercent = resp.data.value;
          this.couponPercent = `-${this.order.couponPercent}%`;
          this.order.couponValue = (this.subTotal * resp.data.value) / 100;
          this.couponValue = this.order.couponValue;
          this.totalAmount = this.subTotal - this.couponValue;
          return (this.order.totalAmount = this.totalAmount);
        }

        this.order.couponValue = resp.data.value;
        this.couponValue = this.order.couponValue;
        this.order.couponPercent = parseFloat(
          ((this.order.couponValue / this.subTotal) * 100).toFixed(2)
        );
        this.couponPercent = `-${this.order.couponPercent}%`;
        if (resp.data.value > this.subTotal) {
          this.couponValue = this.subTotal;
          this.couponPercent = "-100%";
        }
        this.totalAmount =
          this.cart.totalAmount - this.couponValue < 0
            ? 0
            : this.cart.totalAmount - this.couponValue;
        this.order.totalAmount = this.totalAmount;
      })
      .catch((er) => {
        // console.log(er);
        this.couponInvalid = true;
        this.removeCoupon();
        this.couponInvalid = true;

      });
  }
  removeCoupon() {
    this.couponValue = 0;
    this.couponPercent = `0`;
    this.couponName = null;
    this.couponCode = "";

    delete this.order.couponValue;
    delete this.order.couponPercent;
    delete this.order.couponName;
    delete this.order.couponCode;
    delete this.order.couponId;

    this.couponDisplay = false;
    this.couponInvalid = false;

  }
}
