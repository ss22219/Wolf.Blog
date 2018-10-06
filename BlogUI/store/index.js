import Vuex from 'vuex'

const store = () => new Vuex.Store({
  state: {
    categories: []
  },
  mutations: {
    setCategories(state, categories) {
      state.categories = categories
    }
  },
  actions: {
    async getCategories({
      state,
      commit
    }) {
      var res = await this.$axios('/category')
      commit('setCategories', res.data)
    }
  }
})

export default store
