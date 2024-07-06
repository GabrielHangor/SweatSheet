<template>
  <h1>Workouts list1</h1>
</template>

<script setup lang="ts">
import useWorkoutsInfiniteQuery from "@shared/queries/useWorkoutsInfiniteQuery";
import { computed } from "vue";

const { data, error, isLoading, fetchNextPage } = useWorkoutsInfiniteQuery();

const hasMoreData = computed(() => data.value?.pages.at(-1)?.hasNext);
</script>
