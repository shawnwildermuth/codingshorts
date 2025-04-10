<script setup lang="ts">
import { sum } from "radash";
import { useCart } from '@/stores/cart';
import { computed, onMounted } from "vue";
import type { OrderItem } from "@/models";
import { useCatalog } from "@/stores/catalog";
import { money, percentage } from "@/filters";
import { useRouter } from "vue-router";

const cart = useCart();
const catalog = useCatalog();
const router = useRouter();

onMounted(async () => {
  for (let x = 0; x < cart.items.length; ++x) {
    if (!cart.items[x].product) {
      const product = catalog.products.find(p => p.id === cart.items[x].productId);
      if (product) cart.items[x].product = product;
      else {
        const loadedProduct = await catalog.loadProduct(cart.items[x].productId);
        if (loadedProduct) cart.items[x].product = loadedProduct;
      }
    }
  }
});


function lineTotal(item: OrderItem) {
  return (item.price * item.quantity) * (1 - item.discount);
}

function remove(item: OrderItem) {
  cart.items.splice(cart.items.findIndex(i => i.id === item.id), 1);
  if (cart.items.length === 0) router.push("/");
}

const total = computed(() => sum(cart.items, i => lineTotal(i)));

function save() {
  if (cart.processCheckout()) {
    router.push("/customerInfo");
  }
}

</script>

<template>
  <div>
    <checkout-progress :stage="1" />
    <div class="w-full sm:w-4/5 lg:w-2/3 mx-auto ">
      <div class="mb-4">
        <div class="italic text-sm text-slate-500">Free Shipping and Prices
          include Taxes</div>
        <div class="flex justify-end gap-2">
          <button @click="router.back()"
            class="whitespace-nowrap btn-sm btn btn-ghost"><chevron-left-icon />
            Back</button>
          <button @click="save"
            class="whitespace-nowrap btn btn-sm btn-success"><chevron-right-icon />
            Next</button>
        </div>
      </div>
      <div class="rounded shadow-xl p-1">
        <table class="table table-sm table-fixed">
          <thead>
            <tr class="border-b border-slate-500">
              <th class="w-8"></th>
              <th>Product</th>
              <th class="w-24 text-center">Quantity</th>
              <th class="w-24 text-center">Price</th>
              <th class="w-24 text-center">Discount</th>
              <th class="text-right w-24">Line Total</th>
            </tr>
            <tr v-for="i in cart.items" class="text-slate-600">
              <td><button @click="remove(i)"><delete-icon
                    class="text-red-900/50 hover:text-red-900 w-6 h-6" /></button>
              </td>
              <td class="whitespace-normal">{{ i.product?.title ?? "Unknown" }}
              </td>
              <td class="text-center">{{ i.quantity }}</td>
              <td class="text-center">{{ money(i.price) }}</td>
              <td class="text-center">{{ percentage(i.discount) }}</td>
              <td class="text-right">{{ money(lineTotal(i)) }}</td>
            </tr>
            <tr class="text-slate-600">
              <td colspan="5"></td>
              <td class="text-right border-t border-b border-slate-600">{{
                money(total) }}</td>
            </tr>
          </thead>
        </table>
      </div>
    </div>
  </div>
</template>