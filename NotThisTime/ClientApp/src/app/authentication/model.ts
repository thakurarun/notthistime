export interface RegisterModel {
  email: string;
  fullName: string;
  username: string;
  password: string;
  confirmPassword: string;
}

export interface LoginModel {
  username: string;
  password: string;
}
