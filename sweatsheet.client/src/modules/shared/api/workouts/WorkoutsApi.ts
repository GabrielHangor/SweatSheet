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
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    const { data } = await this.httpClient.get<PaginatedList<Workout>>("", {
      params: { page, size },
    });

    return data;
  }

  async getWorkout(id: number) {
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    const { data } = await this.httpClient.get<Workout>(`${id}`);
    return data;
  }

  async createWorkout(payload: WorkoutCreateRequestParams) {
    const { data } = await this.httpClient.post<Workout>("", payload);
    return data;
  }

  async updateWorkout(id: number, payload: WorkoutUpdateRequestParams) {
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    const { data } = await this.httpClient.put<Workout>(`/${id}`, payload);
    return data;
  }

  async deleteWorkout(id: number) {
    await new Promise((resolve) => setTimeout(resolve, DELAY));
    await this.httpClient.delete(`${id}`);
  }
}

export default new WorkoutsApi();
