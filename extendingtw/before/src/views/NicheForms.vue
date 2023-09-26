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
      <label>Number</label>
      <input type="number" />
      <label>Date</label>
      <input type="date" />
      <label>DateTime-Local</label>
      <input type="datetime-local" />
      <label>Month</label>
      <input type="month" />
      <label>Search</label>
      <input type="search" />
      <label>Tel</label>
      <input type="tel" />
      <label>Time</label>
      <input type="time" />
      <label>Week</label>
      <input type="week" />
      <label>Select - Multi</label>
      <select multiple>
        <option>Foo</option>
        <option>Bar</option>
        <option>Quux</option>
      </select>
      <label> <input type="checkbox" /> Checkbox </label>
      <label>Radio Buttons</label>
      <label>
        <input type="radio" value="One" name="aRadio" />
        One</label
      >
      <label>
        <input type="radio" value="Two" name="aRadio" />
        Three</label
      >
      <label>
        <input type="radio" value="Three" name="aRadio" />
        Three</label
      >
      <button type="submit">Submit</button>
      <button>Cancel</button>
    </div>
  </form>
</template>
