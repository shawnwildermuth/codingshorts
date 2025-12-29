<script setup lang="ts">

type ErrorList = { _errors: string[] } | undefined;

defineProps<{
  errors: ErrorList
}>()

</script>

<template>
  <div v-if="errors" class="text-error">
    <ul>
      <li v-for="e in errors._errors">{{ e }}</li>
    </ul>
  </div>
</template>
