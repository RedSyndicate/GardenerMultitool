import adapter from '@sveltejs/adapter-static';
import preprocess from 'svelte-preprocess';

const mode = process.env.NODE_ENV;
const dev = mode === "development";

/** @type {import('@sveltejs/kit').Config} */
const config = {
	// Consult https://github.com/sveltejs/svelte-preprocess
	// for more information about preprocessors
	preprocess: [
		preprocess({
			postcss: true,
			typescript: './tsconfig.json'
		})
	],
	kit: {
		adapter: adapter({
			pages: "dist",
			assets: "dist",
			fallback: null,
			precompress: false
		}),
		vite: {
			plugins: []
		}
	},
	// hydrate the <div id="svelte"> element in src/app.html
	target: '#svelte',
};

export default config;
