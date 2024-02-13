<script setup lang="ts">
import { ref } from "vue";

withDefaults(defineProps<{
  title: string,
  confirmButtonText?: string,
  enableConfirmButton?: boolean
}>(), {
  confirmButtonText: "Ok",
  enableConfirmButton: true
});

const emit = defineEmits<{
  close: [confirm: boolean]
}>();

const theDialog = ref<HTMLDialogElement | null>(null);

function showModal() {
  theDialog.value?.showModal();
}

function closeDialog(success: boolean) {
  theDialog.value?.close();
  emit('close', success);
}

defineExpose({
  showModal,
  closeDialog
});
</script>


<template>
  <dialog ref="theDialog">
    <div class="fixed inset-0 flex items-center justify-center z-50">
      <div class="bg-slate-700 text-white rounded-lg shadow-lg p-2">
        <div class="flex justify-between text-lg mb-2">
          <div class="font-bold">{{ title }}</div>
          <div @click="closeDialog(false)"
            class="text-black cursor-pointer hover:font-bold">&cross;</div>
        </div>
        <div class="my-4">
          <slot></slot>
        </div>
        <div class="flex justify-end"><button
            @click="closeDialog(false)" class="btn">Cancel</button><button
            @click="closeDialog(true)" :disabled="!enableConfirmButton" class="btn btn-primary">{{ confirmButtonText }}</button></div>
      </div>
    </div>
  </dialog>
</template>

<style>
dialog::backdrop {
  @apply backdrop-blur-[2px];
}
</style>

