<script setup lang="ts">
import type { AddressModel, EntryModel } from '@/models';
import ModalDialog from './ModalDialog.vue';
import { useStore } from '@/store';
import type { inferFormattedError } from 'zod';
import { onMounted, ref } from 'vue';
import { cloneDeep } from "lodash";
import ValidationError from "./ValidationError.vue";
import { useAddressSchema } from "@/schemas/address";
const addressSchema = useAddressSchema();


const props = defineProps<{
  entry: EntryModel,
  address: AddressModel | null
}>();

const store = useStore();
const theDialog = ref<typeof ModalDialog>();
const editorAddress = ref<AddressModel>();
const dirty = ref(false);
type ErrorsType = inferFormattedError<typeof addressSchema>;

const errors = ref<ErrorsType>();

onMounted(() => {
  if (!props.address) {// new
    editorAddress.value = {
      id: 0,
      name: "Home",
      line1: "123 Main Street",
      line2: "",
      line3: "",
      cityTown: "Atlanta",
      stateProvince: "GA",
      postalCode: "30303",
      country: ""
    };
  } else {
    const copy = cloneDeep(props.address);
    editorAddress.value = copy;
  }
});

async function onSave(confirm: boolean) {
  if (confirm) {
    const result = addressSchema.safeParse(editorAddress.value);
    if (result.success) {
      // If New
      if (props.address === null) {
        // Add
        await store.saveAddress(props.entry, editorAddress.value!);
      } else {
        // update
        await store.updateAddress(props.entry, editorAddress.value!);
      }
      theDialog.value!.closeDialog(false);
    } else {
      errors.value = result.error.format();
    }
  }
}

function showModal() {
  theDialog.value!.showModal();
}

defineExpose({
  showModal
});
</script>

<template>
  <modal-dialog ref="theDialog" title="Address" confirm-button-text="Save"
    @close="onSave" :enableConfirmButton="dirty">
    <div v-if="editorAddress" class="w-96" >
      <div class="label">Address Name</div>
      <input class="input w-full" placeholder="e.g. Home" v-model="editorAddress.name"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="name" />
      <div class="label">Street Address</div>
      <input class="input w-full" placeholder="e.g. 123 Main St." v-model="editorAddress.line1"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="email" />
      <input class="input w-full" placeholder="e.g. Suite 500" v-model="editorAddress.line2"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="email" />
      <input class="input w-full" placeholder="e.g. 2nd Floor" v-model="editorAddress.line3"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="email" />
      <div class="label">City</div>
      <input class="input w-full" placeholder="e.g. Atlanta" v-model="editorAddress.cityTown"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="cityTown" />
      <div class="label">State/Province</div>
      <input class="input w-full" placeholder="e.g. GA" v-model="editorAddress.stateProvince"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="stateProvince" />
      <div class="label">Postal Code</div>
      <input class="input w-full" placeholder="e.g. 30303" v-model="editorAddress.postalCode"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="postalCode" />
      <div class="label">Country</div>
      <input class="input w-full" placeholder="e.g. United States" v-model="editorAddress.country"
        @input="dirty = true" />
      <ValidationError :errors="errors" fieldName="country" />
  </div>
</modal-dialog></template>
