<script setup lang="ts">
  import { type WeatherForecast } from "@/models";
  import { ref, reactive, onMounted } from "vue";
  import Axios, { type AxiosError } from "axios";
  import { useAuth } from "@/auth";

  const auth = useAuth();

  const error = ref("");
  const items = reactive(new Array<WeatherForecast>())

  onMounted(async () => {
    try {
      const result = await Axios.get<WeatherForecast[]>("/weatherforecast", {
        headers: {
          "Authorization": "Bearer " + auth.token
        }
      });
      items.splice(0, items.length, ...result.data);
    } catch (e) {
      const result = e as AxiosError;
      error.value = `Response: ${result.response?.status}: ${result.response?.statusText}`;
    }
  })

</script>

<template>
  <table class="table w-full">
    <thead class="font-bold border-b border-gray-500">
      <tr>
        <td>Date</td>
        <td>Temp</td>
        <td>Comment</td>
      </tr>
    </thead>
    <tr v-for="f in items">
      <td>{{ f.date }}</td>
      <td>{{ f.temperatureF }}F/{{ f.temperatureC }}C</td>
      <td>{{ f.summary }}</td>
    </tr>
  </table>
</template>