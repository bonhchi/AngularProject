import { Component, OnInit } from '@angular/core';
import { InstaSlider } from 'src/app/shared/data/slider';
import { InstagramService } from 'src/app/shared/services/instagram.service';

@Component({
  selector: 'app-instagram',
  templateUrl: './instagram.component.html',
  styleUrls: ['./instagram.component.scss']
})
export class InstagramComponent implements OnInit {
  
  public instagram: any;

  constructor(private instaService: InstagramService) { 
  	this.instaService.getInstagramData.subscribe(response => this.instagram = response);
  }

  ngOnInit(): void {
  }

  public InstaSliderConfig: any = InstaSlider;

}
