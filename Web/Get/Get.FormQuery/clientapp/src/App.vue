<template>
  <div id="app">
    <!-- <img alt="Vue logo" src="./assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/> -->
    <button @click="getdata">click me</button>
    <div :key="result" v-for="result in results">
      {{result}}
    </div>
  </div>
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue'
import axios from "axios";

export default {
  name: 'App',
  components: {
    // HelloWorld
  },
  data: () => ({
    results: [],
    numbers: [1,2,3,4,5]
  }),
  methods: {
    getdata() {
      axios.get("/api/values", {
        params: {
          numbers: this.numbers
        }
      }).then(res => {
        this.results = res.data;
      });
    }
  }
}
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
