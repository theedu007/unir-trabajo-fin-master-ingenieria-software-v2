<template>
    <v-card :title="displayName" :subtitle="description">
    </v-card>
</template>

<script setup>
const props = defineProps(["displayName", "description", "publicKey"]);
</script>