/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'text': '#252503',
        'background': '#FBFBF8',
        'primary': '#bab04c',
        'secondary': '#9fdad4',
        'accent': '#7397c9',
       },
       
      fontSize: {
        'base': '1.25rem', // Sets the base font size to `text-xl`
      },
    },
  },
  plugins: [],
}
