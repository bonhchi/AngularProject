import { BaseModel } from '../common';

export interface SocialMediaModel extends BaseModel {
  title: string;
  link: string;
  iconUrl: string;
  displayOrder: number;
}
