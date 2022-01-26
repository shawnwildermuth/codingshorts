<script>
export default {
  data: function () {
    return {
      name: "Shawn",
      items: [],
    };
  },
  methods: {
    add() {
      this.items.push({ title: "A new item" });
    },
    nameChange() {
      this.name = "New Name";
    }
  },
};
</script>

<template>
  <div>
    <h1>Options API</h1>
    <button @click="add()">Click me...</button>
    <button @click="nameChange()">Change the Name</button>
    <div>Name: {{ name }}</div>
    <div v-for="i in items" :key="i.title">
      {{ i.title }}
    </div>
  </div>
</template>
