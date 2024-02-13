<script setup lang="ts">
import { computed } from 'vue';


const props = defineProps<{
  errors: any,
  fieldName: string
}>();


const errorItems = computed(() => {
  if (props.errors && props.errors[props.fieldName]) {
    return props.errors[props.fieldName]?._errors;
  }
  return [];
});

</script>

<template>
  <div class="label">
  <div class="label-text text-warning" v-if="errorItems">
    <div v-for="e in errorItems" :key="e">
      {{ e }}
    </div>
  </div>
</div>
</template>