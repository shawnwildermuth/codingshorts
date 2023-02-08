<script setup lang="ts">
import type Customer from "@/models/Customer";
import { onMounted, reactive } from "vue";

const customers = reactive(new Array<Customer>());

onMounted(async () => {
  const response = await fetch("/api/customers");
  if (response.status === 200) {
    const result = await response.json();
    customers.splice(0, customers.length, ...result);
  } else {
    throw "Failed to load customers...";
  }
});
</script>

<template>
  <main class="p-2">
    <h1 class="text-2xl font-bold">Customer List</h1>
    <div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-2">
      <div
        v-for="c in customers"
        :key="c.id"
        class="p-1 rounded border border-slate-300 bg-slate-200 hover:bg-slate-300 cursor-pointer"
      >
        <div class="text-lg font-bold">{{ c.companyName }}</div>
        <div class="pl-2">
          <div>{{ c.contact }} <span v-if="c.phone">- <a class="text-blue-500 hover:font-bold" :href="'tel:' + c.phone">{{ c.phone }}</a></span></div>
          <div>{{ c.address1 }}</div>
          <div v-if="c.address2">{{ c.address2 }}</div>
          <div v-if="c.address3">{{ c.address3 }}</div>
          <div>{{ c.city }}, {{  c.state }}  {{  c.postalCode }}</div>
          <div><strong>Credit Limit:</strong> ${{ c.creditLimit  }}</div>
        </div>
      </div>
    </div>
  </main>
</template>
