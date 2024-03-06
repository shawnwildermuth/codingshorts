<script setup lang="ts">
</script>

<template>
  <div class="w-full bg-gray-900 text-slate-100 h-screen p-2">

    <header class="flex justify-between gap-1 p-2">
      <div>
        <h1>Auth Sample</h1>
      </div>
      <div>
        <router-link class="mr-1 btn" to="/">Home</router-link>
        <router-link class="mr-1 btn" to="/data">Data</router-link>
      </div>
    </header>

    <main>
      <router-view></router-view>
    </main>
  </div>
</template>

