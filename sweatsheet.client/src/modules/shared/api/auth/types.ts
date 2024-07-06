export type SignInRequestParams = {
  username: string;
  password: string;
};

export type SignUpRequestParams = {
  username: string;
  password: string;
  confirmPassword: string;
};

export type UserIdentity = {
  username: string;
  userId: string;
  role: string;
};

export type ChangePasswordRequestParams = {
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
};
