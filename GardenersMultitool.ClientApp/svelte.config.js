import adapter from '@sveltejs/adapter-node';
import { vitePreprocess } from '@sveltejs/kit/vite';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	// Consult https://kit.svelte.dev/docs/integrations#preprocessors
	// for more information about preprocessors
	preprocess: vitePreprocess(),

	kit: {
		adapter: adapter({
			fallback: "index.html",
			out: "dist"
		}),
		alias: {
			"$components": "./src/lib/components"
		},
		files: {
			appTemplate: "./src/index.html"
		}
	}
};

export default config;
