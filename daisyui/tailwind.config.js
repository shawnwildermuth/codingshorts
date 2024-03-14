/** @type {import('tailwindcss').Config} */

export default {
  content: [
    "src/**/*.{ts,vue,html}"
  ],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: ["dark", "light", "business"]
  }
}

