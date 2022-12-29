<template>
    <v-row>
        <v-col v-for="workspace in workspaceStore.workspaces" :key="workspace.publicKey" cols="12" lg="4" md="6" sm="12">
            <WorkspaceCard :display-name="workspace.name" :description="workspace.description" :public-key="workspace.publicKey" />
        </v-col>
    </v-row> 
</template>

<script setup>
import WorkspaceCard from '../Workspace/WorkspaceCard.vue';
import { onBeforeMount } from 'vue';
import { useWorkspacesStore } from '../../stores/workspaceStore'

const workspaceStore = useWorkspacesStore();

onBeforeMount(function () {
    workspaceStore.getWorkspacesForUser;
})
</script>