import { defineStore } from 'pinia'
import { ref } from 'vue';

const error = ref("");
const isBusy = ref(false);

function startRequest() {
  isBusy.value = true;
  error.value = "";
}

function endRequest() {
  isBusy.value = false;
}


export const useStore = defineStore('root', () => {

  return {
    error,
    isBusy, 
    startRequest,
    endRequest
  };
})
