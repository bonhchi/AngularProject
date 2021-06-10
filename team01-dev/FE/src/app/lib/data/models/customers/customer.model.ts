import { BaseModel } from '../common';
import { UserModel } from '../users/user.model';
export interface CustomerModel extends BaseModel, UserModel {
    address: string,
    phone: string,
}