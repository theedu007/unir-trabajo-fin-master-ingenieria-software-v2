import { defineStore } from "pinia";
import axios from "axios";

export const useWorkspacesStore = defineStore("workspaces", {
    state: () => {
        return {
            workspaces: []
        }
    },
    getters: {
        getWorkspacesForUser() {
            axios.get('api/workspace')
                .then(response => {
                    this.workspaces = response.data;
                });
        }
    },
    actions: {
        createWorkspace(name, description) {
            var json = JSON.stringify({
                name: name,
                description: description
            });
            return axios.post('api/workspace/create', json, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.data)
        },
        addNewWorkspace(workpsace) {
            this.$patch((state) => {
                state.workspaces.push(workpsace);
            });
        }
    }
})