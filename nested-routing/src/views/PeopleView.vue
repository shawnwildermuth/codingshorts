<script>
import { defineComponent } from "vue";
import router from "../router";
import store from "../store";

export default defineComponent({
  setup() {

    function showDetails(id) {
      router.push(`/people/details/${id}`);
    }

    return {
      store,
      showDetails
    };
  },
});
</script>

<template>
  <h1>People</h1>
  <p>Sample Page of simple data</p>
  <div class="flex">
    <div class="w-2/3">
      <router-link to="/people/editor" class="button">Add New</router-link>
      <div v-for="s in store.items" :key="s.id" class="even:bg-blue-50 border">
        <div class="p-2 hover:bg-blue-100" @click="showDetails(s.id)">
          Name: {{ s.name }} ({{ s.age }})
        </div>
      </div>
    </div>
    <div class="w-1/3 border">
      <router-view></router-view>
    </div>
  </div>
</template>
