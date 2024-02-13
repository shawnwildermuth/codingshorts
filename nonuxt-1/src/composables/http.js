import axios from "axios";
import { sortBy } from "lodash";

const baseUrl = "https://restdesign.azurewebsites.net/api";

export function useHttp() {
  return api;
}

const api = {
  async getCustomers() {
    const result = await axios.get(`${baseUrl}/customers`);
    if (result.status === 200) {
      return sortBy(result.data, i => i.companyName);
    }
  },
  async getCustomer(id) {
    const result = await axios.get(`${baseUrl}/customers/${id}`);
    if (result.status === 200) {
      return result.data;
    }
  }
}