import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import AboutView from "../views/AboutView.vue";
import PeopleView from "../views/PeopleView.vue";
import EditorView from "../views/people/EditorView.vue";
import DetailsView from "../views/people/DetailsView.vue";

const routes = [
  {
    path: "/",
    component: HomeView,
  },
  {
    path: "/about",
    component: AboutView,
  },
  {
    path: "/people",
    component: PeopleView,
    children: [
      {
        path: "editor",
        component: EditorView,
      },
      {
        path: "details/:id",
        component: DetailsView

      }
    ],
  },
];

export default createRouter({
  routes,
  history: createWebHistory(),
});
