import { object, string, type AnyZodObject } from "zod";

const schema = object({
  name: string().min(1),
  line1: string().min(1),
  line2: string().optional(),
  line3: string().optional(),
  cityTown: string().min(1),
  stateProvince: string().min(1),
  postalCode: string().min(5),
  country: string().optional()
});

export function useAddressSchema(additionalRules: AnyZodObject) {
  if (additionalRules) {
    return schema.merge(additionalRules);
  }
  return schema;
}
