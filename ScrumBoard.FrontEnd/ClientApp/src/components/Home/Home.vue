<template>
    <v-row>
        <v-col v-for="workspace in workspaceStore.workspaces" :key="workspace.publicKey" cols="12" lg="4" md="6" sm="12">
            <WorkspaceCard :display-name="workspace.name" :description="workspace.description" :public-key="workspace.publicKey" />
        </v-col>
    </v-row> 
    <v-btn icon fab right absolute primary-title @click="toggleDialog">
        <v-icon>add</v-icon>
        <v-tooltip activator="parent" location="end">
            Agregar un nuevo espacio de trabajo
        </v-tooltip>
    </v-btn>
    <CreateWorkspace :show-dialog="showDialog" @close-dialog="closeDialog" />
    <v-snackbar v-model="showSnackbar" :color="snackbarColor">{{ createWorkpsaceMessage }}</v-snackbar>
</template>

<script setup>
import WorkspaceCard from '../Workspace/WorkspaceCard.vue';
import CreateWorkspace from '../Workspace/CreateWorkspace.vue';

import { onBeforeMount, ref } from 'vue';
import { useWorkspacesStore } from '../../stores/workspaceStore'

const workspaceStore = useWorkspacesStore();

const showDialog = ref(false);
const showSnackbar = ref(false);
const createWorkpsaceMessage = ref('');
const snackbarColor = ref('');

onBeforeMount(function () {
    workspaceStore.getWorkspacesForUser;
});

function toggleDialog() {
    showDialog.value = !showDialog.value;
}

function closeDialog(message, isSuccessfull) {
    showDialog.value = false;
    createWorkpsaceMessage.value = message;
    snackbarColor.value = isSuccessfull ? 'success' : 'error';
    showSnackbar.value = true;
}
</script>