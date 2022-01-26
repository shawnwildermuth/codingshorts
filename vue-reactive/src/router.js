import { createRouter, createWebHistory } from "vue-router";
import Composition from "./components/Composition.vue";
import Options from "./components/Options.vue";

const routes = [
  {
    path: "/",
    component: Options
  },
  { path: "/composition", component: Composition}
];

const router = createRouter({history: createWebHistory(), routes});

export default router;