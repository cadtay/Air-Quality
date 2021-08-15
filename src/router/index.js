import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import MoreInfo from "@/views/MoreInfo";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: '/moreinfo',
    name: 'moreinfo',
    component: MoreInfo
  }
];

const router = new VueRouter({
  routes,
});

export default router;
