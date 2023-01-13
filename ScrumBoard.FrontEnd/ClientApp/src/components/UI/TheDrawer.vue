<template>
    <v-navigation-drawer expand-on-hover rail>
        <v-list-item
          class="p-2"
          :prepend-avatar=Logo
          title="Mi Scrun"
          nav
        />
        <v-divider/>
        
    </v-navigation-drawer>
</template>

<script setup>
import Logo from '../../assets/scrumLogo.png'

</script>