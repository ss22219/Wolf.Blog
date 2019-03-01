export default function ({
  isDev,
  env,
  req,
  store: {
    dispatch
  },
  redirect
}) {
  dispatch('getCategories');
  dispatch('getLoginState');
}
