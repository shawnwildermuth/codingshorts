import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import './site.src.css'

createApp(App)
  .use(router)
  .mount("#app");
