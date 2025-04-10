import { object, string, type inferFormattedError } from "zod";
import type { Address } from "./Address";
import type { OrderItem } from "./OrderItem";
import { OrderStatus } from "./OrderStatus";
import { OrderType } from "./OrderType";
import type { Payment } from "./Payment";

export interface Order {
  id: number;
  orderDate: string;
  notes: string | null;
  orderType: OrderType;
  companyName: string | null;
  contact: string;
  email: string | null;
  phoneNumber: string | null;
  orderStatus: OrderStatus;
  shippingAddress: Address | null;
  payment: Payment | null;
  items: OrderItem[];
}

export function createEmptyOrder() : Order {
  return {
    id: 0,
    orderDate: new Date().toISOString(),
    notes: "Ship this quickly please.",
    orderType: OrderType.Online,
    companyName: "Smith Towing",
    contact: "Shawn Wildermuth",
    email: "shawn@aol.com",
    phoneNumber: "",
    orderStatus: OrderStatus.New,
    shippingAddress: null,
    payment: null,
    items: new Array<OrderItem>()
  } 
}

export const OrderSchema = object({
  contact: string().min(1, "required"),
  email: string().email(),
  notes: string().max(500, "500 max length on Notes")
});

export type OrderErrors = inferFormattedError<typeof OrderSchema>;