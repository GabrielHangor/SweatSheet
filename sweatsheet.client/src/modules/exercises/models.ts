export enum MuscleGroup {
  Chest,
  Back,
  Shoulders,
  Biceps,
  Triceps,
  Legs,
}

export type Exercise = {
  id: number;
  primaryMuscleGroup: MuscleGroup;
  primaryMuscleGroupTitle: string;
  exerciseTitle: string;
};