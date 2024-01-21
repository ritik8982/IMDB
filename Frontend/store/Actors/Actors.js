import axios from "axios";

const ActorsModule = {
    state(){
        return {
            Actors:[],
        }
    },
    getters : {
        GetActors(state)
        {
            return state.Actors;
        },
    },
    mutations:{
        SetActors(state,data)
        {
            state.Actors = data;
        },
    },
    actions:{
        async loadActors(context)
        {
            try{
                const response = await axios.get('https://localhost:5001/api/actors');
                context.commit('SetActors',response.data);
            }
            catch(err)
            {
                throw err;
            }
        },
        async AddActor(context,data)
        {
            try{
                const response = await axios.post('https://localhost:5001/api/actors',data);
                console.log(response);
                return response;
            }
            catch(err)
            {
                throw err;
            }
        },
    },
}

export default ActorsModule;