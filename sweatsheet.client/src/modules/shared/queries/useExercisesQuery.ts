import ExercisesApi from "@shared/api/exercises/ExercisesApi";
import { useQuery } from "@tanstack/vue-query";

export default function useExercisesQuery() {
  return useQuery({
    queryKey: ["exercises"],
    queryFn: () => ExercisesApi.getExercises(),
    staleTime: 1000 * 60 * 3,
  });
}
