import { BrowserModule, Title } from "@angular/platform-browser";
import { APP_INITIALIZER, NgModule } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClient, HttpClientModule } from "@angular/common/http";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { LoadingBarHttpClientModule } from "@ngx-loading-bar/http-client";
import { LoadingBarRouterModule } from "@ngx-loading-bar/router";
import { ToastrModule } from "ngx-toastr";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { SharedModule } from "./shared/shared.module";
import { AppRoutingModule } from "./app-routing.module";

import { AppComponent } from "./app.component";
import { PagesComponent } from "./components/pages.component";
import { environment } from "./lib/environments/environment";
import { AppConfig } from "./lib/environments/config/appConfig";
import { AuthModule } from "./components/auth/auth.module";

import { NgHttpLoaderModule } from "ng-http-loader";
import { ImageWrapperComponent } from "./shared/components/imageWrapper/imageWrapper.component";

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

@NgModule({
  declarations: [AppComponent, PagesComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "serverApp" }),
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule,
    LoadingBarHttpClientModule,
    LoadingBarRouterModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      progressBar: false,
      enableHtml: true,
    }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    SharedModule,
    AuthModule,
    AppRoutingModule,
    NgHttpLoaderModule.forRoot(),
  ],
  providers: [
    { provide: "BASE_URL", useValue: environment.host },
    AppConfig,
    Title,
    {
      provide: APP_INITIALIZER,
      multi: true,
      deps: [AppConfig],
      useFactory: (appConfigService: AppConfig) => () =>
        appConfigService.load(),
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
