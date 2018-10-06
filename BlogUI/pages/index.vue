<template>
  <section class="section">
    <div class="container">
      <nuxt-link class="mb6 db" v-for="article in list" :key="article.id" :to="`/article/${article.id}`">
        <header class="bd-header">
          <h1 class="title">{{article.title}}</h1>
        </header>
        <markdown :source="article.content | desc"></markdown>
        <p class="flex justify-center mt3">
          <a :href="`/article/${article.id}`" class="button is-dark is-small is-rounded">Detail</a>
        </p>
      </nuxt-link>

      <p v-if="nextPage" class="flex justify-center">
        <a @click="showNextPage" class="button is-rounded">More</a>
      </p>
    </div>
  </section>
</template>

<script>
import markdown from "~/components/markdown.vue";

export default {
  components: { markdown },
  async asyncData({ app }) {
    let res = await app.$axios("/article");
    let data = res.data;
    data.page = 1;
    return data;
  },
  methods: {
    async showNextPage() {
      this.page++;
      this.nextPage = false;
      let res = await this.$axios("/article?page=" + this.page);
      let data = res.data;
      this.list = this.list.concat(
        data.list.filter(i => !this.list.find(o => o.id === i.id))
      );
      this.nextPage = data.nextPage;
    }
  },
  filters: {
    desc(content) {
      if (!content) return "";
      if (content.length > 255) return content.substr(0, 255);
      return content;
    }
  }
};
</script>

<style>
</style>
