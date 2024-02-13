<template>
  <div class="p-2">
    <h1 class="text-lg font-bold">Login</h1>
    <button class="btn" @click="buttonClick">Login</button>
  </div>
</template>


<script setup>
import { useRouter } from "vue-router";
import { useState } from "@/composables/state"

const router = useRouter();
const state = useState();

function buttonClick() {
  state.loggedIn = true;
  router.push("/");
}

</script>