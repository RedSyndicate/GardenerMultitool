/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    require('path').join(require.resolve('@skeletonlabs/skeleton')),
    './src/**/*.{html,js,svelte,ts}',
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('@tailwindcss/typography'),
    require('@tailwindcss/forms'),
    // require('@tailwindcss/aspect-ratio'),
    ...require('@skeletonlabs/skeleton/tailwind/skeleton.cjs')()
  ],
}
