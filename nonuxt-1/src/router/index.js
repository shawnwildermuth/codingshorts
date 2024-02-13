import { createRouter, createWebHistory } from 'vue-router'
//import HomeView from '../views/HomeView.vue'
import { useState } from "@/composables/state"
import originalRoutes from "~pages";
import { setupLayouts } from "virtual:generated-layouts";

const state = useState();

const routes = setupLayouts(originalRoutes);

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
  // routes: [
  //   {
  //     path: '/',
  //     name: 'home',
  //     component: HomeView
  //   },
  //   {
  //     path: '/customers/:id',
  //     name: 'customers',
  //     // route level code-splitting
  //     // this generates a separate chunk (About.[hash].js) for this route
  //     // which is lazy-loaded when the route is visited.
  //     component: () => import('../views/CustomersView.vue')
  //   },
  //   {
  //     path: '/about',
  //     name: 'about',
  //     // route level code-splitting
  //     // this generates a separate chunk (About.[hash].js) for this route
  //     // which is lazy-loaded when the route is visited.
  //     component: () => import('../views/About.vue'),
  //     meta: {
  //       requiresAuth: true
  //     }
  //   },
  //   {
  //     path: '/login',
  //     name: 'login',
  //     // route level code-splitting
  //     // this generates a separate chunk (About.[hash].js) for this route
  //     // which is lazy-loaded when the route is visited.
  //     component: () => import('../views/Login.vue')
  //   }
  // ]
})

router.beforeEach((to) => {
  if (!state.loggedIn && to.meta.requiresAuth) {
    return { path: "/Login" };
  }
  return true;
});

export default router
