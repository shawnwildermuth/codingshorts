import { object, string, type AnyZodObject } from "zod";

export const addressSchema = object({
  name: string().min(1),
  line1: string().min(1),
  line2: string().optional(),
  line3: string().optional(),
  cityTown: string().min(1),
  stateProvince: string().min(1),
  postalCode: string().min(5),
  country: string().optional()
});

