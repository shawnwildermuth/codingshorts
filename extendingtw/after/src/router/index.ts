import { createRouter, createWebHistory } from "vue-router";
import HeadersView from "@/views/Headers.vue";
import SimpleFormsView from "@/views/SimpleForms.vue";
import NicheFormsView from "@/views/NicheForms.vue";

const routes = [
  {
    path: "/",
    name: "Headers",
    component: HeadersView
  },
  {
    path: "/simple",
    name: "SimpleForm",
    component: SimpleFormsView
  },
  {
    path: "/niche",
    name: "NicheForms",
    component: NicheFormsView
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;