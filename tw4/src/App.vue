<script setup>
import { RouterLink, RouterView } from 'vue-router'
import useStore from '@/store';

const store = useStore();
</script>

<template>
  <header>
    <div>
      <h1>TailVue</h1>
      <nav>
        <RouterLink to="/">Home</RouterLink> |
        <RouterLink to="/contact">Contact</RouterLink>
      </nav>
    </div>
  </header>

  <section>
    <div>{{ store.error ? store.error : '&nbsp;'}}</div>
    <div>{{ store.loading ? 'Loading...' : '&nbsp;'}}</div>
    <RouterView />
  </section>

  <footer>
    <div>
      Bechdel Test - An Example Website based on
      <a href="https://data.fivethirtyeight.com"
        target="_blank">fivethirtyeight.com data</a>
    </div>
  </footer>
</template>

<style scoped></style>
