<template>
  <div class="container mx-auto bg-slate-50">
    <header class="bg-slate-800 text-white p-2">
      <div class="flex justify-between">
        <h1>
          <router-link
            to="/"
            class="hover:text-yellow-200 drop-shadow-[0_2px_4px_rgba(255,255,255,.25)]"
            >Nested Route Demo</router-link
          >
        </h1>
        <div class="flex justify-end p-2 children: border-r last:border-r-0">
          <router-link to="/" class="mr-2 hover:text-yellow-300 border-r pr-2"
            >Home</router-link
          >
          <router-link
            to="/people"
            class="mr-2 hover:text-yellow-300 border-r pr-2"
            >People</router-link
          >
          <router-link
            to="/about"
            class="mr-2 hover:text-yellow-300"
            >About</router-link
          >
        </div>
      </div>
    </header>
    <section class="min-h-screen p-2">
      <router-view></router-view>
    </section>
    <footer class="text-center">&copy; 2022 Wilder Minds LLC</footer>
  </div>
</template>
