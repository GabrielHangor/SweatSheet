import MainLayout from "@shared/ui/layouts/MainLayout.vue";
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "MainLayout",
      redirect: "/workouts",
      component: MainLayout,
      children: [
        {
          name: "workoutsList",
          path: "workouts",
          component: () => import("@workouts/views/WorkoutsList.vue"),
        },
        {
          name: "workoutDetails",
          path: "workout/:id",
          component: () => import("@workouts/views/WorkoutDetails.vue"),
          props: (route) => ({ id: Number(route.params.id) }),
          meta: { showTabBar: false },
        },
        {
          name: "exercises",
          path: "exercises",
          component: () => import("@exercises/views/ExercisesList.vue"),
        },
      ],
    },
    {
      path: "/login",
      name: "login",
      component: () => import("@auth/views/LoginPage.vue"),
    },
  ],
});

export default router;
