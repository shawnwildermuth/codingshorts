<script setup lang="ts">
  import EntryList from "@/components/EntryList.vue";
  import { useStore } from "@/store";
  import { ChevronDoubleLeftIcon, ChevronDoubleRightIcon } from "@heroicons/vue/24/solid";
  import { ref } from "vue";

  const store = useStore();
  const showSidebar = ref(true);

  function toggleSidebar() {
    showSidebar.value = !showSidebar.value;
  }
</script>

<template>
  <div class="dark w-full bg-base-100 text-gray-100 h-screen font-light flex" data-theme="business">
    <section id="sidebar" class="bg-neutral/50 w-80 p-2 transition-all ease-in-out duration-500" :class="{ '-ml-[18rem]': !showSidebar }">
      <div class="flex justify-between w-full">
        <h1 class="text-2xl font-bold">
          <router-link to="/">
            Your Address
            Book
          </router-link>
        </h1>
        <div>
          <chevron-double-left-icon v-if="showSidebar"
                                    class="cursor-pointer w-6 hover:text-yellow-400"
                                    @click="toggleSidebar"></chevron-double-left-icon>
          <chevron-double-right-icon v-if="!showSidebar"
                                     class="cursor-pointer w-6 hover:text-yellow-400"
                                     @click="toggleSidebar"></chevron-double-right-icon>
        </div>
      </div>

      <div class="mr-6">
        <entry-list />
      </div>
    </section>

    <section class="flex-grow">

      <div class="flex gap-2 h-[calc(100vh-5rem)]">
        <div class="p-2 flex-grow">
          <div class="bg-warning w-full p-2 text-xl" v-if="store.errorMessage">
            {{
              store.errorMessage
            }}
          </div>
          <div class="bg-primary w-full p-2 text-xl" v-if="store.isBusy">
            Loading...
          </div>
          <router-view></router-view>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped>
</style>
