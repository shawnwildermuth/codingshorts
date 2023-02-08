<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
  <div class="container mx-auto bg-slate-100 font-sans">
    <header class="flex justify-between bg-black text-white p-4">
      <div class="font-bold text-xl"><a href="/">Customer List</a></div>
      <nav>
        <RouterLink to="/">Home</RouterLink>
      </nav>
    </header>
    <RouterView />
  </div>
</template>

<style scoped>
nav a {
  @apply text-lg 
    font-bold 
    rounded 
    bg-blue-700 
    text-white
    hover:bg-blue-500 
    border 
    border-blue-900 
    hover:border-blue-500 
    p-2 
    mx-1;
}
</style>
