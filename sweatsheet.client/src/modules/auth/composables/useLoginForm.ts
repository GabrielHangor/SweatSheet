import AuthApi from "@shared/api/auth/AuthApi";
import { AxiosError } from "axios";
import { computed, ref } from "vue";

export default function useLoginForm() {
  const formModel = ref({ username: "", password: "" });

  const isDisabled = computed(() => {
    return !formModel.value.username || !formModel.value.password;
  });

  const isLoading = ref(false);
  const errors = ref({ username: "", password: "" });

  const handleSubmit = async () => {
    try {
      errors.value = { username: "", password: "" };
      isLoading.value = true;
      await new Promise((resolve) => setTimeout(resolve, 1000));

      await AuthApi.signIn(formModel.value);
    } catch (error: unknown) {
      const axiosError = error as AxiosError;

      if (axiosError.response?.status === 400) {
        errors.value.password = "Incorrect username or password";
        errors.value.username = "Incorrect username or password";
      }
    } finally {
      isLoading.value = false;
    }
  };

  return {
    formModel,
    isDisabled,
    isLoading,
    errors,
    handleSubmit,
  };
}
