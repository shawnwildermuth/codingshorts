<script setup>
import useStore from '@/store';
import { ref, onMounted } from 'vue';
import NumAbbr from 'number-abbreviate';

const store = useStore();
const page = ref(1);

let resultType = "all";

async function getAll() {
  await loadFilms("all");
}

async function getPassed() {
  await loadFilms("passed");
}

async function getFailed() {
  await loadFilms("failed");
}

async function loadFilms(filmType) {

  if (filmType) {
    resultType = filmType;
    page.value = 1;
  }

  if (resultType === "all") {
    await store.loadAllFilms(page.value);
  } else if (resultType === "passed") {
    await store.loadPassedFilms(page.value);
  } else if (resultType === "failed") {
    await store.loadFailedFilms(page.value);
  }
}

async function nextPage() {
  page.value++;
  await loadFilms();
}

async function prevPage() {
  if (page.value > 1) {
    page.value--;
    await loadFilms();
  }
}

function toMoney(value) {
  if (!value) return value;
  return value.toLocaleString('en-US', {
    style: 'currency',
    currency: 'USD',
  });
}

onMounted(async () => {
  await loadFilms();
});

</script>

<template>
  <section>
    <h1>Bechdel Test</h1>
    <div>
      <div>
        <button @click="getAll">All Films</button>
        <button @click="getPassed">Passed Films</button>
        <button @click="getFailed">Failed Films</button>
      </div>
      <div>
        <section>
          <div>
            <button @click="prevPage" :disabled="page === 1">
              &lt; Prev
            </button>
            <button @click="nextPage">
              Next &gt;
            </button>
          </div>
          <div>
            <div v-for="film in store.films">
              <img :src="film.posterUrl" :alt="film.title" />
              <div>{{ film.title }}</div>
              <div>{{ film.year }}</div>
              <div>{{ film.rating }}</div>
              <div><strong>Passed</strong>: {{ film.passed }}</div>
              <div><strong>Reason</strong>: {{ film.reason }}</div>
              <div>
                <strong>Budget</strong>: {{ toMoney(film.budget) }}
              </div>
              <div>
                <strong>Domestic</strong>: {{ toMoney(film.domesticGross) }}
              </div>
              <div>
                <strong>Int'l</strong>: {{ toMoney(film.internationalGross) }}
              </div>
              <p>{{ film.overview }}</p>
            </div>
          </div>
        </section>
      </div>
    </div>
  </section>
</template>
