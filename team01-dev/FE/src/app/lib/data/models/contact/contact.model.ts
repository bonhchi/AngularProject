import { BaseModel } from '../common';

export interface ContactModel extends BaseModel {
  firstName?: string;
  lastName?: string;
  phoneNumber?: string;
  email?: string;
  message?: string;
  status?: string;
}
