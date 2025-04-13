import type { Address, Order, OrderItem, Payment } from '@/models';
import { createEmptyOrder } from '@/models/Order';
import { defineStore } from 'pinia'
import { reactive, ref } from 'vue';
import { useStore } from '.';
import { useHttp } from '@/composables/http';

const http = useHttp();

const items = ref<Array<OrderItem>>([
  {
    id: 0,
    productId: 271,
    quantity: 2,
    price: 255,
    discount: 0,
    size: "13",
    width: "E",
    product: null,
    orderId: 0
  }, {
    id: 0,
    productId: 386,
    quantity: 2,
    price: 255,
    discount: 0,
    size: "13",
    width: "E",
    product: null,
    orderId: 0
  }]);

const order = ref<Order>();

function add(item: OrderItem) {
  items.value.push(item);
}

function processCheckout() {
  order.value = createEmptyOrder();

  if (items.value.length < 1) return false;

  // Add them every time we confirm the order
  order.value.items.splice(0, order.value.items.length, ...items.value)

  return true;
}

function isOrderValid() {
  if (!order.value) {
    return false;
  }
  return true;
}

function processPayment(payment: Payment, address: Address) {
  if (!isOrderValid()) return false;

  if (!payment || !address) return false;

  order.value!.payment = payment;
  order.value!.shippingAddress = address;
  return true;
}

function processCustomer() {
  if (!isOrderValid()) return false;
  // NOOP as we are just setting the values on the order directly.
  return true;
}

async function processOrder() {
  if (!isOrderValid()) return false;

  const result = await http.post("/api/orders", order.value);
  if (result) {
    order.value = undefined;
  }
  return result;
}

export const useCart = defineStore('cart', () => {
  return {
    items,
    order,
    add,
    isOrderValid,
    processCheckout,
    processCustomer,
    processPayment,
    processOrder,
  };
})