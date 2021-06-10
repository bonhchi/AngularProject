import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SharedModule } from 'src/app/shared/shared.module';
import { BannersService } from 'src/app/lib/data/services';
import { ListOrdersComponent } from './list-order/list-orders.component';
import { OrdersRoutingModule } from './orders-routing.module';
import { UpdateOrderComponent } from './update-order/update-order.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    Ng2SmartTableModule,
    OrdersRoutingModule,
    SharedModule,
  ],
  declarations: [ListOrdersComponent, UpdateOrderComponent],
  providers: [BannersService],
})
export class OrdersModule {}
