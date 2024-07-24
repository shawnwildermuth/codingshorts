import http from '@/http';
import {
  type EntryModel,
  type EntryLookupModel,
  type AddressModel
} from '@/models';
import { defineStore } from "pinia"
import { formatName } from '@/formatters';

export const useStore = defineStore("main", {
  state: () => {
    return {
      entries: new Array<EntryLookupModel>(),
      filter: "",
      errorMessage: "",
      isBusy: false
    };
  },
  actions: {
    startRequest() {
      this.isBusy = true;
      this.errorMessage = "";
    },
    async loadLookupList() {
      if (this.entries.length === 0) {
        try {
          this.startRequest();
          const result = await http.get<Array<EntryLookupModel>>("/api/entries/lookup");
          this.entries.splice(0, this.entries.length, ...result);
          this.sortEntities();
        } catch (e: any) {
          this.errorMessage = e;
        } finally {
          this.isBusy = false;
        }
      }
    },
    async getEntryById(id: Number) {
      try {
        this.startRequest();
        const result = await http.get<EntryModel>(`/api/entries/${id}`);
        return result;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
    },
    async saveEntry(data: EntryModel) {
      try {
        this.startRequest();
        const result = await http.post<EntryModel>("/api/entries", data);
        this.entries.push({
          id: result.id,
          displayName: formatName(result),
        });
        this.sortEntities();
        return result.id;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
      return null;
    },
    async updateEntry(data: EntryModel) {
      try {
        this.startRequest();
        const result = await http.put<EntryModel>(`/api/entries/${data.id}`, data);
        this.entries.splice(this.entries.findIndex(e => e.id == data.id), 1);
        this.entries.push({
          id: result.id,
          displayName: formatName(result),
        });
        this.sortEntities();
        return result.id;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
      return null;
    },
    async deleteEntry(data: EntryModel) {
      try {
        this.startRequest();
        const result = await http.deleteItem(`/api/entries/${data.id}`);
        if (result) {
          this.entries.splice(this.entries.findIndex(e => e.id == data.id), 1);
        } else {
          this.errorMessage = "Failed to delete an entry";
        }
        this.sortEntities();
        return true;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
      return null;
    },
    sortEntities() {
      this.entries.sort((a, b) => {
        return a.displayName < b.displayName ? -1 :
          (a.displayName > b.displayName ? 1 : 0)
      });
    },
    async getAddressById(entry: EntryModel, id: Number) {
      try {
        this.startRequest();
        const result = await http.get<AddressModel>(`/api/entries/${entry.id}/addresses/${id}`);
        return result;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
    },
    async saveAddress(entry: EntryModel, data: AddressModel) {
      try {
        this.startRequest();
        const result = await http.post<AddressModel>(`/api/entries/${entry.id}/addresses/`, data);
        entry.addresses.push(result);
        return result.id;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
      return null;
    },
    async updateAddress(entry: EntryModel, data: AddressModel) {
      try {
        this.startRequest();
        const result = await http.put<AddressModel>(`/api/entries/${entry.id}/addresses/${data.id}`, 
          data);
        entry.addresses.splice(entry.addresses.findIndex(a => a.id == data.id), 1);
        entry.addresses.push(result);
        return result.id;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
      return null;
    },
    async deleteAddress(entry: EntryModel, data: AddressModel) {
      try {
        this.startRequest();
        const result = await http.deleteItem(`/api/entries/${entry.id}/addresses/${data.id}`);
        if (result) {
          entry.addresses.splice(entry.addresses.findIndex(a => a.id == data.id), 1);
        } else {
          this.errorMessage = "Failed to delete an address";
        }
        this.sortEntities();
        return true;
      } catch (e: any) {
        this.errorMessage = e;
      } finally {
        this.isBusy = false;
      }
      return null;
    },
  },
  getters: {
    entryList: (state) => {
      if (state.filter) {
        return state.entries.filter((e) => e.displayName.toLowerCase().includes(state.filter));
      } else {
        return state.entries;
      }
    }
  }
});

