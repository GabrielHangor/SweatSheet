<template>
  <IonCard v-for="(i, idx) in 8" :key="idx">
    <IonSkeletonText class="m-0 w-full h-[130px]" animated />
  </IonCard>
</template>

<script setup lang="ts">
import { IonCard, IonSkeletonText } from "@ionic/vue";
</script>
