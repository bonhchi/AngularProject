import { FileDtoModel } from '..';
import { BaseModel } from '../common';
export interface UserModel extends BaseModel {
  username: string;
  password: string;
  email: string;
  firstName: string;
  lastName: string;
  imageUrl: number;
  files: FileDtoModel[];
}
