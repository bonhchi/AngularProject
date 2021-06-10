import { Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { InformationWebModel, ReturnMessage } from 'src/app/lib/data/models';
import { AuthService, FileService, InformationWebsiteService } from 'src/app/lib/data/services';
import { NavService, Menu } from '../../service/nav.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [AuthService, InformationWebsiteService]
})
export class SidebarComponent implements OnInit, OnDestroy {
  public infoWeb: InformationWebModel;
  public menuItems: Menu[];
  public url: any;
  public fileurl: any;
  userInfo: any;
  entrySub: Subscription;

  constructor(private router: Router,
     public navServices: NavService,
    private authService: AuthService,
    private inforWebService:InformationWebsiteService) {
    // this.userManagerService.UserSubject.subscribe((it) =>{
    //   this.userInfo = it;
    // });
    this.fetchWebInFo();
    this.navServices.items.subscribe(menuItems => {
      this.menuItems = menuItems
      this.router.events.subscribe((event) => {
        if (event instanceof NavigationEnd) {
          menuItems.filter(items => {
            if (items.path === event.url)
              this.setNavActive(items)
            if (!items.children) return false
            items.children.filter(subItems => {
              if (subItems.path === event.url)
                this.setNavActive(subItems)
              if (!subItems.children) return false
              subItems.children.filter(subSubItems => {
                if (subSubItems.path === event.url)
                  this.setNavActive(subSubItems)
              })
            })
          })
        }
      })
    })
  }
  ngOnDestroy(): void {
    if(this.entrySub)
    {
      this.entrySub.unsubscribe();
      this.entrySub = null;
    }
  }
  ngOnInit(): void {
    this.authService.callUserInfo.subscribe(it => this.userInfo = it);
  }

  // Active Nave state
  setNavActive(item) {
    this.menuItems.filter(menuItem => {
      if (menuItem != item)
        menuItem.active = false
      if (menuItem.children && menuItem.children.includes(item))
        menuItem.active = true
      if (menuItem.children) {
        menuItem.children.filter(submenuItems => {
          if (submenuItems.children && submenuItems.children.includes(item)) {
            menuItem.active = true
            submenuItems.active = true
          }
        })
      }
    })
  }

  // Click Toggle menu
  toggletNavActive(item) {
    if (!item.active) {
      this.menuItems.forEach(a => {
        if (this.menuItems.includes(item))
          a.active = false
        if (!a.children) return false
        a.children.forEach(b => {
          if (a.children.includes(item)) {
            b.active = false
          }
        })
      });
    }
    item.active = !item.active
  }

  //Fileupload
  readUrl(event: any) {
    if (event.target.files.length === 0)
      return;
    //Image upload validation
    var mimeType = event.target.files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    // Image upload
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (_event) => {
      this.url = reader.result;
    }
  }
  //Web Information
      
  fetchWebInFo()
  {
    this.inforWebService
      .get(null)
      .then((res : ReturnMessage<InformationWebModel>) => {
      if(!res.hasError)
      {
        this.infoWeb = res.data;
      }
    }).catch((er) => {
      
      if(er.error.hasError)
      {
      }
    });
  }
}
