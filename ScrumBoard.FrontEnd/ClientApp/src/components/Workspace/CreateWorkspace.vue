<template>
    <v-dialog width="75rem" persistent v-model="toggleDialog">
        <v-card>
            <v-card-title>
                Nuevo espacio de trabajo
            </v-card-title>
            <v-card-text>
                <v-container>
                    <v-form ref="form">
                        <v-row autocomplete>
                            <v-col cols="12">
                                <v-text-field label="Nombre del espacio de trabajo" required v-model="name" />
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols="12">
                                <v-text-field label="Descripcion del espacio de trabajo" required
                                    v-model="description" />
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <div class="d-flex justify-end">
                                    <div class="ma-2 pa-2">
                                        <v-btn class="bg-red" @click="closeDialog">Descartar</v-btn>
                                    </div>
                                    <div class="ma-2 pa-2">
                                        <v-btn class="bg-green" @click="submit">Crear</v-btn>
                                    </div>
                                </div>
                            </v-col>
                        </v-row>
                    </v-form>
                </v-container>
            </v-card-text>
        </v-card>
    </v-dialog>
</template>

<script setup>
import { ref, computed } from 'vue';
import { useWorkspacesStore } from '../../stores/workspaceStore'

const props = defineProps(['showDialog']);
const emit = defineEmits(['closeDialog']);

const workspaceStore = useWorkspacesStore();

const name = ref('');
const description = ref('');
const form = ref(null);

const toggleDialog = computed(() => {
    return props.showDialog;
});

function closeDialog() {
    emit('closeDialog');
}

function submit() {
    workspaceStore.createWorkspace(name.value, description.value)
        .then(({ data, isSuccessful }) => {
            workspaceStore.addNewWorkspace(data);
            emit('closeDialog', 'Espacio de trabajo creado con exito!', isSuccessful);
        })
        .catch(error => {
            emit('closeDialog', 'Hubo un error al crear el espacio de trabajo', false);
        })
}
</script>