<template>
  <v-app>
    <v-layout>
      <TheDrawer />
      <TheHeader />
      <v-main>
        <v-container class="px-6 py-4" fluid>
          <router-view></router-view>
        </v-container>
      </v-main>
    </v-layout>
  </v-app>
</template>

<script setup>
import TheHeader from './components/UI/TheAppBar.vue'
import TheDrawer from './components/UI/TheDrawer.vue';

</script>
