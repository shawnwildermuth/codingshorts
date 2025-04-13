import daisyui from './node_modules/daisyui/src/index';

/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "src/**/*.{vue,ts,html}"
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require("daisyui")
  ],
  daisyui: {  
    themes: ["nord"],
  }
}
