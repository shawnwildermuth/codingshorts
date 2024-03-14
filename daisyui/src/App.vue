<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div class="bg-black" data-theme="business">
    <header class="flex justify-between bg-base-100 text-base-content p-2">
      <h1 class="font-bold text-xl flex"><app-icon class="mr-2"></app-icon> A Website</h1>
      <div class="flex gap-1">
        <RouterLink class="btn btn-ghost" to="/">Home</RouterLink>
        <RouterLink class="btn btn-ghost"  to="/about">About</RouterLink>
      </div>
    </header>
    <div class="p-2 container mx-auto bg-base-200 text-base-content min-h-screen">
      <RouterView />
    </div>
  </div>
</template>
