<template>
  <div class="sw-loader__container" role="alert" aria-busy="true" aria-live="polite">
    <svg
      class="sw-loader py-1"
      :width="width"
      :height="height"
      viewBox="0 0 50 50"
      xmlns="http://www.w3.org/2000/svg"
    >
      <circle class="path" cx="25" cy="25" r="20" fill="none" :stroke-width="strokeWidth"></circle>
    </svg>
  </div>
</template>

<script setup lang="ts">
interface SWLoadingSpinnerProps {
  width?: string;
  height?: string;
  strokeColor?: string;
  strokeWidth?: string;
}

const props = withDefaults(defineProps<SWLoadingSpinnerProps>(), {
  width: "50px",
  height: "50px",
  strokeColor: "rgb(var(--k-color-primary) / var(--tw-text-opacity))",
  strokeWidth: "5px",
});
</script>

<style>
.sw-loader__container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;

  & .sw-loader {
    animation: rotate 1s linear infinite;
  }

  & .path {
    stroke: v-bind("strokeColor");
    stroke-linecap: round;
    animation: dash 1.5s ease-in-out infinite;
  }
}

@keyframes rotate {
  100% {
    transform: rotate(360deg);
  }
}

@keyframes dash {
  0% {
    stroke-dasharray: 1, 150;
    stroke-dashoffset: 0;
  }
  50% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -35;
  }
  100% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -124;
  }
}
</style>
