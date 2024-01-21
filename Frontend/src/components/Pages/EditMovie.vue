<template>
     <v-dialog v-model="dialog" width="700px">
        <create-edit-movie :movieId="movieId"></create-edit-movie>
      </v-dialog>
</template>

<script>
import CreateEditMovie from './MovieForm.vue';
export default {
    components: {
        CreateEditMovie
    },
    data() {
        return {
            movieId: null, 
            dialog: true,
        }
    },
    created() {
        if (this.$route.params.movieId) {
            console.log("inside the edit movies compo");
            this.movieId = this.$route.params.movieId;
        }
    },
    watch : {
        dialog(value)
        {
            console.log('dailog ki value = ',value);
            if(value == false)
            {
                this.$router.push('/movies');
            }
        }
    },
    methods:{
        setDialog(payload)
        {
            this.dialog = payload;
            this.$emit('close',false);
        }
    },
}
</script>

<style>
.v-overlay__scrim
{
    background: rgba(0, 0, 0, 0.8) !important;
}
</style>