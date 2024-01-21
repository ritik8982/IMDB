import axios from "axios";
const ProducerModule = {
    state(){
        return {
            Producers:[],
        }
    },
    getters : {
        GetProducers(state)
        {
            return state.Producers;
        },
    },
    mutations:{
        SetProducers(state,data)
        {
            state.Producers = data;
        },
    },
    actions:{
        async loadProducers(context)
        {
            try{
                const response = await axios.get('https://localhost:5001/api/producers');
                context.commit('SetProducers',response.data);
            }
            catch(err)
            {
                throw err;
            }
        },
        async AddProducer(context,data)
        {
            try{
                const response = await axios.post('https://localhost:5001/api/producers',data);
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

export default ProducerModule;