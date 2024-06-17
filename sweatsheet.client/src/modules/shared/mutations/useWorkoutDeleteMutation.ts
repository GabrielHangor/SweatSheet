import WorkoutsApi from "@shared/api/workouts/WorkoutsApi";
import { useMutation, useQueryClient } from "@tanstack/vue-query";

export default function useWorkoutDeleteMutation() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (workoutId: number) => WorkoutsApi.deleteWorkout(workoutId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["workouts"] });
    },
  });
}
