import { type Category } from "./Category";

export interface Product {
  id: number;
  gender: string | null;
  categoryId: number;
  category: Category | null;
  type: string;
  color: string;
  usage: string;
  title: string;
  imageFile: string | null;
  imageUrl: string | null;
  price: number;
}