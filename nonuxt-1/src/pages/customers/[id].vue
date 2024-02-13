<script setup>
import { useRoute } from "vue-router"
import { ref, onMounted } from "vue";
import { useHttp } from "@/composables/http";
import { useState } from "@/composables/state"

const route = useRoute();
const item = ref(null);
const http = useHttp();
const state = useState();

onMounted(async () => {
  const result = await http.getCustomer(route.params.id);
  if (result) {
    item.value = result;
  }
  //state.error = "No error, I'm just showing off";
})
</script>
<template>
  <div class="p-2 text-lg">
    <router-link to="/" class="btn btn-sm">&lt;- Back</router-link>
    <div v-if="item" class="card">
      <div class="card-body">
        <div class="card-title">{{ item.companyName }}</div>
        <div>{{ item.contact }}</div>
        <div>{{ item.phoneNumber }}</div>
        <div>{{ item.email }}</div>
        <div v-if="item.address1">{{ item.address1 }}</div>
        <div v-if="item.cityTown">{{ item.cityTown }}, {{ item.stateProvince }} {{
          item.postalCode }}</div>
      </div>
    </div>
  </div>
</template>

<route lang="json">
  {
    "meta": {
      "layout": "yellow"
    }
  }
</route>

<style></style>
