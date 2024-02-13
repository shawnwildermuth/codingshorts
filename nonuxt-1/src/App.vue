<script setup>
import Error from "@/components/Error.vue";
import { useState } from "@/composables/state"
import { useRouter } from "vue-router";

const state = useState();
const router = useRouter();

function logout() {
  state.loggedIn = false;
  router.push("/");
}
</script>

<template>
  <div data-theme="dark">
    <header class="navbar bg-neutral text-neutral-content">
      <div class="flex justify-between w-full">
        <h1 class="font-bold text-xl"><a href="/">Customer List</a></h1>
        <nav>
          <a @click="state.error = 'Test Error'" class="btn btn-sm btn-ghost rounded-none">Test Error
          </a>
          <a @click="logout" v-if="state.loggedIn" class="btn btn-sm btn-ghost rounded-none">Logout
          </a>
          <RouterLink to="/login" v-if="!state.loggedIn" class="btn btn-sm btn-ghost rounded-none">Login
          </RouterLink>
          <RouterLink to="/" class="btn btn-sm btn-ghost rounded-none">Home
          </RouterLink>
          <RouterLink to="/about" class="btn btn-sm btn-ghost rounded-none">About
          </RouterLink>
        </nav>
      </div>
    </header>
    <error :error-message="state.error" />
    <RouterView />
  </div>
</template>

<style scoped>

</style>
