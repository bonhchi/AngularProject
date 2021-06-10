import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PagesComponent } from "./components/pages.component";
const routes: Routes = [
  {
    path: "",
    redirectTo: "/home",
    pathMatch: "full",
  },
  {
    path: "",
    component: PagesComponent,
    loadChildren: () =>
      import("./components/pages.module").then((m) => m.PagesModule),
  },
  {
    path: "auth",
    component: PagesComponent,
    loadChildren: () =>
      import("./components/auth/auth.module").then((m) => m.AuthModule),
  },
  {
    path: "**",
    redirectTo: "error",
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      initialNavigation: "enabled",
      useHash: false,
      anchorScrolling: "enabled",
      scrollPositionRestoration: "enabled",
      relativeLinkResolution: "legacy",
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
