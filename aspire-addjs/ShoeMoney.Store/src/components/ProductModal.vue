<script lang="ts" setup>
import { type OrderItem, type Product } from '@/models';
import { ref, type PropType } from 'vue';
import { money } from "@/filters";
import { useCart } from '@/stores/cart';

const cart = useCart();

const product = ref<Product>();

const theDialog = ref<HTMLDialogElement | null>(null);

function showModal(p: Product) {
  product.value = p;
  theDialog.value?.showModal();
}

const sizes = [
  "6", 
  "6 1/2",
  "7", 
  "7 1/2",
  "8", 
  "8 1/2",
  "9", 
  "9 1/2",
  "10", 
  "10 1/2",
  "11", 
  "11 1/2",
  "12", 
  "13",
  "14", 
  "15",
];

const widths = [
 ["E", "narrow"],
 ["F", "standard"],
 ["G", "width"],
 ["H", "extra-wide"]
]

const item = ref({
  size: "11",
  width: "E",
  quantity: 1
} as OrderItem);

function buy() {
  item.value.productId = product.value!.id;
  item.value.price = product.value!.price;
  item.value.discount = 0;
  cart.add(item.value);
  theDialog.value?.close();

}

defineExpose({
  showModal
});
</script>

<template>
  <dialog ref="theDialog" class="modal">
    <div class="modal-box  bg-white">
      <button class="absolute right-2 top-2"
        @click="theDialog?.close()"><close-icon></close-icon></button>
      <div class="grid grid-cols-2" v-if="product">
        <img :src="product.imageUrl ?? ''" :alt="product?.title"
          class="" />
        <div class="card p-2">
          <div>
            <h2 class="font-bold text-lg">{{ product.category?.name }}</h2>
            <p class="pt-2">{{ product.title }}</p>
            <div class="flex flex-wrap gap-1 mt-4">
              <div class="badge badge-ghost">{{ product.category?.name }}</div>
              <div class="badge badge-ghost">{{ product.color }}</div>
              <div class="badge badge-ghost">{{ product.gender }}</div>
              <div class="badge badge-ghost">{{ product.usage }}</div>
            </div>
            <div class="selection mt-4">
              <div>Quantity</div>
              <select v-model="item.quantity">
                <option v-for="num in [...Array(100).keys()].slice(1, 100)"
                  :value="num">{{ num }}</option>
              </select>
            </div>
            <div class="selection">
              <div>Size</div>
              <select  v-model="item.size">
                <option v-for="s in sizes" :value="s">{{ s }}</option>
              </select>
            </div>
            <div class="selection">
              <div>Width</div>
              <select  v-model="item.width">
                <option v-for="w in widths" :value="w[0]">{{ `${w[0]} (${w[1]})` }}</option>
              </select>
            </div>
            <div class="flex justify-end text-lg font-bold mt-4 mr-2">{{
              money(product.price) }}</div>
          </div>
        </div>
      </div>
      <div class="modal-action">
        <div class="flex">        
          <button @click="buy" class="btn btn-success"><cart-icon /> Add to Cart</button>
        </div>
      </div>
    </div>
  </dialog>
</template>

<style scoped>
.selection {
  @apply flex px-2 mt-2  justify-between;
}

.selection > div {
  @apply self-center w-24 text-sm;
}

.selection select {
  @apply select select-sm text-xs w-[9rem] border-base-300 border-2 bg-base-100 focus:bg-base-300 focus:border-none h-6 !outline-none;
}
</style>