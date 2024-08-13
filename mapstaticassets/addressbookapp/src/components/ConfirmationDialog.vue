<script setup lang="ts">
import { ref } from 'vue';
import ModalDialog from './ModalDialog.vue';

const emit = defineEmits<{
  close: [confirm: boolean]
}>();

const theDialog = ref<typeof ModalDialog>();

function onClose(confirm: boolean) {
  emit("close", confirm);
}

function showModal() {
  theDialog.value!.showModal();
}

defineExpose({
  showModal
})
</script>

<template>
  <modal-dialog title="Are you sure?" 
    ref="theDialog"
    confirm-button-text="Yes"
    @close="onClose">
    <div>Please confirm your choice.</div>
  </modal-dialog>
</template>

