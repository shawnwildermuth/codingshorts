<script>
import { reactive, defineComponent } from "vue";
import router from "../../router";
import store from "../../store";

export default defineComponent({
  setup() {
    const person = reactive({
      id: 0,
      name: "",
      age: "",
    });

    function save() {
      store.addItem(person);
      router.push("/people");
    }

    function cancel() {
      router.push("/people");
    }

    return {
      person,
      save,
      cancel,
    };
  },
});
</script>

<template>
  <div class="p-2">
    <h3>Editor</h3>
    <form class="border p-1 flex flex-col" novalidate @submit.prevent="save()">
      <label>Name</label>
      <input type="text" v-model="person.name" />
      <label>Age</label>
      <input type="number" v-model="person.age" />
      <div>
        <input type="submit" value="Save" class="mr-2" />
        <button @click="cancel()" class="bg-gray-700 hover:bg-gray-800">
          Cancel
        </button>
      </div>
    </form>
  </div>
</template>
