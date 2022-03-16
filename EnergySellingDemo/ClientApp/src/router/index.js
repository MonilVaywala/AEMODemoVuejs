import { createWebHistory, createRouter } from "vue-router";
import EnergySellingVue from "../components/EnergySelling.vue";
import CreateEnergyVue from "../components/CreateEnergy.vue";

const routes = [
    {
        path: "/",
        redirect: "/EnergySellingData"
    },
    {
        path: "/EnergySellingData",
        name: "EnergySellingData",
        component: EnergySellingVue,
    },
    {
        path: "/CreateEnergy",
        name: "CreateEnergy",
        component: CreateEnergyVue,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;