/** @type {import('tailwindcss').Config} */

const colors = require("tailwindcss/colors");

export default {
  content: [
    "./src/**/*.{vue,ts,js}",
    "./node_modules/vue-tailwind-datepicker/**/*.js",
  ],
  theme: {
    extend: {
      fontFamily: {
        'sans': ['Noto Sans', 'sans-serif']
      },
      colors: {
        "vtd-primary": colors.sky,
        "vtd-secondary": colors.gray,
      },
    },
  },
  plugins: [
    require("@tailwindcss/forms"), 
    require("@tailwindcss/typography"), 
    require("daisyui")
  ],
  daisyui: {
    themes: ["business"],
  }
}

