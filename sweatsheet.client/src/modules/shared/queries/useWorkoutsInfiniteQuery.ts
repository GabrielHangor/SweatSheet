import WorkoutsApi from "@shared/api/workouts/WorkoutsApi";
import { useInfiniteQuery } from "@tanstack/vue-query";
import { Ref } from "vue";

export default function useWorkoutsInfiniteQuery() {
  return useInfiniteQuery({
    queryKey: ["workouts"],
    queryFn: ({ pageParam }) => WorkoutsApi.getWorkouts({ page: pageParam }),
    staleTime: 1000 * 60 * 3,
    initialPageParam: 1,
    getNextPageParam: (lastPage, allPages) =>
      allPages.at(-1)?.hasNext ? lastPage.currentPage + 1 : undefined,
  });
}
