<template>
  <k-list-input
    v-bind="$props"
    :value="modelValue"
    :error="isDirty ? error : ''"
    @input="handleInput"
    @blur="handleBlur"
  />
</template>

<script lang="ts">
import { kListInput } from "konsta/vue";
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "SWListInput",
  components: { kListInput },
  extends: kListInput,
  props: {
    modelValue: { type: [String, Number], required: true },
  },
  emits: ["update:modelValue"],
  setup(props, { emit }) {
    const isDirty = ref(false);
    const isModified = ref(false);

    function handleInput(event: Event) {
      if (!isModified.value) isModified.value = true;

      const target = event.target as HTMLInputElement;
      emit("update:modelValue", target.value);
    }

    function handleBlur() {
      if (isModified.value) isDirty.value = true;
    }

    return {
      isDirty,
      handleInput,
      handleBlur,
    };
  },
});
</script>
