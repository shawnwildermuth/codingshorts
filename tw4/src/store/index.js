import { reactive } from 'vue';
import api from '../api';

const store = reactive({
  films: [],
  loading: false,
  error: null,
  loadAllFilms: async function (page) {
    try {
      this.loading = true;
      const result = await api.getAll(page);
      if (result.results) {
        this.films.splice(0, this.films.length, ...result.results)
      }
    } catch (error) {
      this.error = error;
    } finally { this.loading = false; }
  },
  loadFailedFilms: async function(page) {
    try {
      this.loading = true;
      const result = await api.getFailed(page);
      if (result.results) {
        this.films.splice(0, this.films.length, ...result.results)
      }
    } catch (error) {
      this.error = error;
    } finally { this.loading = false; }
  },
  loadPassedFilms: async function(page) {
    try {
      this.loading = true;
      const result = await api.getPassed(page);
      if (result.results) {
        this.films.splice(0, this.films.length, ...result.results)
      }
    } catch (error) {
      this.error = error;
    } finally { this.loading = false; }
  }
});

export default function () {
  return store;
}