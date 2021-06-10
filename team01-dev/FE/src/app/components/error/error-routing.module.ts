import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ErrorComponent } from "./error/error.component";

const routes: Routes = [
    {
      path: '',
      children: [
        {
          path: '',
          component: ErrorComponent,
          data: {
            title: "404",
            breadcrumb: "404"
          }
        },
      ]
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ErrorRoutingModule { }