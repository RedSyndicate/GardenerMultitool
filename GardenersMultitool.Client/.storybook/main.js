const path = require('path');

module.exports = {
  stories: [
    "../src/**/*.stories.mdx",
    "../src/**/*.stories.@(js|ts|svelte)"
  ],
  addons: [
    "@storybook/addon-links",
    "@storybook/addon-essentials",
    '@storybook/addon-svelte-csf'
  ],
  framework: "@storybook/svelte",
  svelteOptions: {
    preprocess: import("../svelte.config.js").preprocess
  },
  core: { builder: 'storybook-builder-vite' },
  async viteFinal(config, { configType }) {
    return config;
  }
}