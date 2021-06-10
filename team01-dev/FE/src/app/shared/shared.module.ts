import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FeatherIconsComponent } from './components/feather-icons/feather-icons.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { ToggleFullscreenDirective } from './directives/fullscreen.directive';
import { ContentLayoutComponent } from './layout/content-layout/content-layout.component';
import { NavService } from './service/nav.service';
import { WINDOW_PROVIDERS } from './service/windows.service';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { RightSidebarComponent } from './components/right-sidebar/right-sidebar.component';
import { ModalHeaderComponent } from './components/modals/modal-header/modal-header.component';
import { ModalFooterComponent } from './components/modals/modal-footer/modal-footer.component';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FileService } from '../lib/data/services';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { UiImageLoaderDirective } from './directives/uiImageLoader.directive';
import { ViewImageCellComponent } from './components/viewimagecell/viewimagecell.component';
import { CustomViewCellComponent } from './components/customViewCell/customViewCell.component';
import { CustomViewCellNumberComponent } from './components/custom-view-cell-number/custom-view-cell-number.component';
import { VndFormatPipe } from '../lib/data/services/pipe/vnd-format.pipe';
import { PagingServerViewComponent } from './components/paging-server-view/paging-server-view.component';
import { MessageService } from '../lib/data/services/messages/message.service';
@NgModule({
  declarations: [
    ToggleFullscreenDirective,
    FeatherIconsComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    ContentLayoutComponent,
    BreadcrumbComponent,
    RightSidebarComponent,
    ModalHeaderComponent,
    ModalFooterComponent,
    UploadFileComponent,
    UiImageLoaderDirective,
    ViewImageCellComponent,
    CustomViewCellComponent,
    CustomViewCellNumberComponent,
    VndFormatPipe,
    PagingServerViewComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgbModalModule,
    NgxDropzoneModule,
    SweetAlert2Module.forRoot(),
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  providers: [NavService, WINDOW_PROVIDERS, FileService,MessageService],
  exports: [
    UiImageLoaderDirective,
    FeatherIconsComponent,
    ToggleFullscreenDirective,
    ModalFooterComponent,
    ModalHeaderComponent,
    UploadFileComponent,
    SweetAlert2Module,
    ViewImageCellComponent,
    CustomViewCellComponent,
    CustomViewCellNumberComponent,
    VndFormatPipe,
    PagingServerViewComponent,
  ],
})
export class SharedModule {}
