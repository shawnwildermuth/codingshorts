/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "src/**/*.{vue,js}"
  ],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [ "dark" ]
  }
}

