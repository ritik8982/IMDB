import axios from "axios";

const GenresModule = {
    state(){
        return {
            Genres:[],
        }
    },
    getters : {
        GetGenres(state)
        {
            return state.Genres;
        }
    },
    mutations:{
        SetGenres(state,data)
        {
            state.Genres = data;
        }
    },
    actions:{
        async loadGenres(context)
        {
            try{
                const response = await axios.get('https://localhost:5001/api/genres');
                context.commit('SetGenres',response.data);
            }
            catch(err)
            {
                throw err;
            }
        },
    },
}

export default GenresModule;