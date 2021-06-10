import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuardsAdminService } from "../lib/guards/auth.guard";
import { BlogDetailComponent } from "./blogs/blog-detail/blog-detail.component";
import { BlogMainComponent } from "./blogs/blog-main/blog-main.component";
import { CartComponent } from "./cart/cart.component";
import { CheckoutComponent } from "./checkout/checkout.component";
import { ErrorComponent } from "./error/error.component";
import { HomeComponent } from "./home/home.component";
import { OrderSuccessComponent } from "./order-success/order-success.component";
import { PageContentComponent } from "./page-content/page-content.component";
import { ProductDetailsComponent } from "./product/product-details/product-details.component";

import { ProductListComponent } from "./product/product-list/product-list.component";
import { ProfileComponent } from "./profile/profile/profile.component";
import { WishlistComponent } from "./wishlist/wishlist.component";

const routes: Routes = [
  {
    path: "home",
    component: HomeComponent,
  },
  {
    path: "blog",
    component: BlogMainComponent,
  },
  {
    path: "product",
    component: ProductListComponent,
  },
  {
    path: "cart",
    component: CartComponent,
  },
  {
    path: "checkout",
    component: CheckoutComponent,
  },
  {
    path: "checkout/success",
    component: OrderSuccessComponent,
  },
  {
    path: "blog/:id",
    component: BlogDetailComponent,
  },
  {
    path: "product-details",
    component: ProductDetailsComponent,
  },
  {
    path: "page-info/:id",
    component: PageContentComponent,
  },
  {
    path: "profile",
    component: ProfileComponent,
    canActivate: [AuthGuardsAdminService],
  },
  {
    path: "wishlist",
    component: WishlistComponent,
    canActivate: [AuthGuardsAdminService],
  },
  {
    path: "error",
    component: ErrorComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [AuthGuardsAdminService],
})
export class PagesRoutingModule {}
