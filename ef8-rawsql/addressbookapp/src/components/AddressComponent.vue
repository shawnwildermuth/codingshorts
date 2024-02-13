<script setup lang="ts">
import type { AddressModel, EntryModel } from '@/models';
import { useStore } from "@/store";
import { ref } from "vue";
import AddressDialog from './AddressDialog.vue';
import ConfirmationDialog from "./ConfirmationDialog.vue";

const store = useStore();
const confirmDialog = ref<typeof ConfirmationDialog>();
const addressDialog = ref<typeof AddressDialog>();

const props = defineProps<{
  address: AddressModel,
  entry: EntryModel
}>();

function editAddress() {
  addressDialog.value!.showModal();
}

function deleteAddress() {
  confirmDialog.value!.showModal();
}

function getGoogleMapsLink() {
  const urlAddress = `${props.address.line1},+${props.address.cityTown},+${props.address.stateProvince}+${props.address.postalCode}+${props.address.country}`;
  return `https://www.google.com/maps/place/${urlAddress.replace(" ", "+")}`;
}

function confirmDelete(confirm: boolean) {
  if (confirm) {
    store.deleteAddress(props.entry, props.address);  
  }
}

</script>

<template>
  <div v-if="address" class="card my-4 rounded">
    <div class="card-body bg-base-300 shadow-xl hover:bg-base-200">
    <div class="flex justify-between card-title">
      <div class="text-md mb-2">{{ address.name }}</div>
      <div>
        <button @click="deleteAddress"
          class="btn btn-xs btn-warning mr-1">Delete</button>
        <button @click="editAddress" class="btn btn-xs btn-primary">Edit</button>
      </div>
    </div>
    <div class="flex flex-col text-sm">
    <div>{{ address.line1 }}</div>
    <div v-if="address.line2">{{ address.line2 }}</div>
    <div v-if="address.line3">{{ address.line3 }}</div>
    <div v-if="address.cityTown">{{ address.cityTown }},
      {{ address.stateProvince }} {{ address.postalCode }}
    </div>
    <div v-if="address.country">{{ address.country }}</div>
    <div><a :href="getGoogleMapsLink" target="_blank">Map</a></div>
  </div>
  </div>
  </div>
  <confirmation-dialog @close="confirmDelete" ref="confirmDialog">
  </confirmation-dialog>
  <address-dialog ref="addressDialog" :address="address" :entry="entry">
  </address-dialog>
</template>

<style scoped>a {
  @apply text-blue-300 hover:underline;
}</style>