const cssnano = require("cssnano");

const mode = process.env.NODE_ENV;
const dev = mode === "development";

module.exports = {
  plugins: [
    // Some plugins, like postcss-nested, need to run before Tailwind
    require("tailwindcss"),
    // But others, like autoprefixer, need to run after
    require("autoprefixer")({
      flexbox: "no-2009"
    }),
    require('postcss-flexbugs-fixes'),

    !dev && cssnano({
      preset: "default",
    }),
  ],
};