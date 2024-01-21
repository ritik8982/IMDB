<template>
  <v-app>
      <nav-bar></nav-bar>
  </v-app>
</template>

<script>
import navbar from './components/layout/NavBar.vue'
export default{
  components:{
    navbar,
  }
}
</script>

<style>
.section-container {
    font-weight: bold;
    text-align: center;
    padding: 0.5rem;
    margin: 1rem auto;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.26);
    border-radius: 12px;
    min-height: 200px;
    width: 50%;
    font-size: xx-large;
  }
</style>