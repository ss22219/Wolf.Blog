<template>
  <section class="section">
    <div class="container">
      <h1 class="title">创建文章</h1>
      <div class="field">
        <div class="control">
          <input placeholder="Title" class="input" v-model="title" type="text"/>
        </div>
      </div>
      <div class="field">
        <div class="control">
          <textarea placeholder="Content" rows="20" class="textarea" v-model="content"></textarea>
        </div>
      </div>
      <div class="field">
        <div class="control">
          <label class="label">分类</label>
          <select v-model="categoryid" class="select">
            <option selected="selected" value="">无</option>
            <option v-for="category in $store.state.categories" :key="category.id" v-bind:value="category.id">
              {{category.name}}
            </option>
          </select>
          <div class="control add-input mv2 flex">
            <input placeholder="添加分类" class="input is-small" v-model="category" type="text"/>
            <a class="button is-small" @click="addCategory">添加</a>
          </div>
        </div>
      </div>
      <div class="field">
        <label class="label">标签</label>
        <a v-for="t in tags" :key="t" @click="removeTag(t)" class="button is-small is-rounded mr1">{{t}}</a>
        <div class="control mv2 add-input">
          <input placeholder="添加标签" class="input is-small" v-model="tag" type="text"/>
          <a class="button is-small" @click="addTag">添加</a>
        </div>
      </div>

      <div class="field mv5">
        <div class="control">
          <a @click="submit" class="button is-primary">提交</a>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      tag: "",
      category: "",
      title: "",
      id: "",
      content: "",
      tags: [],
      categoryid: ""
    };
  },
  methods: {
    removeTag(tag) {
      this.tags = this.tags.filter(t => t !== tag);
    },
    addTag() {
      if (this.tags.indexOf(this.tag) === -1) {
        this.tags.push(this.tag);
      }
    },
    async addCategory() {
      let res = await this.$axios.post("/category/create", {
        name: this.category
      });
      if (res.data.code) alert(res.data.message);
      else this.getCategories();
    },
    async submit() {
      let res = await this.$axios.post("/article/create", {
        title: this.title,
        id: this.id,
        content: this.content,
        tags: this.tags,
        categoryid: this.categoryid
      });
      if (res.data.code) alert(res.data.message);
      else {
        alert("提交成功");
      }
    },
    ...mapActions(["getCategories"])
  }
};
</script>

<style scoped>
.container {
  max-width: 900px;
}

.add-input {
  display: flex;
}

.add-input input {
  max-width: 200px;
  border-right-width: 0;
  border-radius: 2px 0px 0px 2px;
}

.add-input a {
  border-radius: 0 2px 2px 0;
}
</style>
