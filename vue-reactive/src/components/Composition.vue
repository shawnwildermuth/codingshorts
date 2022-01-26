<script>
import { reactive, ref } from "vue";

export default {
  setup() {
    const name = ref("Shawn");
    const items = reactive([]);

    function add() {
      const tempItems = reactive(items);
      tempItems.push({ title: "A new item" });
    }

    function nameChange() {
      name.value = "New Name";
    }

    return {
      name,
      items,
      add,
      nameChange
    };
  },
};
</script>

<template>
  <div>
    <h1>Composition API</h1>
    <button @click="add()">Click me...</button>
    <button @click="nameChange()">Change the Name</button>
    <div>Name: {{ name }}</div>
    <div v-for="i in items" :key="i.title">
      {{ i.title }}
    </div>
  </div>
</template>
