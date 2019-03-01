const pkg = require('./package')

module.exports = {
  mode: 'spa',
  router: {
    mode: 'history'
  },
  /*
   ** Headers of the page
   */
  head: {
    title: pkg.name,
    meta: [{
        charset: 'utf-8'
      },
      {
        name: 'viewport',
        content: 'width=device-width, initial-scale=1'
      },
      {
        hid: 'description',
        name: 'description',
        content: pkg.description
      }
    ],
    link: [{
      rel: 'icon',
      type: 'image/x-icon',
      href: '/favicon.ico'
    }]
  },

  /*
   ** Customize the progress-bar color
   */
  loading: {
    color: '#FFFFFF'
  },

  /*
   ** Global CSS
   */
  css: [
    'github-markdown-css/github-markdown.css',
    'normalize.css',
    {
      src: 'bulma',
      lang: 'sass'
    },
    {
      src: '~assets/css/main.scss',
      lang: 'scss'
    },
    {
      src: '~assets/css/tool.css',
      lang: 'css'
    }
  ],

  /*
   ** Plugins to load before mounting the App
   */
  plugins: [
    '~/plugins/init'
  ],

  /*
   ** Nuxt.js modules
   */
  modules: [
    // Doc: https://github.com/nuxt-community/axios-module#usage
    '@nuxtjs/axios'
  ],
  /*
   ** Axios module configuration
   */
  axios: {
    // See https://github.com/nuxt-community/axios-module#options
    baseURL: '/'
  },

  /*
   ** Build configuration
   */
  build: {
    publicPath: 'https://unreal-1251258709.cos.ap-guangzhou.myqcloud.com/blog/',
    /*
     ** You can extend webpack config here
     */
    extend(config, ctx) {

    }
  }
}
