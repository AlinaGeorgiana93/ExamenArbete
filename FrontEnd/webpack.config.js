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
      // Video files - Using file-loader to handle .mp4 files
      {
        test: /\.mp4$/,
        use: [
          {
            loader: 'file-loader',
            options: {
              name: '[name].[hash].[ext]', // To avoid caching issues
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
    historyApiFallback: true, // Ensure your routing works for direct page refreshes
    port: 3000,
    open: true, // Automatically open the browser
    hot: true, // Enable Hot Module Replacement
  },
};
