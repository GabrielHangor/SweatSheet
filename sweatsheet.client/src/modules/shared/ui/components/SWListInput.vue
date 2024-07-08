<template>
  <k-list-input
    v-bind="$props"
    :value="model"
    :clear-button="!!model"
    @clear="model = ''"
    @input="model = $event.target.value"
    @blur="$emit('blur')"
    @change="$emit('change')"
  />
</template>

<script setup lang="ts">
const model = defineModel<string | number | undefined>({ required: true });

defineEmits(["blur", "change", "input"]);
</script>

<script lang="ts">
import { kListInput } from "konsta/vue";
import { defineComponent } from "vue";

export default defineComponent({
  name: "SWListInput",
  components: { kListInput },
  extends: kListInput,
});
</script>
