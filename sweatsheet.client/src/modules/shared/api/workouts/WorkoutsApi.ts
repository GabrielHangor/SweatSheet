import ApiClient from "@shared/api/apiClient";
import { PaginatedList } from "@shared/api/typeUtils";
import { WorkoutCreateRequestParams, WorkoutUpdateRequestParams } from "@shared/api/workouts/types";
import { Workout } from "@workouts/models";

const DELAY = 1000;

class WorkoutsApi extends ApiClient {
  constructor() {
    super("/workouts");
  }

  async getWorkouts({ page, size }: { page?: number; size?: number }) {
    const { data } = await this.httpClient.get<PaginatedList<Workout>>("", {
      params: { page, size },
    });
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    return data;
  }

  async getWorkout(id: number) {
    const { data } = await this.httpClient.get<Workout>(`${id}`);
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    return data;
  }

  async createWorkout(payload: WorkoutCreateRequestParams) {
    const { data } = await this.httpClient.post<Workout>("", payload);
    return data;
  }

  async updateWorkout(id: number, payload: WorkoutUpdateRequestParams) {
    const { data } = await this.httpClient.put<Workout>(`/${id}`, payload);
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    return data;
  }

  async deleteWorkout(id: number) {
    await this.httpClient.delete(`${id}`);
    await new Promise((resolve) => setTimeout(resolve, DELAY));
  }
}

export default new WorkoutsApi();
