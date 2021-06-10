import { Component, OnInit } from '@angular/core';
import { NavService } from '../../service/nav.service';
import { trigger, transition, useAnimation } from '@angular/animations';
import { bounce, zoomOut, zoomIn, fadeIn, bounceIn } from 'ng-animate';
import { ActivatedRoute } from '@angular/router';
import {
  ReturnMessage,
  TypeSweetAlertIcon,
  UserDataReturnDTOModel,
} from 'src/app/lib/data/models';
import { RouterHelperService } from 'src/app/lib/helpers';

@Component({
  selector: 'app-content-layout',
  templateUrl: './content-layout.component.html',
  styleUrls: ['./content-layout.component.scss'],
  animations: [
    trigger('animateRoute', [
      transition(
        '* => *',
        useAnimation(fadeIn, {
          // Set the duration to 5seconds and delay to 2 seconds
          //params: { timing: 3}
        })
      ),
    ]),
  ],
})
export class ContentLayoutComponent implements OnInit {
  public right_side_bar: boolean;
  public layoutType: string = 'RTL';
  public layoutClass: boolean = false;
  public userInfo: any;

  constructor(
    public navServices: NavService,
    public activedRoute: ActivatedRoute
  ) {}

  public getRouterOutletState(outlet) {
    return outlet.isActivated ? outlet.activatedRoute : '';
  }

  public rightSidebar($event) {
    this.right_side_bar = $event;
  }

  public clickRtl(val) {
    if (val === 'RTL') {
      document.body.className = 'rtl';
      this.layoutClass = true;
      this.layoutType = 'LTR';
    } else {
      document.body.className = '';
      this.layoutClass = false;
      this.layoutType = 'RTL';
    }
  }

  ngOnInit() {}
}
