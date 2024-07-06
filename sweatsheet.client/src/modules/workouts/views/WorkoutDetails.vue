<template>
  <h1>WorkoutDetails</h1>
</template>

<script setup lang="ts">
import useWorkoutDeleteMutation from "@shared/mutations/useWorkoutDeleteMutation";
import useWorkoutUpdateMutation from "@shared/mutations/useWorkoutUpdateMutation";
import useWorkoutQuery from "@shared/queries/useWorkoutQuery";
import { WorkoutModel } from "@workouts/models";
import { computed, ref, toRef } from "vue";

const { id } = defineProps<{ id: number }>();

const { data: workoutModel, isLoading, isError } = useWorkoutQuery(toRef(id));
const { error: deleteError, isPending: isDeleting, mutateAsync: deleteWorkout } = useWorkoutDeleteMutation();
const { error: updateError, isPending: isUpdating, mutateAsync: updateWorkout } = useWorkoutUpdateMutation();
</script>
