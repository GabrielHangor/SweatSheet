import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { VueQueryPlugin } from "@tanstack/vue-query";
import dayjs from "dayjs";
import duration from "dayjs/plugin/duration";
import ru from "dayjs/locale/ru";

import "./style.css";

dayjs.extend(duration);
dayjs.locale(ru);

const app = createApp(App);

app.use(VueQueryPlugin);

app.use(router);

router.isReady().then(() => app.mount("#app"));
