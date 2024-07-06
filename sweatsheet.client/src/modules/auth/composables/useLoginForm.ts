import { computed, ref } from "vue";

export default function useLoginForm() {
  const formModel = ref({ login: "", password: "" });

  const isDisabled = computed(() => {
    return !formModel.value.login || !formModel.value.password;
  });

  const handleSubmit = () => {
    console.log("formModel", formModel.value);
  };

  return {
    formModel,
    isDisabled,
    handleSubmit
  };
}
  