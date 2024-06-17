<template>
  <IonPage>
    <IonTabs>
      <IonRouterOutlet />
      <Transition name="slide-down">
        <IonTabBar v-show="showTabBar" slot="bottom" key="footer">
          <IonTabButton v-for="tab in tabs" :key="tab.name" :tab="tab.name" :href="tab.path">
            <IonIcon :icon="tab.icon" />
            <IonLabel>{{ tab.name }}</IonLabel>
          </IonTabButton>
        </IonTabBar>
      </Transition>
    </IonTabs>
  </IonPage>
</template>

<script setup lang="ts">
import { IonIcon, IonLabel, IonPage, IonRouterOutlet, IonTabBar, IonTabButton, IonTabs } from "@ionic/vue";
import { listOutline, barbellOutline, addCircleOutline } from "ionicons/icons";
import { computed } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();
const showTabBar = computed(() => (route.meta?.showTabBar as boolean) ?? true);

const tabs = [
  {
    name: "Workouts",
    path: "/workouts",
    icon: listOutline,
  },
  // {
  //   name: "Workout Creator",
  //   path: "/workout-create",
  //   icon: addCircleOutline,
  // },
  {
    name: "Exercises",
    path: "/exercises",
    icon: barbellOutline,
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
