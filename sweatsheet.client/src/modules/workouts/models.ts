import { Exercise } from "@exercises/models";
import { WorkoutUpdateRequestParams } from "@shared/api/workouts/types";

export class WorkoutModel {
  id?: number;
  key?: string;
  title: string;
  startTime: string;
  endTime: string;
  workoutDurationInSeconds: number;
  totalWeightWorkout: number;
  activities: ActivityModel[];

  constructor(workout: Workout) {
    this.id = workout.id;
    this.key = workout.id ? undefined : crypto.randomUUID();
    this.title = workout.title ?? "";
    this.startTime = workout.startTime ?? new Date().toISOString();
    this.endTime = workout.endTime ?? "";
    this.workoutDurationInSeconds = workout.workoutDurationInSeconds ?? 0;
    this.totalWeightWorkout = workout.totalWeightWorkout ?? 0;
    this.activities = workout.activities.map((activity) => new ActivityModel(activity)) ?? [];
  }

  static toUpdateParams(workout: WorkoutModel): WorkoutUpdateRequestParams {
    return {
      title: workout.title,
      startTime: workout.startTime,
      endTime: workout.endTime,
      activities: workout.activities.map((a) => ({
        exerciseId: a.exercise?.id,
        sets: a.sets.map((s) => ({ weight: s.weight || 0, reps: s.reps || 0 })),
      })),
    };
  }
}

export class ActivityModel {
  id?: number;
  key?: string;
  exercise: Exercise | null;
  sets: ActivitySetModel[];
  totalWeightActivity: number;

  constructor(activity?: Activity) {
    this.id = activity?.id;
    this.key = activity?.id ? undefined : crypto.randomUUID();
    this.exercise = activity?.exercise ?? null;
    this.sets = activity?.sets?.map((set) => new ActivitySetModel(set, activity)) ?? [];
    this.totalWeightActivity = activity?.totalWeightActivity ?? 0;
  }
}

export class ActivitySetModel {
  weight: number;
  reps: number;
  isFinished: boolean;

  constructor(activitySet?: ActivitySet, activity?: Activity) {
    this.weight = activitySet?.weight ?? 0;
    this.reps = activitySet?.reps ?? 0;
    this.isFinished = activity?.id ? true : false;
  }
}

export type Workout = {
  id?: number;
  title: string;
  startTime: string;
  endTime: string;
  workoutDurationInSeconds: number;
  totalWeightWorkout: number;
  activities: Activity[];
};

export type Activity = {
  id?: number;
  exercise: Exercise;
  sets: ActivitySet[];
  totalWeightActivity: number;
};

export type ActivitySet = {
  weight: number;
  reps: number;
};
