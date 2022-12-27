<script>
import { defineComponent } from "vue";
import store from "../store";

export default defineComponent({
  setup() {
    return {
      store
    }
  }
})
</script>

<template>
  <h1>Nested Routing</h1>
  <p>This is just a simple example of how nested routing works.</p>
</template>