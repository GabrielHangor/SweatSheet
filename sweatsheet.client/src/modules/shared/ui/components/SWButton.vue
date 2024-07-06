<template>
  <k-button
    :class="{ 'pointer-events-none': disabled || isLoading }"
    v-bind="$props"
    @click="$emit('click', $event)"
  >
    <SWLoadingSpinner v-if="isLoading" />
    <slot v-else />
  </k-button>
</template>

<script lang="ts">
import SWLoadingSpinner from "@shared/ui/components/SWLoadingSpinner.vue";
import { kButton } from "konsta/vue";
import { defineComponent } from "vue";

export default defineComponent({
  name: "SWButton",
  components: {
    kButton,
    SWLoadingSpinner,
  },
  extends: kButton,
  props: {
    isLoading: { type: Boolean, default: false },
  },
  emits: ["click"],
});
</script>
