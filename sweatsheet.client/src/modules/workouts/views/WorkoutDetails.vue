<template>
  <IonPage>
    <IonHeader :translucent="true">
      <IonToolbar>
        <IonButtons slot="start">
          <IonBackButton text="Назад" default-href="/workouts" @click="handleGoBack" />
        </IonButtons>
        <IonButtons slot="end">
          <IonButton color="danger" @click="handleDelete">Удалить</IonButton>
        </IonButtons>
        <IonTitle>Workout Details</IonTitle>
      </IonToolbar>
    </IonHeader>
    <IonContent fullscreen>
      <IonSpinner v-if="isLoading" />
      <template v-else>
        <WorkoutEditor
          v-if="workoutModel"
          :workout="workoutModel"
          :display-footer="displayFooter"
          @workout:update="handleUpdate"
        />
      </template>
      <IonLoading
        v-if="isDeleting || isUpdating"
        :message="loadingMessage"
        :is-open="isDeleting || isUpdating"
      />
    </IonContent>

    <Transition name="slide-down">
      <IonFooter v-if="displayFooter">
        <IonToolbar>
          <div id="workoutDetailsToolbar" />
        </IonToolbar>
      </IonFooter>
    </Transition>
  </IonPage>
</template>

<script setup lang="ts">
import {
  IonBackButton,
  IonButton,
  IonButtons,
  IonContent,
  IonFooter,
  IonHeader,
  IonLoading,
  IonPage,
  IonSpinner,
  IonTitle,
  IonToolbar,
  actionSheetController,
  onIonViewDidEnter,
  useIonRouter,
} from "@ionic/vue";
import useWorkoutDeleteMutation from "@shared/mutations/useWorkoutDeleteMutation";
import useWorkoutUpdateMutation from "@shared/mutations/useWorkoutUpdateMutation";
import useWorkoutQuery from "@shared/queries/useWorkoutQuery";
import WorkoutEditor from "@workouts/components/WorkoutEditor";
import { WorkoutModel } from "@workouts/models";
import { computed, ref, toRef } from "vue";

const { id } = defineProps<{ id: number }>();

const { data: workoutModel, isLoading, isError } = useWorkoutQuery(toRef(id));
const { error: deleteError, isPending: isDeleting, mutateAsync: deleteWorkout } = useWorkoutDeleteMutation();
const { error: updateError, isPending: isUpdating, mutateAsync: updateWorkout } = useWorkoutUpdateMutation();

const ionRouter = useIonRouter();

const displayFooter = ref(false);

const handleDelete = async () => {
  const alert = await actionSheetController.create({
    header: "Удалить тренировку?",
    subHeader: "Данные будут безвозвратно удалены!",
    buttons: [
      {
        text: "Отмена",
        role: "cancel",
      },
      {
        text: "Удалить",
        role: "destructive",
        handler: async () => {
          await deleteWorkout(id);
          ionRouter.back();
        },
      },
    ],
  });

  await alert.present();
};

const handleUpdate = (workoutModel: WorkoutModel) => {
  updateWorkout({ workoutId: workoutModel.id!, payload: WorkoutModel.toUpdateParams(workoutModel) });
};

const handleGoBack = async () => {
  const alert = await actionSheetController.create({
    header: "Вернуться назад?",
    subHeader: "Все несохраненные изменения будут потеряны!",
    buttons: [
      {
        text: "Отмена",
        role: "cancel",
      },
      {
        text: "Вернуться",
        role: "confirm",
        handler: () => {
          ionRouter.navigate("/workouts", "back", "replace");
        },
      },
    ],
  });

  await alert.present();
};

const loadingMessage = computed(() => {
  if (isDeleting.value) return "Удаление тренировки...";

  return "Сохранение тренировки...";
});

onIonViewDidEnter(() => (displayFooter.value = true));
</script>
