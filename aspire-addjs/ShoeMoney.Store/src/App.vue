<script setup lang="ts">
import { watch } from 'vue';
import { useStore } from './stores';
import { useCart } from "./stores/cart"

const store = useStore();
const cart = useCart();

let timeoutId = 0;

// Clear after five seconds
watch(() => store.error,
  () => {
    if (store.error) {
      timeoutId = setTimeout(() => {
        store.error = "";
      }, 5000)
    }
  })

function closeError() {
  clearTimeout(timeoutId);
  store.error = "";
}
</script>

<template>
  <div data-theme="nord" class="bg-slate-200">
    <div v-if="store.isBusy"
      class="absolute w-full h-screen z-50 flex justify-center items-center bg-gray-200/75">
      <span class="loading loading-ring loading-lg"></span>
    </div>
    <header class="bg-slate-700 p-2 text-white">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="font-bold text-2xl"><router-link
              to="/"><logo-icon></logo-icon> Shoe Money Tonight</router-link>
          </h1>
          <div class="text-sm italic">Put 'em on your feet!</div>
        </div>
        <div class="flex">
          <router-link to="/checkout" title="Checkout"
            class="whitespace-nowrap flex self-center gap-1 p-1 mx-1"
            v-if="cart.items.length > 0">
            <cart-icon />
            <div> ({{ cart.items.length }})</div>
          </router-link>
          <router-link to="/" class="btn btn-neutral">Home</router-link>
        </div>
      </div>
    </header>

    <div class="flex-grow p-1">
      <transition enter-active-class="duration-500 ease-out"
        enter-from-class="transform opacity-0" enter-to-class="opacity-100"
        leave-active-class="duration-300 ease-in" leave-from-class="opacity-100"
        leave-to-class="transform opacity-0">
        <div role="alert" class="alert p-1 alert-warning shadow-sm"
          v-if="store.error">
          <info-icon />
          <div>
            <h3 class="font-bold">Error!</h3>
            <div class="text-xs">{{ store.error }}</div>
          </div>
          <div @click="closeError">
            <delete-icon />
          </div>
        </div>
      </transition>
      <main class="mx-auto p-2 bg-base-100">
        <router-view></router-view>
      </main>
    </div>
  </div>
</template>
