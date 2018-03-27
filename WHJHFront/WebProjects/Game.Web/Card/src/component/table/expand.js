export default {
  name: "cell",
  functional: true,
  props: {
    title: String,
    render: Function,
  },
  render: (h, ctx) => {
    return ctx.props.render(h, ctx.props);
  }
}
