import { BaseModel } from "../common";
import { FileDtoModel } from "../files/file.model";
export interface ProfileModel extends BaseModel {
    firstName: string;
    lastName: string;
    email: string;
    imageUrl: string;
    files: FileDtoModel[];
}
export interface ChangePasswordProfileModel{
    userName: string;
    password: string;
    newPassword: string;
    confirmNewPassword: string;
}