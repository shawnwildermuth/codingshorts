<script setup lang="ts">
import { stateList } from '@/lookups/states';
import { OrderSchema, type OrderErrors } from '@/models/Order';
import { useStore } from '@/stores';
import { useCart } from '@/stores/cart';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();
const cart = useCart();
const errors = ref<OrderErrors | null>(null);
const dirty = ref(false);

onMounted(() => {
  if (!cart.isOrderValid()) router.push("/checkout");
});

function validate() {
  dirty.value = true;
  errors.value = null;

  const valid = OrderSchema.safeParse(cart.order);
  if (!valid.success) errors.value = valid.error.format();
  return valid.success;
}

function handleFocusOut() {
  if (dirty.value) validate();
}

function save() {
  if (validate()) {
    if (cart.processCustomer()) {
      router.push("/payment")
    } else {
      store.error = "Could not save payment information.";
    }
  }
}

</script>

<template>
  <div class="w-full sm:w-4/5 lg:w-2/3 mx-auto">
    <checkout-progress :stage="2" />
    <div class="flex justify-end gap-2 mb-4">
        <button @click="router.back()"
          class="whitespace-nowrap btn btn-sm btn-ghost"><chevron-left-icon />
          Back</button>
        <button @click="save"
          class="whitespace-nowrap btn btn-sm btn-success"><chevron-right-icon />
          Next</button>
      </div>    <div @focusout="handleFocusOut">
      <div class="border rounded border-slate-500/50 p-2" v-if="cart.order">
        <div class="bg-base-100 -mt-5 mb-2 w-40">Customer Information</div>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Name</span>
          </div>
          <input type="text" v-model="cart.order.contact" class="grow"
            :class="{ error: errors?.contact }" placeholder="John Smith" />
          <zod-error :errors="errors?.contact" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Company Name</span>
          </div>
          <input type="text" v-model="cart.order.companyName" class="grow"
            placeholder="Acme Rocket Supplies, Inc." />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Email</span>
          </div>
          <input type="text" v-model="cart.order.email"
            :class="{ error: errors?.email }" placeholder="shawn@aol.com" />
          <zod-error :errors="errors?.email" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Phone</span>
          </div>
          <input type="text" v-model="cart.order.phoneNumber"
            placeholder="+1 404 555 1212 " />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Notes</span>
          </div>
          <textarea rows="4" v-model="cart.order.notes"
            :class="{ error: errors?.notes }" class="h-24"
            placeholder="For your records..."></textarea>
          <zod-error :errors="errors?.notes" />
        </label>
      </div>

    </div>
  </div>
</template>
