<script setup>
import { ref} from "vue";

async function langChange(locale) {
  const result = await fetch(`/${locale}/main.welcome`);
  if (result) {
    theMessage.value = await result.text();
  }
}

const theMessage = ref("");
</script>

<template>
  <header>
    <h1>{{ $t("main.welcome") }}</h1>
    <select v-model="$i18n.locale" @change="langChange($i18n.locale)">
      <option  v-for="locale in $i18n.availableLocales" :key="`locale-${locale}`" :value="locale">{{ locale}}</option>
    </select>
    <div>{{ theMessage }}</div>
  </header>

  <main>
    <div>
      <button>{{ $t("main.ok") }}</button>
      <button>{{ $t("main.cancel") }}</button>
    </div>
  </main>
</template>

<style scoped></style>
