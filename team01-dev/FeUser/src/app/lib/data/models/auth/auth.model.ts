export interface AuthLoginModel {
  username: string;
  password: string;
}

export class AuthRegisterModel {
  username: string;
  password: string;
  confirmPassword: string = undefined;
  email: string;
  firstName: string;
  lastName: string;
}
