<script setup>
import { useState } from "@/composables/state"

const state = useState();

defineProps({
  "errorMessage": {
    type: String
  }
})

function close() {
  state.error = "";
  return false;
}

</script>
<template>
  <div class="toast z-50" v-if="errorMessage">
    <div class="alert alert-info gap-1 justify-between">

      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
        strokeWidth="{1.5}" stroke="currentColor"
        class="stroke-current shrink-0 w-6 h-6">
        <path strokeLinecap="round" strokeLinejoin="round"
          d="m11.25 11.25.041-.02a.75.75 0 0 1 1.063.852l-.708 2.836a.75.75 0 0 0 1.063.853l.041-.021M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9-3.75h.008v.008H12V8.25Z" />
      </svg><span>{{ errorMessage }}</span>
      <svg xmlns="http://www.w3.org/2000/svg" @click.stop="close" fill="none"
        viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor"
        class="w-4 h-4 cursor-pointer">
        <path stroke-linecap="round" stroke-linejoin="round"
          d="M6 18 18 6M6 6l12 12" />
      </svg>

    </div>
  </div>
</template>