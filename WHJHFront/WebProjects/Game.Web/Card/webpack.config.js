const path = require('path')
const webpack = require('webpack')
const CopyWebpackPlugin = require('copy-webpack-plugin')

module.exports = {
  // mode: 'development',
  mode: 'production',
  entry: {
    vendor: ['vue', 'vue-router', 'vuex', 'blueimp-md5'],
    build: './src/main.js'
  },
  output: {
    path: path.resolve(__dirname, 'v2'),
    // publicPath: '/dist/',
    filename: '[name].js'
  },
  module: {
    rules: [
      {
        test: /\.css$/,
        use: [
          'vue-style-loader',
          {
            loader: 'css-loader'
          }
        ]
      },
      {
        test: /\.vue$/,
        use: [
          {
            loader: 'vue-loader',
            options: {
              loaders: {}
              // other vue-loader options go here
            }
          }
        ],
        include: [path.resolve(__dirname, 'src/component')],
        enforce: 'post'
      },
      {
        test: /\.js$/,
        loader: 'babel-loader?cacheDirectory',
        exclude: /node_modules/,
        include: [path.resolve(__dirname, 'src')]
      },
      {
        test: /\.(png|jpg|gif|svg)$/,
        loader: 'file-loader',
        // options: {
        //   name: '[name].[ext]?[hash]'
        // },
        include: [path.resolve(__dirname, 'src/assets/images')]
      }
    ]
  },
  resolve: {
    alias: {
      vue$: 'vue/dist/vue.esm.js',
      component: path.resolve(__dirname, 'src/component/')
    },
    extensions: ['*', '.js', '.vue', '.json', '.css'],
    mainFields: ['jsnext:main', 'browser', 'main']
  },
  devServer: {
    // openPage: 'dist/',
    // publicPath: '/dist/',
    historyApiFallback: true,
    noInfo: true,
    overlay: true,
    proxy: {
      '/Card': 'http://localhost:8081'
    }
  },
  performance: {
    hints: false
  },
  plugins: [
    // new CleanWebpackPlugin(["dist"], {
    //   verbose: true,
    //   dry: false
    // }),
    // 告诉 Webpack 使用了哪些动态链接库
    // new DllReferencePlugin({
    //   // 描述 md5 动态链接库的文件内容
    //   manifest: require("./version2/util.manifest.json")
    // }),
    new CopyWebpackPlugin([
      {
        from: './src/assets',
        to: 'assets'
      }
    ])
    // 开启 Scope Hoisting
    // new ModuleConcatenationPlugin(),
    // new webpack.HotModuleReplacementPlugin(),
    // new webpack.optimize.CommonsChunkPlugin("vendor")
  ],
  // devtool: "#eval-source-map",
  watchOptions: {
    ignored: /node_modules/,
    aggregateTimeout: 1000
  },
  externals: {
    // vue: "Vue",
    // vuex: "Vuex",
    // "vue-router": "VueRouter"
  },
  // recordsPath: path.join(__dirname, 'records.json'),
  // devtool: "eval-source-map"
  optimization: {
    splitChunks: {
      chunks: 'all'
    }
  },
  stats: {
    children: false
  }
}
