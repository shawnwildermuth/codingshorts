<script>
import Options from "./components/Options.vue";
import Composition from "./components/Composition.vue";
export default {
  components: {
    Options,
    Composition
  },
};
</script>

<template>
  <ul id="menu">
    <li><router-link to="/">Options API</router-link></li>
    <li><router-link to="/composition">Composition API</router-link></li>
  </ul>
  <router-view></router-view>
</template>

<style scoped>
* {
  font-family: Arial, Helvetica, sans-serif;
}

#menu {
  display: flex;
  flex-direction: row;
  padding-left: 0;
}

#menu li {
  list-style: none;
}

#menu li a {
  text-decoration: none;
  color: #888;
}

#menu li a:hover {
  text-decoration: underline;
}

#menu li:after {
  content: " |\00a0";
  color: #888;
}

#menu li:last-child:after {
  content: "";
}

</style>
