import WorkoutsApi from "@shared/api/workouts/WorkoutsApi";
import { useQuery } from "@tanstack/vue-query";
import { WorkoutModel } from "@workouts/models";
import { Ref } from "vue";

export default function useWorkoutQuery(workoutId: Ref<number>) {
  return useQuery({
    staleTime: 1000 * 60 * 3,
    queryKey: ["workout", workoutId.value],
    queryFn: () => WorkoutsApi.getWorkout(workoutId.value),
    select: (workout) => new WorkoutModel(workout),
  });
}
