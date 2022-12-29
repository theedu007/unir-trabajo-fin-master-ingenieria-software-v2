import { createApp } from 'vue';
import { createRouter, createWebHistory } from "vue-router";
import Workspaces from '../components/Home/Home.vue'

export default createRouter({
    history: createWebHistory(),
    routes: [
        { path: "/", component: Workspaces}
    ]
});