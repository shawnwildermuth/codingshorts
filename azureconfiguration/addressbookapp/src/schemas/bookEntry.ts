import { object, string } from "zod";

export const entrySchema = object({
  firstName: string().min(1),
  lastName: string().min(1),
  email: string().email().nullable()
});
