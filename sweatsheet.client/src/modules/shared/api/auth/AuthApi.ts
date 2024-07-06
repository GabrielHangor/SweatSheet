import ApiClient from "@shared/api/apiClient";
import {
  ChangePasswordRequestParams,
  SignInRequestParams,
  SignUpRequestParams,
  UserIdentity,
} from "@shared/api/auth/types";

class AuthApi extends ApiClient {
  constructor() {
    super("/auth");
  }

  signUp(payload: SignUpRequestParams): Promise<void> {
    return this.httpClient.post("/register", payload);
  }

  signIn(payload: SignInRequestParams): Promise<void> {
    return this.httpClient.post("/login", payload);
  }

  logout(): Promise<void> {
    return this.httpClient.post("/logout");
  }

  async getCurrentUser() {
    const { data } = await this.httpClient.get<UserIdentity>("/user");
    return data;
  }

  changePassword(payload: ChangePasswordRequestParams): Promise<void> {
    return this.httpClient.post("/changePassword", payload);
  }
}

export default new AuthApi();
