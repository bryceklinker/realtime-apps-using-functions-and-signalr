const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  devtool: 'source-map',
  entry: {
    'index': path.resolve(__dirname, 'src', 'index.tsx')
  },
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: '[name].js',
    sourceMapFilename: '[file].map'
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.jsx', '.js', '.scss', '.css']
  },
  module: {
    rules: [
      {
        use: 'babel-loader',
        test: /\.(ts|tsx)$/
      },
      {
        use: ['style-loader', 'css-loader', 'sass-loader'],
        test: /\.(css|scss)$/
      },
      {
        use: 'file-loader',
        test: /\.(png|svg|jpg|gif)$/,
      }
    ]
  },
  plugins: [
    new HtmlWebpackPlugin({
      filename: 'index.html',
      template: path.resolve(__dirname, 'src', 'index.html'),
      inject: 'body'
    })
  ]
};