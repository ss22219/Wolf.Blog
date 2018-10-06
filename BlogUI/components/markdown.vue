<template>
<div class="markdown-body" v-html="html">
</div>
</template>
<script>
import MarkdownIt from "markdown-it";
import mermaid from "mermaid";
import markdownItAnchor from "markdown-it-anchor";
import markdownPlantuml from "markdown-it-plantuml"
export default {
  name: "markdown",
  props: ["source"],
  data() {
    return {
      md: null
    };
  },
  computed: {
    html() {
      if (this.md) {
        var h = this.md.render(this.source);
        setTimeout(() => {
          mermaid.init(undefined, ".mermaid");
        }, 100);
        return h;
      }
    }
  },
  mounted() {
    var md = new MarkdownIt({
      html: true,
      breaks: true,
      linkify: true
    });

    md.use(markdownItAnchor, {
      level: 1,
      // slugify: string => string,
      permalink: true,
      // renderPermalink: (slug, opts, state, permalink) => {},
      permalinkClass: "header-anchor",
      permalinkSymbol: "#",
      permalinkBefore: true
    });

    mermaid.initialize({
      theme: "neutral"
    });

    md.use(markdownPlantuml)
    //markdown-mermaid
    md.use(function(md) {
      md.mermaid = mermaid;
    });
    this.md = md;

    const temp = md.renderer.rules.fence.bind(md.renderer.rules);
    md.renderer.rules.fence = (tokens, idx, options, env, slf) => {
      const token = tokens[idx];
      const code = token.content.trim();
      if (token.info === "mermaid") {
        return mermaidChart(code);
      }
      const firstLine = code.split(/\n/)[0].trim();
      if (
        firstLine === "gantt" ||
        firstLine === "sequenceDiagram" ||
        firstLine.match(/^graph (?:TB|BT|RL|LR|TD);?$/)
      ) {
        return mermaidChart(code);
      }
      return temp(tokens, idx, options, env, slf);
    };

    const mermaidChart = code => {
      try {
        mermaid.parse(code);
        return `<div class="mermaid">${code}</div>`;
      } catch ({ str, hash }) {
        return `<pre>${str}</pre>`;
      }
    };
  }
};
</script>
