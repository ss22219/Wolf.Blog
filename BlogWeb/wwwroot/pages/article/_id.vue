<template>
  <section class="container">
    <h1 class="title"><a href="javascript:;">{{title}}</a></h1>
    <markdown :source="source"></markdown>
  </section>
</template>

<script>
import markdown from "~/components/markdown.vue";

export default {
  components: { markdown },
  data() {
    return {
      source: `
# 生产消费者模式

利用MQ可以很轻易的在分布式系统中实现生产消费者模式
在不修改生产者服务代码的情况下就可以添加新的消费者，提高了扩展性和维护性
在复杂的系统中，各个服务的开发者，部门，公司都可能是不一样的
  
对于生产者，只关注推送消息，不关心消息去往何方，被如何处理
对于消费者，他们只关注消息本身，以及如何处理消息
MQ最终实现了服务之间的解耦
  
# 削峰填谷  
  
系统中有一些业务，可能瞬间并发很高，这个时候就产生了很多任务，如果直接去处理，有可能将服务器累死，资源不足将会引发一系列的系统问题
这个时候通过升级服务器解决了问题，但当这些业务在一瞬间请求高峰过去之后，可能会陷入一个请求低谷，资源得不到有效利用，造成了一定的浪费
  
将任务先加入到MQ中，处理者服务按批次获取任务，处理完成后再获取，再处理，能够保证高峰期均速的处理任务，将服务器负载控制在安全水平内
当高峰期过去后，MQ内还存在没处理完成的任务，此时服务器依然均速处理任务，让服务器资源得到充分利用

# UML example:
\`\`\`uml
sequenceDiagram
    Alice ->> Bob: Hello Bob, how are you?
    Bob-->>John: How about you John?
    Bob--x Alice: I am good thanks!
    Bob-x John: I am good thanks!
    Note right of John: Bob thinks a long<br/>long time, so long<br/>that the text does<br/>not fit on a row.

    Bob-->Alice: Checking with John...
    Alice->John: Yes... John, how are you?
\`\`\`
`,
      title: "Title"
    };
  },
  mounted() {}
};
</script>

<style>
.title {
  text-align: center;
  margin: 30px;
  font-size: 3em;
}
.title a {
  text-decoration: none;
  color: #24292e;
}
.container {
  max-width: 960px;
  padding: 0 20px;
  margin: auto;
  min-height: 100vh;
}
</style>
