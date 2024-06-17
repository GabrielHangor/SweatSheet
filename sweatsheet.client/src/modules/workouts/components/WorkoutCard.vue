<template>
  <IonCard :router-link="`/workout/${workout.id}`" button>
    <IonCardHeader>
      <IonCardTitle>{{ workout.title }}</IonCardTitle>
      <IonCardSubtitle>{{ dayjs(workout.endTime).format("DD.MM.YYYY") }}</IonCardSubtitle>
    </IonCardHeader>

    <IonCardContent>
      <div class="flex justify-between">
        <div class="flex items-center">
          <IonIcon :icon="time" />
          <span class="ml-2">{{ dayjs.duration({ seconds: workout.workoutDurationInSeconds }).asMinutes() }} мин.</span>
        </div>
        <span>{{ workout.totalWeightWorkout }} кг</span>
      </div>
    </IonCardContent>
  </IonCard>
</template>

<script setup lang="ts">
import { IonCard, IonCardContent, IonCardHeader, IonCardSubtitle, IonCardTitle, IonIcon } from "@ionic/vue";
import { time } from "ionicons/icons";
import { Workout } from "@workouts/models";
import dayjs from "dayjs";

type Props = {
  workout: Workout;
};

const props = defineProps<Props>();
</script>
