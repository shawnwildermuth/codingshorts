import axios from "axios";

const instance = axios.create({
  timeout: 10000
});

async function get<T>(url: string) {
  const result = await instance.get<T>(url);
  if (result.status === 200) {
    return result.data;
  }
  throw Error("Error while executing get");
}

async function post<T>(url: string, data: T) {
  const result = await instance.post<T>(url, data);
  if (result.status === 201) {
    return result.data;
  }
  throw Error("Error while executing creating");
}

async function put<T>(url: string, data: T) {
  const result = await instance.put<T>(url, data);
  if (result.status === 200) {
    return result.data;
  }
  throw Error("Error while executing updating");
}

async function deleteItem(url: string) {
  const result = await instance.delete(url);
  if (result.status === 200) {
    return true;
  } else {
    return false;
  }
}


export default {
  get,
  post,
  put,
  deleteItem
}