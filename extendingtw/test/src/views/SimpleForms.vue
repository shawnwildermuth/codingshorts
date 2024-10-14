<script setup lang="ts"></script>

<template>
  <form novalidate>
    <div>
      <label>Empty</label>
      <input />
      <label>Text</label>
      <input type="text" />
      <label>Email</label>
      <input type="email" />
      <label>Url</label>
      <input type="url" />
      <label>Password</label>
      <input type="password" />
      <label>Text Area</label>
      <textarea rows="4"></textarea>
      <label>Select</label>
      <select>
        <option>Foo</option>
        <option>Bar</option>
        <option>Quux</option>
      </select>
      <label>Button</label>
      <button type="submit">Submit</button>
    </div>
  </form>
</template>
