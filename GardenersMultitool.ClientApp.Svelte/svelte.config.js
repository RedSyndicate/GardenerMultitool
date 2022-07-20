import autoprefixer from 'autoprefixer'
import tailwindcss from 'tailwindcss';
import sveltePreprocess from 'svelte-preprocess'

export default {
  // Consult https://github.com/sveltejs/svelte-preprocess
  // for more information about preprocessors
  preprocess: sveltePreprocess({
    postcss: {
      plugins: [tailwindcss(), autoprefixer()]
    }
  })
}
