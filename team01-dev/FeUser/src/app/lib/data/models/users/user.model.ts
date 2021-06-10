import { BaseModel } from "../common";
import { FileDtoModel } from "../files/file.model";
export interface UserModel extends BaseModel {
  username: string;
  password: string;
  customerId: string;
  email: string;
  firstName: string;
  lastName: string;
  imageUrl: string;
  phone: string;
  address: string;
  files: FileDtoModel[];
}

export interface UserDataReturnDTOModel {
  id: string;
  username: string;
  customerId: string;
  email: string;
  firstName: string;
  lastName: string;
  imageUrl: string;
  address: string;
  phone: string;
  token: string;
}
