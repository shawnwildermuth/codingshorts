<script setup lang="ts">
import { stateList } from '@/lookups/states';
import { createEmptyAddress, AddressSchema, type AddressErrors } from '@/models/Address';
import { createEmptyPayment, PaymentSchema, type PaymentErrors } from '@/models/Payment';
import { useStore } from '@/stores';
import { useCart } from '@/stores/cart';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();
const cart = useCart();
const payment = ref(createEmptyPayment());
const shipping = ref(createEmptyAddress());
const payErrors = ref<PaymentErrors | null>(null);
const addressErrors = ref<AddressErrors | null>(null);
const dirty = ref(false);

onMounted(() => {
  if (!cart.isOrderValid()) router.push("/checkout");
});

function validate() {
  dirty.value = true;
  payErrors.value = null;
  addressErrors.value = null;

  const valid = PaymentSchema.safeParse(payment.value);
  if (!valid.success) payErrors.value = valid.error.format();
  const addValid = AddressSchema.safeParse(shipping.value);
  if (!addValid.success) addressErrors.value = addValid.error.format();
  return valid.success && addValid.success;
}

function handleFocusOut() {
  if (dirty.value) validate();
}

function confirm() {
  if (validate()) {
    if (cart.processPayment(payment.value, shipping.value)) {
      router.push("/confirmation")
    } else {
      store.error = "Could not save payment information.";
    }
  }
}

</script>

<template>
  <div class="w-full sm:w-4/5 lg:w-2/3 mx-auto">
    <checkout-progress :stage="3" />
    <div class="flex justify-end gap-2 mt-8">
        <button @click="router.back()"
          class="whitespace-nowrap btn btn-sm btn-ghost"><chevron-left-icon />
          Back</button>
        <button @click="confirm"
          class="whitespace-nowrap btn btn-sm btn-success"><chevron-right-icon />
          Next</button>
      </div>
    <div @focusout="handleFocusOut">
      <div class="text-lg font-bold mb-4">Payment Information</div>
      <div class="border rounded border-slate-500/50 p-2">
        <div class="bg-base-100 -mt-5 mb-2 w-48">Shipping/Billing Address</div>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Shipping Contact</span>
          </div>
          <input type="text" v-model="shipping.attentionTo" class="grow"
            placeholder="John Smith" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Shipping Phone</span>
          </div>
          <input type="text" v-model="shipping.shippingPhoneNumber"
            :class="{ error: addressErrors?.shippingPhoneNumber }" class="grow"
            placeholder="+1 404 555 1212" />
          <zod-error :errors="addressErrors?.shippingPhoneNumber" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Street Address</span>
          </div>
          <input type="text" v-model="shipping.line1"
            :class="{ error: addressErrors?.line1 }" class="grow"
            placeholder="123 Main Street" />
          <zod-error :errors="addressErrors?.line1" />

        </label>
        <label class="input-label">
          <input type="text" v-model="shipping.line2" class="grow"
            placeholder="Suite 400" />
        </label>
        <div class="flex gap-1">
          <label class="input-label">
            <div class="label">
              <span class="label-text">City</span>
            </div>
            <input type="text" v-model="shipping.cityTown"
              :class="{ error: addressErrors?.cityTown }" class="grow"
              placeholder="Atlanta" />
            <zod-error :errors="addressErrors?.cityTown" />
          </label>
          <label class="input-label">
            <div class="label">
              <span class="label-text">State/Province</span>
            </div>
            <select class="select grow" v-model="shipping.stateProvince"
              :class="{ error: addressErrors?.stateProvince }">
              <option value="" selected disabled>Pick One...</option>
              <option v-for="state in stateList" :value="state.abbreviation">{{
                state.name }}</option>
            </select>
            <zod-error :errors="addressErrors?.stateProvince" />
          </label>
          <label class="input-label">
            <div class="label">
              <span class="label-text">Postal Code</span>
            </div>
            <input type="text" class="grow" placeholder="10101"
              :class="{ error: addressErrors?.postalCode }"
              v-model="shipping.postalCode" />
            <zod-error :errors="addressErrors?.postalCode" />
          </label>
        </div>
      </div>
      <div class="border rounded border-slate-500/50 p-2 mt-8">
        <div class="bg-base-100 -mt-5 mb-2 w-48">Credit Card</div>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Card Number</span>
          </div>
          <input type="text" v-model="payment.cardNumber" class="grow"
            :class="{ error: payErrors?.cardNumber }"
            placeholder="4000 0000 0000 0000" />
          <zod-error :errors="payErrors?.cardNumber" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Expiration</span>
          </div>
          <input type="text" v-model="payment.expiration" class="grow"
            :class="{ error: payErrors?.expiration }" placeholder="04/24" />
          <zod-error :errors="payErrors?.expiration" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">CVV</span>
          </div>
          <input type="text" v-model="payment.cvv" class="grow"
            :class="{ error: payErrors?.cvv }" placeholder="456" />
          <zod-error :errors="payErrors?.cvv" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Cardholder's Name</span>
          </div>
          <input type="text" v-model="payment.cardholder" class="grow"
            :class="{ error: payErrors?.cardholder }"
            placeholder="Bill Smith" />
          <zod-error :errors="payErrors?.cardholder" />
        </label>
        <label class="input-label">
          <div class="label">
            <span class="label-text">Cardholder's Postal Code</span>
          </div>
          <input type="text" v-model="payment.postalCode" class="grow"
            :class="{ error: payErrors?.postalCode }" placeholder="10101" />
          <zod-error :errors="payErrors?.postalCode" />
        </label>
      </div>
    </div>
  </div>
</template>
