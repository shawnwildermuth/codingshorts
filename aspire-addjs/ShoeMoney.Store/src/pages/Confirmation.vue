<script setup lang="ts">
import { useCart } from '@/stores/cart';
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { money } from '@/filters';
import { sum } from "radash";
import type { OrderItem } from '@/models';

const router = useRouter();
const cart = useCart();
const isProcessed = ref(false);

onMounted(() => {
  if (!cart.isOrderValid()) router.push("/checkout");
});

function lineTotal(item: OrderItem) {
  return (item.price * item.quantity) * (1 - item.discount);
}

const total = computed(() => sum(cart.items, i => lineTotal(i)));

async function submitOrder() {
  if (await cart.processOrder()) {
    router.push("/");
  }
}

</script>

<template>
  <div class="w-full sm:w-4/5 lg:w-2/3 mx-auto ">
    <checkout-progress :stage="4" />
    <div v-if="!isProcessed" class="flex justify-end gap-2 mb-4">
      <button @click="router.back()"
        class="whitespace-nowrap btn btn-sm btn-ghost"><chevron-left-icon />
        Back</button>
      <button @click="submitOrder"
        class="whitespace-nowrap btn btn-sm btn-success"><chevron-right-icon /> Submit Order</button>
    </div>
    <div class="border rounded border-slate-500/50 p-2">
      <div v-if="!isProcessed" class="bg-base-100 -mt-5 mb-2 w-24">Order Details
      </div>
      <div v-if="isProcessed" class="bg-base-100 -mt-5 mb-2 w-32">Order
        Confirmation</div>
      <div v-if="cart.order">
        <div>
          <div class="text-lg font-bold">Customer</div>
          <div  class="px-4">
            <div>{{ cart.order.contact }}</div>
            <div v-if="cart.order.companyName">{{ cart.order.companyName }}
            </div>
            <div>{{ cart.order.email }}</div>
            <div v-if="cart.order.phoneNumber">{{ cart.order.phoneNumber }}
            </div>
            </div>
            <div>
            <div  class="text-lg font-bold">Shipping Address</div>
            <div class="px-4">
              <div>{{ cart.order.shippingAddress?.line1 }}</div>
              <div v-if="cart.order.shippingAddress?.line2">{{
                cart.order.shippingAddress?.line2 }}</div>
              <div>
                <div>{{ cart.order.shippingAddress?.cityTown }}, {{
                  cart.order.shippingAddress?.stateProvince }} {{
                    cart.order.shippingAddress?.postalCode }}</div>
              </div>
              <div v-if="cart.order.shippingAddress?.country">{{
                cart.order.shippingAddress?.country }}</div>
            </div>
          </div>
        </div>
        <table class="table table-fixed border border-slate-300 ">
          <tr v-for="item in cart.order.items">
            <td class="text-center w-4">{{ item.quantity }}</td>
            <td>{{ item.product?.title }}</td>
            <td class="text-center w-16">{{ item.product?.color }}</td>
            <td class="text-center w-32">{{ item.product?.type }}</td>
            <td class="text-center w-20">{{ money(item.price) }}</td>
            <td class="text-right w-24">{{ money(lineTotal(item)) }}</td>
          </tr>
          <tr>
            <td colspan="5" class=text-right>Order Total:</td>
            <td class="text-right font-bold border-y border-slate-500">{{ money(total) }}</td>
          </tr>

        </table>

      </div>
    </div>

  </div>
</template>