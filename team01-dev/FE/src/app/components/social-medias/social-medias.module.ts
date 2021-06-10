import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SocialMediaService } from 'src/app/lib/data/services/social-media/social-media.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListSocialMediaComponent } from './list-social-media/list-social-media.component';
import { SocialMediaDetailComponent } from './social-media-detail/social-media-detail.component';
import { SocialMediasRoutingModule } from './social-medias-routing.module';

@NgModule({
  declarations: [ListSocialMediaComponent, SocialMediaDetailComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SocialMediasRoutingModule,
    NgbModule,
    Ng2SmartTableModule,
    SharedModule,
  ],
  providers: [SocialMediaService],
})
export class SocialMediasModule {}
