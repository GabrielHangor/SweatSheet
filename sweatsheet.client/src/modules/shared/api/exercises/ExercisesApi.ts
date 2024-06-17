import { Exercise } from "@exercises/models";
import ApiClient from "@shared/api/apiClient";

class ExercisesApi extends ApiClient {
  constructor() {
    super("/exercises");
  }

  async getExercises() {
    const { data } = await this.httpClient.get<Exercise[]>("");
    return data;
  }
}

export default new ExercisesApi();
