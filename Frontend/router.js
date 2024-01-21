import { createRouter, createWebHistory } from "vue-router";
import CreateMovie from "./src/components/Pages/CreateMovie.vue";
import EditMovie from "./src/components/Pages/EditMovie.vue";
import MoviesList from "./src/components/Pages/MoviesList.vue";
import NotFound from "./src/components/NotFound.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", redirect: "/movies" },
    {
      path: "/movies",
      component: MoviesList,
      children: [{ path: ":movieId/edit", component: EditMovie }],
    },
    { path: "/movies/add", component: CreateMovie },
    { path: "/:notFound(.*)", component: NotFound },
  ],
});

export default router;
