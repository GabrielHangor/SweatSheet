<template>
  <IonPage>
    <IonHeader translucent>
      <IonToolbar>
        <IonTitle>Workouts</IonTitle>
      </IonToolbar>
    </IonHeader>
    <IonContent fullscreen>
      <WorkoutsListSkeleton v-if="isLoading" />
      <IonList v-else>
        <template v-for="page in data?.pages" :key="page.currentPage">
          <WorkoutCard v-for="workout in page.items" :key="workout.id" :workout="workout" />
        </template>
      </IonList>
      <IonInfiniteScroll :disabled="!hasMoreData" @ion-infinite="() => fetchNextPage()">
        <IonInfiniteScrollContent></IonInfiniteScrollContent>
      </IonInfiniteScroll>
    </IonContent>
  </IonPage>
</template>

<script setup lang="ts">
import {
  IonContent,
  IonHeader,
  IonInfiniteScroll,
  IonInfiniteScrollContent,
  IonList,
  IonPage,
  IonTitle,
  IonToolbar,
} from "@ionic/vue";

import useWorkoutsInfiniteQuery from "@shared/queries/useWorkoutsInfiniteQuery";
import WorkoutCard from "@workouts/components/WorkoutCard.vue";
import WorkoutsListSkeleton from "@workouts/components/WorkoutsListSkeleton.vue";
import { computed } from "vue";

const { data, error, isLoading, fetchNextPage } = useWorkoutsInfiniteQuery();

const hasMoreData = computed(() => data.value?.pages.at(-1)?.hasNext);
</script>
