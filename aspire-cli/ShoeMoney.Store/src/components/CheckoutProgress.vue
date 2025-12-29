<script setup lang="ts">

defineProps({
  stage: {
    type: Number,
    required: true
  }
});

</script>

<template>
  <div class="flex justify-center mb-8">
    <ul class="steps">
      <li class="step" :class="{ 'step-primary': stage > 0 }">Checkout</li>
      <li class="step" :class="{ 'step-primary': stage > 1 }">Customer
        Information</li>
      <li class="step" :class="{ 'step-primary': stage > 2 }">Payment</li>
      <li class="step" :class="{ 'step-primary': stage > 3 }">Confirmation</li>
    </ul>
  </div>
</template>

<style scoped>
 .steps .step-primary:after {
  @apply text-white;
}

</style>