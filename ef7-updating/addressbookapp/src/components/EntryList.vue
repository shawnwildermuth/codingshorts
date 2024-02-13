<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import { useStore } from "@/store";
import type { EntryLookupModel } from "@/models";
import { useRouter } from "vue-router";
import { XMarkIcon, HomeIcon, DocumentPlusIcon } from "@heroicons/vue/24/solid"

const store = useStore();
const router = useRouter();
const currentId = ref(0);

function onSelected(item: EntryLookupModel) {
  router.push(`/details/${item.id}`);
  currentId.value = item.id;
}

onMounted(async () => {
  await store.loadLookupList();
  onSelected(store.entries[0]);
})

watch(router.currentRoute, () => {
  if (router.currentRoute.value.name === "home") {
    currentId.value = 0;
  }
})

</script>

<template>
  <div class="py-2 px-1 text-gray-300 text-lg">
    <div  class="text-base">
      <ul class="breadcrumbs"><li><home-icon class="w-5 mb-1 inline hover:text-yellow-200"></home-icon> <a  class="hover:text-yellow-200" href="/">Home</a></li></ul>
      <ul class="breadcrumbs"><li><document-plus-icon class="w-5 mb-1 inline hover:text-yellow-200"></document-plus-icon> <a href="/edit/-1" class="hover:text-yellow-200">New Address</a></li></ul>
    </div>
      <div class="join w-full my-1">
      <input class="input join-item caret-neutral text-sm" placeholder="Search..."
        v-model="store.filter" />
      <button class="btn join-item border-0 bg-neutral" @click="store.filter = ''"><x-mark-icon
          class="w-5"></x-mark-icon></button>
    </div>
    <div class="h-[calc(100vh-14rem)] overflow-y-scroll bg-yellow">
      <ul class="menu text-lg">
        <li v-for="e in store.entryList" :key="e.id">
          <div @click="onSelected(e)"
      :class="{ 'text-white': currentId === e.id, 'font-bold': currentId === e.id }"            
            >{{ e.displayName
            }}</div>
        </li>
      </ul>
    </div>
</div>
</template>