import { useHttp } from '@/composables/http';
import type { Category, Product } from '@/models';
import { defineStore } from 'pinia'
import { reactive, ref } from 'vue';
import { type ProductResult } from '@/models/ProductResult';

const http = useHttp();

const products = reactive(new Array<Product>());
const categories = reactive(new Array<Category>);

const currentPage = ref(1);
const totalPages = ref(0);

async function _loadProducts(url: string) {
  const result = await http.get<ProductResult>(url);
  if (result) {
    products.splice(0, products.length, ...result.results);
    currentPage.value = result.currentPage;
    totalPages.value = result.totalPages;
  }
}

async function loadProducts(page: number = 1) {
  await _loadProducts(`/api/products?page=${page}`);
}

async function loadProduct(productId: number) {
  return await http.get<Product>(`/api/products/${productId}`);
}

async function loadProductsByCategory(cat: string, page: number = 1) {
  await _loadProducts(`/api/categories/${cat}/products?page=${page}`);
}

async function loadCategories() {
  const result = await http.get<Array<Category>>(`/api/categories`);
  if (result) {
    categories.splice(0, categories.length, ...result);
  }
}

export const useCatalog = defineStore('catalog', () => {
  return {
    loadProducts,
    loadProduct,
    loadCategories,
    loadProductsByCategory,
    products,
    categories,
    currentPage,
    totalPages
  };
})