import { Breakpoints } from "@angular/cdk/layout";
import { Component, HostListener, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-order-success",
  templateUrl: "./order-success.component.html",
  styleUrls: ["./order-success.component.scss"],
  providers: [],
})
export class OrderSuccessComponent implements OnInit {
  public checkOutOrders: any;
  public checkOutOrderDetails: any;
  public offsetHeightHeader = 0;
  public offsetHeightFixedElement = 0;
  public offsetTopFooter = 0;
  public offsetTopFlagElement = 0;
  public breakPoint = 0;
  public blockTop = false;
  public blockBottom = false;
  public bottomMargin = 0;
  public widthElementFixed = 0;

  constructor(private route: Router, private dataRoute: ActivatedRoute) {
    this.checkOutOrders = this.route.getCurrentNavigation().extras?.state?.data;
    if (this.checkOutOrders == null) {
      this.route.navigate(["/home"]);
    }
    this.checkOutOrderDetails = this.checkOutOrders.orderDetails;
  }

  ngOnInit(): void {
    this.offsetHeightHeader =
      document.getElementById("header-one").offsetHeight + 10;
    this.widthElementFixed =
      document.getElementById("ElementFixed").offsetParent.clientWidth;
    console.log(this.widthElementFixed);
  }

  @HostListener("document:scroll")
  scrolling() {
    this.offsetTopFlagElement =
      document.getElementById("FlagElementToFixed").offsetTop;
    this.offsetHeightFixedElement =
      document.getElementById("ElementFixed").offsetHeight;
    this.offsetTopFooter = +document.getElementById("footer-one").offsetTop;

    if (
      window.scrollY + this.offsetHeightHeader > this.offsetTopFlagElement &&
      window.scrollY +
        this.offsetHeightHeader +
        this.offsetHeightFixedElement <=
        this.offsetTopFooter
    ) {
      this.blockTop = true;
      this.blockBottom = false;
      return;
    } else if (
      window.scrollY +
        this.offsetHeightHeader +
        this.offsetHeightFixedElement +
        10 >
      this.offsetTopFooter
    ) {
      this.bottomMargin =
        window.innerHeight -
        document.getElementById("footer-one").getBoundingClientRect().top +
        10;

      this.blockBottom = true;
      this.blockTop = false;
      return;
    }

    this.blockBottom = false;
    this.blockTop = false;
  }
}
