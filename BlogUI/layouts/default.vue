<template>
  <div>
    <header>
      <div class="flex items-center pe-auto justify-between">
        <div class="f3 fw3 lh-copy tracked1 ma0 pa0 h3 pl5 pr4 flex items-center">
          <nuxt-link :class="'midgrey-l2 link fw4 flex items-center ember-view'" to="/">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 48 48" class="w1 mr2 midgrey-l2">
              <path
                fill="#010101"
                d="M37.802 46.247L12.376 24 37.801 1.753A1 1 0 1 0 36.484.247l-26.286 23a1.004 1.004 0 0 0 .001 1.506l26.286 23a.997.997 0 0 0 1.411-.095 1 1 0 0 0-.094-1.411z"
              ></path>
            </svg>
            Home
          </nuxt-link>
        </div>
        <div
          v-if="$store.state.login"
          class="f3 fw3 lh-copy tracked1 ma0 pa0 h3 pl5 pr4 flex items-center"
        >
          <nuxt-link
            :class="'midgrey-l2 link fw4 flex items-center ember-view'"
            to="/article/new"
          >New</nuxt-link>
        </div>
        <div
          v-if="!$store.state.login"
          class="f3 fw3 lh-copy tracked1 ma0 pa0 h3 pl5 pr4 flex items-center"
        >
          <a class="midgrey-l2 link fw4 flex items-center ember-view" href="/home/login">Login</a>
        </div>
      </div>
    </header>
    <nuxt/>
  </div>
</template>
<script>
import "bulma/css/bulma.css";
export default {
  components: {},
  data() {
    return {
      isFixed: false
    };
  }
};
</script>
<style>
</style>
