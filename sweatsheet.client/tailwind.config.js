import konstaConfig from "konsta/config";

/** @type {import('tailwindcss').Config} */
export default konstaConfig({
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  konsta: {},
  theme: {
    extend: {},
  },
  plugins: [],
});
