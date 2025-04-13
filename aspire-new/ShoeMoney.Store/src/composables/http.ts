import { useStore } from "@/stores";
import axios, { type Axios } from "axios";

console.log(import.meta.env);
const instance: Axios = axios.create({
  baseURL: import.meta.env.VITE_APP_URL ?? "http://localhost:5001"
});

async function get<T>(url: string) {
  const store = useStore();
  try {
    const store = useStore();
    const result = await instance.get<T>(url);
    if (result.status === 200) {
      return result.data;
    } else {
      store.error = `Failed to execute GET: ${url}`;
      return null;
    }
  } catch (e) {
    store.error = `Error during GET: ${url}`;
  }finally {
    store.endRequest();
  }
} 

async function post<T>(url: string, content: T) {
  const store = useStore();
  try {
    store.startRequest();
    const result = await instance.post<T>(url, content);

    if (result.status === 201) {
      return result.data;
    } else if (result.status === 202) {
      return true;
    } else {
      store.error = `Failed to execute POST: ${url}`;
      return null;
    }
  } catch (e) {
    store.error = `Error during POST: ${url}`;
  }finally {
    store.endRequest();
  }
} 

export function useHttp() {
  return {
    get,
    post
  }
}