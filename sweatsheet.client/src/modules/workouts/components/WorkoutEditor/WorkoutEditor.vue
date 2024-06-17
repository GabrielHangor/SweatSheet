<template>
  <IonList>
    <WorkoutEditorDetails v-model="workoutModel" />

    <section>
      <template
        v-for="(activity, activityIndex) in workoutModel.activities"
        :key="activity?.id ?? activity.key"
      >
        <table class="my-5 w-full table-auto">
          <thead>
            <tr>
              <th class="w-auto pl-4 text-left text-xl text-blue-500">
                <IonSelect
                  v-model="activity.exercise"
                  :compare-with="
                    (first, second) => (first?.id && second?.id ? first.id === second.id : first === second)
                  "
                  placeholder="Выберите упражнение"
                  interface="action-sheet"
                >
                  <IonSelectOption v-for="exercise in exercises" :key="exercise.id" :value="exercise">
                    {{ exercise.exerciseTitle }} ({{ exercise.primaryMuscleGroupTitle }})
                  </IonSelectOption>
                </IonSelect>
              </th>
              <th class="pr-4 text-right">
                <IonIcon
                  :icon="closeCircleOutline"
                  class="p-2 align-middle"
                  size="large"
                  color="danger"
                  @click="deleteActivity(activityIndex)"
                />
                <IonBadge class="rounded p-2 align-middle">{{ activity.totalWeightActivity }} кг</IonBadge>
              </th>
            </tr>
          </thead>
        </table>

        <table class="w-full table-fixed border-collapse">
          <thead>
            <tr>
              <th>Подход</th>
              <th>Кг</th>
              <th>Повторы</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(set, idx) in activity.sets"
              :key="`${activity?.id ?? activity.key}-${idx}`"
              :class="set.isFinished ? 'bg-green-50' : ''"
            >
              <td class="text-center">{{ idx + 1 }}</td>
              <td class="text-center">
                <IonInput
                  v-model="set.weight"
                  class="!min-h-11 !px-2"
                  type="tel"
                  :maxlength="3"
                  mode="md"
                  :disabled="set.isFinished"
                  :fill="set.isFinished ? undefined : 'outline'"
                />
              </td>
              <td class="text-center">
                <IonInput
                  v-model="set.reps"
                  class="!min-h-11 !px-2"
                  type="tel"
                  :maxlength="3"
                  mode="md"
                  :disabled="set.isFinished"
                  :fill="set.isFinished ? undefined : 'outline'"
                />
              </td>
              <td class="text-center">
                <IonIcon
                  :icon="closeCircleOutline"
                  class="p-2 align-middle"
                  size="large"
                  color="danger"
                  @click="deleteSet(activity, idx)"
                />
                <IonCheckbox v-model="set.isFinished" class="p-2 align-middle" />
              </td>
            </tr>
          </tbody>
        </table>
        <IonButton class="mx-1 my-4" expand="block" color="tertiary" @click="addNewSet(activity)">
          Добавить подход
        </IonButton>
      </template>

      <IonButton class="mx-1 my-4" expand="block" color="success" @click="addNewActivity">
        Добавить упражнение
      </IonButton>
    </section>

    <Teleport v-if="displayFooter" to="#workoutDetailsToolbar">
      <IonButton expand="block" @click="emit('workout:update', workoutModel)">Сохранить</IonButton>
    </Teleport>
  </IonList>
</template>

<script setup lang="ts">
import {
  IonBadge,
  IonButton,
  IonCheckbox,
  IonIcon,
  IonInput,
  IonList,
  IonSelect,
  IonSelectOption,
  actionSheetController,
} from "@ionic/vue";
import useExercisesQuery from "@shared/queries/useExercisesQuery";
import WorkoutEditorDetails from "@workouts/components/WorkoutEditor/WorkoutEditorDetails.vue";
import { ActivityModel, ActivitySetModel, WorkoutModel } from "@workouts/models";
import { closeCircleOutline } from "ionicons/icons";
import { ref, toRaw, watch } from "vue";

type Props = {
  workout: WorkoutModel;
  displayFooter: boolean;
};

const { data: exercises } = useExercisesQuery();

const props = defineProps<Props>();

const emit = defineEmits<{ "workout:update": [workout: WorkoutModel] }>();

const workoutModel = ref(structuredClone(toRaw(props.workout)));

watch(
  () => props.workout,
  (newWorkout) => (workoutModel.value = structuredClone(toRaw(newWorkout))),
);

const addNewSet = (activity: ActivityModel) => activity.sets.push(new ActivitySetModel());

const deleteSet = async (activity: ActivityModel, idx: number) => {
  const alert = await actionSheetController.create({
    header: "Удалить подход?",
    buttons: [
      {
        text: "Отмена",
        role: "cancel",
      },
      {
        text: "Удалить",
        role: "destructive",
        handler: () => {
          activity.sets.splice(idx, 1);
        },
      },
    ],
  });

  await alert.present();
};

const addNewActivity = () => workoutModel.value.activities.push(new ActivityModel());

const deleteActivity = async (idx: number) => {
  const alert = await actionSheetController.create({
    header: "Удалить упражнение?",
    buttons: [
      {
        text: "Отмена",
        role: "cancel",
      },
      {
        text: "Удалить",
        role: "destructive",
        handler: () => {
          workoutModel.value.activities.splice(idx, 1);
        },
      },
    ],
  });

  await alert.present();
};
</script>
