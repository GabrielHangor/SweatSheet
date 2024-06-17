import { ActivitySet } from "@workouts/models";

export type WorkoutCreateRequestParams = {
  title: string;
  startTime: string;
  endTime: string;
  activities: ActivityCreateRequestParams[];
};

export type WorkoutUpdateRequestParams = WorkoutCreateRequestParams;

export type ActivityCreateRequestParams = {
  exerciseId?: number;
  sets: ActivitySet[];
};
