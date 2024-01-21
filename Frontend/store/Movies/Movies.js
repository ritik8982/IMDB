import axios from "axios";

const MoviesModule = {
    state(){
        return {
            Movies:[],
        }
    },
    mutations:{
        SetMovies(state,data)
        {
            state.Movies = data;
        },
    },
    getters:{
        GetMovies(state)
        {
            return state.Movies;
        },
    },
    actions:{
        async loadMovies(context)
        {
            try{
                const response = await axios.get('https://localhost:5001/api/movies');
                context.commit('SetMovies',response.data);
            }
            catch(err)
            {
                throw err;
            }
        },
        async loadMovie(context,movieId)
        {
            try{
                const response = await axios.get('https://localhost:5001/api/movies/'+movieId);
                return response.data;
            }
            catch(err)
            {
                console.log(err);
                throw err;
            }
        },
        async DeleteMovie(context,id)
        {
            try{
                await axios.delete('https://localhost:5001/api/movies/'+id);
            }
            catch(err)
            {
                throw err;
            }
        },
        async CreateMovie(context,data)
        {
            try{
                await axios.post('https://localhost:5001/api/movies',data);
            }
            catch(err)
            {
                throw err;
            }
        },
        async UpdateMovie(context, requestData) 
        {
            let movieId = requestData.movieId;
            const movieData = {
                Name: requestData.Name,
                YearOfRelease: requestData.YearOfRelease,
                Plot: requestData.Plot,
                CoverImage: requestData.CoverImage,
                ProducerId: requestData.ProducerId,
                ActorIds: requestData.ActorIds,
                GenreIds: requestData.GenreIds,
            }

            try 
            {
                await axios.put(`https://localhost:5001/api/movies/${movieId}`, movieData);
            } catch (err) {
                throw err;
            }
        }
    },
}

export default MoviesModule;