import type { Product } from "./Product";

export interface ProductResult {
  totalPages: number;
  currentPage: number;
  results: Array<Product>
}