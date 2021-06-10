import {
  Attribute,
  Directive,
  ElementRef,
  HostListener,
  Input,
  Renderer2,
} from '@angular/core';
import { AppConfig } from 'src/app/lib/environments/config/appConfig';

@Directive({
  selector: '[appUiImageLoader]',
  host: {
    '(error)':'updateUrl()',
    '[src]':'sUrl'
   },
})
export class UiImageLoaderDirective {
  public defaultImg = "/assets/images/error.png";
  public sUrl: string;

  constructor(
    private renderer: Renderer2,
    private el: ElementRef
  ) {
  }
  @Input()
  set src(value: string) {
    if (!value) {
      return;
    }
    var result = `${AppConfig.settings.API_URL}/Files/${value}`;
    if(value.includes('http'))
    {
      result = value;
    }

    if(value.includes('assets'))
    {
      result = value;
    }
    
    this.sUrl = result;
  }

  updateUrl()
  {
    this.sUrl = this.defaultImg;
  }
}
