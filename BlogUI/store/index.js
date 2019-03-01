import Vuex from 'vuex'

const store = () => new Vuex.Store({
  state: {
    categories: [],
    login: false
  },
  mutations: {
    setCategories(state, categories) {
      state.categories = categories
    },
    setLogin(state, isLogin) {
      state.login = isLogin
    }
  },
  actions: {
    async getCategories({
      state,
      commit
    }) {
      var res = await this.$axios('/category')
      commit('setCategories', res.data)
    },
    async getLoginState({
      state,
      commit
    }) {
      try {
        var res = await this.$axios('/home/islogin')
        commit('setLogin', res.login)
      } catch (e) {}
    }
  }
})

export default store
