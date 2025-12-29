import { string, object, type inferFormattedError } from "zod";
import { PaymentType } from "./PaymentType";

export interface Payment {
  id: number;
  paymentType: PaymentType;
  orderId: number;
  amount: number;
  cardNumber: string | null;
  cvv: string | null;
  expiration: string | null;
  cardholder: string | null;
  postalCode: string | null; 
}

export function createEmptyPayment() {
  return {
    id: 0,
    paymentType: PaymentType.CreditCard,
    orderId: 0,
    amount: 0,
    cardNumber: "4000 0000 0000 0000",
    cvv: "123",
    expiration: "04/26",
    cardholder: "Shawn Wildermuth",
    postalCode: "10101"
  }
}

export const PaymentSchema = object({
  cardNumber: string().min(16, "Must be a valid credit card number"),
  cvv: string().min(1, "Required").max(4),
  expiration: string().regex(/^(0[1-9]|1[0-2])\/\d{2}$/, "Must match mm/yy"),
  cardholder: string().min(1, "Required"),
  postalCode: string().min(1, "Required")
});

export type PaymentErrors = inferFormattedError<typeof PaymentSchema>;