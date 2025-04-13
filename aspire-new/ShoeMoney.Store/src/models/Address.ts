import { object, string, type inferFormattedError } from "zod";

export interface Address {
  id: number;
  orderId: number;
  attentionTo: string | null;
  line1: string | null;
  line2: string | null;
  cityTown: string | null;
  stateProvince: string | null;
  postalCode: string | null;
  country: string | null;
  shippingPhoneNumber: string | null;
}

export function createEmptyAddress() {
  return {
    id: 0,
    orderId: 0,
    attentionTo: "Shawn",
    line1: "123 Main Street",
    line2: "",
    cityTown: "Atlanta",
    stateProvince: "GA",
    postalCode: "30303",
    country: "USA",
    shippingPhoneNumber: "+1 404 555 1212"
  }
}

export const AddressSchema = object({
  line1: string().min(1, "Required"),
  cityTown: string().min(1, "Required"),
  stateProvince: string().min(1, "Required"),
  postalCode: string().min(1, "Required"),
  shippingPhoneNumber: string().min(1, "Required"),
});

export type AddressErrors = inferFormattedError<typeof AddressSchema>;