<script>
import { watch, ref, defineComponent } from "vue";
import router from "../../router";
import { useRoute } from "vue-router";
import store from "../../store";

export default defineComponent({
  setup() {
    function close() {
      router.push("/people");
    }

    const route = useRoute();

    watch(
      () => route.params.id, 
      () => person.value = store.findItem(route.params.id));

    const person = ref(store.findItem(route.params.id));

    return {
      person,
      close
    };
  },
});
</script>

<template>
  <div class="p-2">
    <h3>Details</h3>
    <dl>
      <dt class="text-slate-500 ">Name</dt>
      <dd  class="font-bold ml-4">{{ person.name }}</dd>
      <dt class="text-slate-500">Age</dt>
      <dd class="font-bold ml-4">{{ person.age }}</dd>
    </dl>
    <button @click="close()" class="bg-gray-700 hover:bg-gray-800">
      Cancel
    </button>
  </div>
</template>
