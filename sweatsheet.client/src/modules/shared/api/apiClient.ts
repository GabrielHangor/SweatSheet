import router from "@/router";
import axios, { AxiosInstance, isAxiosError } from "axios";

enum StatusCode {
  BadRequest = 400,
  Unauthorized = 401,
  AccessDenied = 403,
  NotFound = 404,
  TooManyRequests = 429,
  InternalServerError = 500,
}

type ValidationErrors = Record<string, [string]>;

export default abstract class ApiClient {
  private readonly blobHeader = { "Content-Type": "multipart/form-data" };

  protected readonly httpClient: AxiosInstance;

  protected constructor(path: string, baseUrl = "/api/v1/", contentType = "application/json") {
    const client = axios.create({
      baseURL: `${baseUrl}${path.startsWith("/") ? path.substring(1) : path}`,
      timeout: 30000,
      headers: { "Content-Type": contentType },
    });

    client.interceptors.response.use(
      (response) => response,
      (error) => {
        return this.handleError(error);
      },
    );

    this.httpClient = client;
  }

  private async handleError(error: unknown) {
    if (error instanceof Error && isAxiosError(error)) {
      if (error.response) {
        const { status } = error.response;

        switch (status) {
          case StatusCode.BadRequest: {
            const errors = error.response.data.errors as ValidationErrors;
            break;
          }

          case StatusCode.InternalServerError: {
            break;
          }
          case StatusCode.TooManyRequests: {
            // Handle Forbidden
            break;
          }
          case StatusCode.Unauthorized: {
            router.push({ name: "login" });
            break;
          }
        }

        return Promise.reject(error);
      }
    }
  }

  // TODO вынести обработку ошибок и создание инстанса аксиоса в отдельный модуль
}
