var HtmlWebpackPlugin = require('html-webpack-plugin')
var HTMLWebpackPluginConfig = new HtmlWebpackPlugin({
  template: __dirname + '/app/index.html',
  filename: 'index.html',
  inject: 'body'
});

var ExtractTextPlugin = require('extract-text-webpack-plugin');


module.exports = {
  entry: [
    './app/index.js',
  ],
  output: {
    path: __dirname + '/dist',
    filename: "index_bundle.js"
  },
  externals: {
	'Config': JSON.stringify({serverUrl: "http://localhost:65120/"})
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel-loader',
        query: {
          presets: ['es2015','react']
        }
      },
      {
        test: /\.scss$/,
        loader: ExtractTextPlugin.extract('css!sass')
      }
    ]
  },
  plugins: [
    HTMLWebpackPluginConfig,
    new ExtractTextPlugin('estilo.css', {
      allChunks: true
    })
  ]
};
