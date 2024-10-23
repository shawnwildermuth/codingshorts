<script setup>
import { ref } from 'vue'

const message = ref('');

const contact = ref({
  name: 'Shawn',
  email: 'shawn@aol.com',
  subject: 'I need your help!',
  message: 'Please help!'
});

function save() {
  message.value = "Sent...";
  contact.value = {
    name: '',
    email: '',
    subject: '',
    message: ''
  }
}
</script>
<template>
  <div>
    <h1>Contact</h1>
    <form novalidate @submit.prevent="save">
      <div v-if="message">{{  message }}</div>
      <label for="name">Name:</label>
      <input type="text" id="name" name="name" v-model="contact.name" required>

      <label for="email">Email:</label>
      <input type="email" id="email" name="email" v-model="contact.email" required>

      <label for="subject">Subject:</label>
      <input type="text" id="subject" name="subject" v-model="contact.subject" required />

      <label for="message">Message:</label>
      <textarea id="message" name="message" v-model="contact.message" required></textarea>

      <button type="submit">Send</button>
    </form>
  </div>
</template>

<style scoped>

</style>
