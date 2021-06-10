import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { CouponService } from 'src/app/lib/data/services/coupons/coupon.service';
import { SharedModule } from 'src/app/shared/shared.module';

import { CouponDetailComponent } from './coupon-detail/coupon-detail.component';
import { CouponsRoutingModule } from './coupons-routing.module';
import { ListCouponComponent } from './list-coupon/list-coupon.component';

@NgModule({
  declarations: [ListCouponComponent, CouponDetailComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CouponsRoutingModule,
    NgbModule,
    Ng2SmartTableModule,
    SharedModule,
  ],
  providers: [CouponService, DatePipe],
})
export class CouponsModule {}
