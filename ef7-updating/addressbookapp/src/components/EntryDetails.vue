<script setup lang="ts">
import { useStore } from '@/store';
import { onMounted, ref, watch } from 'vue';
import type { EntryModel } from '@/models';
import { formatName } from "@/formatters";
import { useRouter } from 'vue-router';
import ConfirmationDialog from "@/components/ConfirmationDialog.vue";
import AddressComponent from "@/components/AddressComponent.vue";
import AddressDialog from './AddressDialog.vue';
import {
  PencilIcon,
  TrashIcon,
  HomeIcon,
  BriefcaseIcon,
  DevicePhoneMobileIcon,
  AtSymbolIcon
} from '@heroicons/vue/24/solid';

const store = useStore();
const currentEntry = ref<EntryModel | undefined>();
const confirmDialog = ref<typeof ConfirmationDialog>();
const addressDialog = ref<typeof AddressDialog>()
const router = useRouter();

const props = defineProps<{
  id: Number
}>();

onMounted(() => {
  updateCurrentEntry();
})

watch(() => props.id,
  () => {
    updateCurrentEntry();
  });

async function updateCurrentEntry() {
  currentEntry.value = await store.getEntryById(props.id);
}

function deleteEntry() {
  confirmDialog.value?.showModal();
}

function editEntry() {
  router.push(`/edit/${currentEntry.value?.id}`);
}

function confirmDelete(confirm: boolean) {
  if (confirm) {
    store.deleteEntry(currentEntry.value!);
    router.push("/");
  }
}

function newAddress() {
  addressDialog.value!.showModal();
}

const genderIcon = new URL("../assets/gender.svg", import.meta.url).href;
</script>

<template>
  <div v-if="!store.isBusy">
    <div class="flex justify-between">
      <h2 class="text-xl font-semibold" v-if="currentEntry">{{
        formatName(currentEntry) }}</h2>
      <div>
        <button class="btn btn-sm btn-warning btn-outline mr-2"
          @click="deleteEntry">
          <trash-icon class="w-5"></trash-icon> Delete
        </button>
        <button class="btn btn-sm btn-primary" @click="editEntry">
          <pencil-icon class="w-5"></pencil-icon> Edit
        </button>
      </div>
    </div>
    <div class="text-lg">
      <div v-if="currentEntry?.companyName"><strong>{{ currentEntry?.companyName
      }}
        </strong></div>
      <div v-if="currentEntry?.homePhone"><home-icon
          class="w-4 inline"></home-icon> <a
          :href="`tel:${currentEntry.homePhone}`">{{ currentEntry?.homePhone
          }}</a>
      </div>
      <div v-if="currentEntry?.workPhone"><briefcase-icon
          class="w-4 inline"></briefcase-icon> <a
          :href="`tel:${currentEntry.workPhone}`">{{ currentEntry?.workPhone
          }}</a>
      </div>
      <div v-if="currentEntry?.cellPhone"><device-phone-mobile-icon
          class="w-4 inline"></device-phone-mobile-icon> <a
          :href="`tel:${currentEntry.cellPhone}`">{{ currentEntry?.cellPhone
          }}</a>
      </div>
      <div v-if="currentEntry?.email"><at-symbol-icon
          class="w-4 inline"></at-symbol-icon> <a
          :href="`mailto:${currentEntry.email}`">{{
            currentEntry?.email }}</a></div>
      <div v-if="currentEntry?.gender">
          <svg id="Glyph" class="w-3 ml-0.5 inline fill-white stroke-none" xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 33.9 50.06">
            <path class="cls-1"
              d="M33.46,3.62c-.06-1.52-1.25-2.75-2.78-2.87L20.67,0c-1.63-.12-3.08,1.14-3.21,2.77s1.14,3.09,2.77,3.21l3.34.25-5.58,5.41c-1.81-1.04-3.86-1.59-5.99-1.59C5.38,10.06,0,15.44,0,22.06c0,5.49,3.74,10.25,9,11.61v4.39h-3c-1.65,0-3,1.34-3,3s1.35,3,3,3h3v3c0,1.65,1.35,3,3,3s3-1.35,3-3v-3h3c1.65,0,3-1.35,3-3s-1.35-3-3-3h-3v-4.39c5.26-1.36,9-6.12,9-11.61,0-2.2-.59-4.33-1.72-6.2l5.48-5.31.14,3.36c.08,1.66,1.48,2.93,3.12,2.87,1.65-.08,2.94-1.48,2.88-3.14l-.44-10.02ZM12,28.06c-3.31,0-6-2.7-6-6s2.69-6,6-6,6,2.69,6,6-2.69,6-6,6Z" />
          </svg>
         {{ currentEntry?.gender }}</div>
      <div v-if="currentEntry?.addresses && currentEntry?.addresses.length > 0"
        class="ml-4 mr-2 my-2">
        <div class="p-2">
          <div class="flex justify-between">
            <h3>Addresses</h3>
            <button class="btn btn-sm btn-primary" @click="newAddress">New
              Address</button>
          </div>
          <div>
            <address-component v-for="address in currentEntry?.addresses"
              :key="address.id" :address="address" :entry="currentEntry">
            </address-component>
          </div>
        </div>
      </div>
    </div>
  </div>
  <confirmation-dialog @close="confirmDelete" ref="confirmDialog">
  </confirmation-dialog>

  <address-dialog v-if="currentEntry" ref="addressDialog" :entry="currentEntry!"
    :address="null">
  </address-dialog>
</template>

<style scoped>
a {
  @apply hover:text-warning text-accent;
}

</style>

