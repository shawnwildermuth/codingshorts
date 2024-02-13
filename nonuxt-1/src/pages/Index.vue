<script setup>
import { reactive, onMounted} from "vue";
import { useHttp } from "@/composables/http";

const items = reactive([]);
const http = useHttp();

onMounted(async () => {
  const result = await http.getCustomers();
  if (result) items.splice(0, items.length, ...result)
});
</script>

<template>
  <div class="p-1 bg-base-300">
  <h1 class="text-lg font-bold">Customers</h1>
  <ul class="menu">
  <li v-for="i in items">
    <router-link :to="'/customers/' + i.id">{{ i.companyName }}</router-link>
  </li>
</ul>
</div>
</template>
