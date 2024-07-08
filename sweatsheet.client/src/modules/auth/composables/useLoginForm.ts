import { StatusCode } from "@shared/api/apiClient";
import AuthApi from "@shared/api/auth/AuthApi";
import { toTypedSchema } from "@vee-validate/zod";
import { AxiosError } from "axios";
import { useForm } from "vee-validate";
import { z } from "zod";

export default function useLoginForm() {
  const schema = toTypedSchema(
    z.object({
      username: z.string().min(4, "Username must be at least 4 characters"),
      password: z.string().min(6, "Password must be at least 6 characters"),
    }),
  );

  const { handleSubmit, defineField, errors, isSubmitting, setErrors } = useForm({
    validationSchema: schema,
    initialValues: { username: "", password: "" },
  });

  const [username, usernameAttrs] = defineField("username", (state) => ({
    validateOnModelUpdate: state.validated,
  }));

  const [password, passwordAttrs] = defineField("password", (state) => ({
    validateOnModelUpdate: state.validated,
  }));

  const onSubmit = handleSubmit(async (values, actions) => {
    try {
      await new Promise((resolve) => setTimeout(resolve, 1000));
      await AuthApi.signIn(values);

      actions.resetForm();
    } catch (error: unknown) {
      const axiosError = error as AxiosError<string>;

      if (axiosError.response?.status === StatusCode.BadRequest) {
        setErrors({ username: axiosError.response.data, password: axiosError.response.data });
      }
    }
  });

  return {
    formModel: { username, password },
    formAttrs: { usernameAttrs, passwordAttrs },
    isSubmitting,
    errors,
    onSubmit,
  };
}
