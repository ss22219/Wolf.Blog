<template>
  <section class="section">
    <div class="container">
      <header class="bd-header">
        <h1 class="title">{{title}}</h1>
      </header>
      <markdown :source="content"></markdown>
    </div>
  </section>
</template>

<script>
import markdown from "~/components/markdown.vue";
export default {
  components: { markdown },
  async asyncData({ app, route, error }) {
    let id = route.params.id;
    if (!id) return error({ statusCode: 404, message: "文章不存在" });
    let res = await app.$axios("/article/detail/" + id);
    if (!res.data) return error({ statusCode: 404, message: "文章不存在" });
    return res.data;
  },
  mounted() {}
};
</script>

<style>
</style>
