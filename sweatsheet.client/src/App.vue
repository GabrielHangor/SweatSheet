<template>
  <k-app theme="ios" safe-areas>
    <router-view />
  </k-app>
</template>

<script setup lang="ts">
import { kApp } from "konsta/vue";
import { RouterView } from "vue-router";
</script>
