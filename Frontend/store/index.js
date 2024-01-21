import { createStore } from "vuex";
import ActorsModule from "./Actors/Actors";
import ProducerModule from "./Producer/Producer";
import MoviesModule from "./Movies/Movies";
import GenresModule from "./Genres/Genres";


const store = createStore({
    modules:{
        Actors: ActorsModule,
        Producers:ProducerModule,
        Movies : MoviesModule,
        Genres : GenresModule
    }
});

export default store;