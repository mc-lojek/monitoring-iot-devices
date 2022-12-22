<template>
  <div>
    <Tables />
  </div>
</template>

<script>
import TablesVue from "./components/Tables.vue";
export default {
  name: "App",
  components: {
    Tables: TablesVue,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>