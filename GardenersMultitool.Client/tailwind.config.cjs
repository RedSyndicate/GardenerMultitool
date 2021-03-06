module.exports = {
  content: ['./src/**/*.{html,js,svelte,ts}'],
  mode: "jit",
  purge: [
    "./src/**/*.{html,js,svelte,ts}",
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('daisyui'),
  ],
}
