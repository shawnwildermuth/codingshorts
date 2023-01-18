<template>
  <div class="flex">
    <h1>Film List</h1>
      <div v-for="i in items" :key="i.id">{{ i.title }} ({{ i.year }})</div>
  </div>
</template>

<script setup>
import { reactive, onMounted } from "vue";

const items = reactive([]);

onMounted(async () => {
  const response = await fetch("http://localhost:8080/api/films");
  if (response.ok) {
    const data = await response.json();
    items.push(...data.results);
  }
});
</script>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
