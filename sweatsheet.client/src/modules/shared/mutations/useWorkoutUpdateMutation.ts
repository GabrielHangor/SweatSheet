import WorkoutsApi from "@shared/api/workouts/WorkoutsApi";
import { WorkoutUpdateRequestParams } from "@shared/api/workouts/types";
import { useMutation, useQueryClient } from "@tanstack/vue-query";

export default function useWorkoutUpdateMutation() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ workoutId, payload }: { workoutId: number; payload: WorkoutUpdateRequestParams }) =>
      WorkoutsApi.updateWorkout(workoutId, payload),
    onSuccess: (workout, variables) => {
      queryClient.setQueryData(["workout", variables.workoutId], workout);
      queryClient.invalidateQueries({ queryKey: ["workouts"] });
    },
  });
}
