<template>
  <IonItem>
    <IonInput v-model="workoutModel.title" placeholder="Название тренировки" />
  </IonItem>

  <section class="my-4">
    <table class="w-full table-auto border-collapse border">
      <thead>
        <tr>
          <th class="border p-2 text-left">
            <IonIcon :icon="time" />
            {{ dayjs.duration({ seconds: workoutModel.workoutDurationInSeconds }).asMinutes() }} мин.
          </th>
          <th class="border p-2 text-left font-semibold">
            {{ dayjs(workoutModel.startTime).format("HH:mm:ss") }}
          </th>
          <th class="border p-2 text-left font-semibold">
            {{ dayjs(workoutModel.startTime).format("DD.MM.YYYY") }}
          </th>
          <th class="border p-2 text-left font-semibold">
            <IonIcon :icon="prism" />
            {{ workoutModel.totalWeightWorkout }} кг
          </th>
        </tr>
      </thead>
    </table>
  </section>
</template>

<script setup lang="ts">
import { IonIcon, IonInput, IonItem } from "@ionic/vue";
import { time, prism } from "ionicons/icons";
import dayjs from "dayjs";
import { WorkoutModel } from "@workouts/models";

const workoutModel = defineModel<WorkoutModel>({ required: true });
</script>
