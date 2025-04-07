const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  entry: './src/index.js', // Your main JS file
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, 'dist'),
  },
  module: {
    rules: [
      // JavaScript files - Using babel-loader to transpile JS
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
        },
      },
      // CSS files - Using style-loader and css-loader for CSS
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      // Image files - Using file-loader to handle images
      {
        test: /\.(png|jpe?g|gif|svg)$/i,
        use: [
          {
            loader: 'file-loader',
            options: {
              name: '[path][name].[ext]',
            },
          },
        ],
      },
    ],
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './public/index.html', // Path to your HTML file
    }),
  ],
  devServer: {
    static: {
      directory: path.resolve(__dirname, 'dist'), // Serve static files from dist
    },
    port: 3000,
    open: true, // Automatically open the browser
    hot: true, // Enable Hot Module Replacement
  },
};
