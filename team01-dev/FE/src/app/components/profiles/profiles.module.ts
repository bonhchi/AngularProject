import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileService } from 'src/app/lib/data/services/profiles/profile.service';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        NgbModule,
        Ng2SmartTableModule,
        ProfileRoutingModule,
        SharedModule
    ],
    declarations: [ProfileSettingsComponent],
    providers: [ProfileService]
})
export class ProfilesModule { }
