import { BaseModel } from "../common/base.model";

export interface ContactModel extends BaseModel {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  message: string;
}
