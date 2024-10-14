<script setup lang="ts"></script>

<template>
  <div class="p-2 container mx-auto bg-gray-200">
    <header class="flex justify-between">
      <div class="text-xl font-bold">Extending Vue</div>
      <ul class="flex">
        <li><router-link to="/">Home</router-link></li>
        <li><router-link to="/simple">Simple Forms</router-link></li>
        <li><router-link to="/niche">Niche Forms</router-link></li>
      </ul>
    </header>
    <div class="w-full p-4">
      <router-view />
    </div>
  </div>
</template>

<style scoped>

ul li {
  @apply font-bold p-2 bg-green-800 text-white mr-2 hover:bg-green-700;
}

</style>
