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
    }
})