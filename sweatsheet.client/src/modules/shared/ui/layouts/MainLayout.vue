<template>
  <h1>main layout1</h1>
  <router-view />
</template>

<script setup lang="ts">
import { useRoute } from "vue-router";

const route = useRoute();

const tabs = [
  {
    name: "Workouts",
    path: "/workouts",
  },
  // {
  //   name: "Workout Creator",
  //   path: "/workout-create",
  //   icon: addCircleOutline,
  // },
  {
    name: "Exercises",
    path: "/exercises",
  },
];
</script>

<style>
.slide-down-enter-active,
.slide-down-leave-active {
  transition: transform 0.5s ease-in-out;
}

.slide-down-enter-from,
.slide-down-leave-to {
  transform: translateY(100%);
}

.slide-down-enter-to,
.slide-down-leave-from {
  transform: translateY(0);
}
</style>
