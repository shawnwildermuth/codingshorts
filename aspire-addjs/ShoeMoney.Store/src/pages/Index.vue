<script setup lang="ts">
import { useCatalog } from '@/stores/catalog';
import { computed, onMounted, ref } from 'vue';
import { money } from "@/filters";
import ProductModal from "@/components/ProductModal.vue";
import type { Product } from '@/models';

const catalogStore = useCatalog();

const categorySelect = ref<HTMLSelectElement|null>(null);
const productModal = ref<typeof ProductModal>();

onMounted(async () => {
  await catalogStore.loadProducts();
  await catalogStore.loadCategories();
})

function computePager(currentPage: number, totalPages: number): number[] {
  let start = currentPage < 5 ? 1 : currentPage - 4;
  let end = start + 8;

  if (end > totalPages) {
    end = totalPages;
    start = end - 8 > 0 ? end - 8 : 1;
  }

  const pager = Array.from({length: end - start + 1}, (_, i) => start + i);
  return pager;
}

const pager = computed(() => computePager(catalogStore.currentPage, catalogStore.totalPages));

async function loadPage(page: number) {
  const cat: string = categorySelect.value?.value ?? "0";
  if (cat === "0") {
    await catalogStore.loadProducts(page);
  } else {
    await catalogStore.loadProductsByCategory(cat, page);
  }
}

function purchase(product: Product) {
  productModal.value?.showModal(product);
}

</script>

<template>
  <div>
    <product-modal ref="productModal" />
    <div class="flex justify-end my-2">
      <select class="select select-xs select-primary mr-1" ref="categorySelect" @change="loadPage(1)">
        <option value="0" selected>All</option>
        <option v-for="c in catalogStore.categories" :value="c.id">{{ c.name }}</option>
      </select>
    </div>
    <div class="grid gap-2 grid-cols-1 md:grid-cols-3 xl:grid-cols-6">
      <div v-for="p in catalogStore.products"
        class="card card-compact bg-base-100 shadow-xl">
        <figure><img :src="p.imageUrl!" :alt="p.title" /></figure>
        <div class="card-body">
          <h2 class="card-title">{{ p.title }}</h2>
          <div class="flex flex-wrap gap-1 text-white">
            <div class="badge badge-ghost">{{ p.category?.name }}</div>
            <div class="badge badge-ghost">{{ p.color }}</div>
            <div class="badge badge-ghost">{{ p.gender }}</div>
            <div class="badge badge-ghost">{{ p.usage }}</div>
          </div>
          <div class="card-actions justify-end">
            <button class="btn btn-success" @click="purchase(p)">{{ money(p.price) }}</button>
          </div>
        </div>
      </div>
    </div>
    <div class="flex justify-center p-2" v-if="catalogStore.products">
      <div class="join">
        <button class="join-item btn btn-sm" @click="loadPage(catalogStore.currentPage - 1)"
          :class="{ 'btn-disabled': catalogStore.currentPage === 1 }"><chevron-left-icon /></button>
        <button class="join-item btn btn-sm" :class="{ 'font-bold': catalogStore.currentPage === num}" @click="loadPage(num)" v-for="num in pager">{{ num }}</button>
        <button class="join-item btn btn-sm" @click="loadPage(catalogStore.currentPage + 1)"
          :class="{ 'btn-disabled': catalogStore.currentPage === catalogStore.totalPages }"><chevron-right-icon /></button>
      </div>
    </div>
  </div>
</template>