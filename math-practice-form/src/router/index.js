import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '@/views/Home.vue'
import Practice from '@/views/Practice.vue'

Vue.use(VueRouter);

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/practice",
        name: "Practice",
        component: Practice,
    }
];

const router = new VueRouter({
    mode: 'history',
    routes
});

export default router;