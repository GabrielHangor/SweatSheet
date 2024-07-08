<template>
  <k-page>
    <k-navbar title="Login" class="sticky top-0" />
    <div class="relative flex min-h-full items-center justify-center">
      <k-block class="mt-[-40px] w-full">
        <k-list strong class="rounded-lg">
          <SWListInput
            v-model="username"
            label="Login"
            type="text"
            placeholder="Login"
            input-class="text-xl"
            v-bind="usernameAttrs"
            :error="errors.username"
          />
          <SWListInput
            v-model="password"
            label="Password"
            type="password"
            placeholder="Password"
            input-class="text-xl"
            v-bind="passwordAttrs"
            :error="errors.password"
          />
          <SWButton
            class="mt-3 rounded-t-none py-6 text-xl"
            :is-loading="isSubmitting"
            tonal
            @click="onSubmit"
          >
            Log in
          </SWButton>
        </k-list>
      </k-block>
    </div>
  </k-page>
</template>

<script setup lang="ts">
import useLoginForm from "@auth/composables/useLoginForm";
import SWButton from "@shared/ui/components/SWButton.vue";
import SWListInput from "@shared/ui/components/SWListInput.vue";
import { kBlock, kList, kNavbar, kPage } from "konsta/vue";

const {
  formModel: { username, password },
  formAttrs: { usernameAttrs, passwordAttrs },
  isSubmitting,
  errors,
  onSubmit,
} = useLoginForm();
</script>
