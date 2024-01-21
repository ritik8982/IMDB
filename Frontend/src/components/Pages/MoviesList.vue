<template>
    <section>
        <v-container>
            <section v-if="isLoading" style="width: 100%; height: 100%;">
                <!-- Loading spinner -->
                <base-spinner></base-spinner>
            </section>
            <section v-else-if="!isLoading && movieList.length > 0">
                <v-row class="mx-auto">
                    <movie-item v-for="movie in movieList" :key="movie.id" :id="movie.id" :name="movie.name"
                        :description="movie.plot" :logo="movie.coverImage"></movie-item>
                </v-row>
                <router-view></router-view>
            </section>
            <section v-else-if="!error && !isLoading && movieList.length == 0" class="section-container">
                There is no movie, Please add a movie.
            </section>
            <section v-else-if="error" class="section-container">
                <p>{{ error }}</p>
            </section>
        </v-container>
    </section>
</template>

<script>
import MovieItem from '../MovieItem.vue'
import BaseSpinner from '../Ui/BaseSpinner.vue';
export default {
    components: {
        MovieItem,
        BaseSpinner
    },
    computed: {
        movieList() {
            return this.$store.getters.GetMovies;
        }
    },
    data() {
        return {
            isLoading: false,
            error: null
        }
    },
    async mounted() {
        try {
            this.isLoading = true;
            this.error = null;
            await this.$store.dispatch('loadMovies');
            this.isLoading = false;
        }
        catch (error) {
            this.isLoading = false;
            this.error = error.message;
            if (this.error === 'Network Error')
                this.error = 'Server is down. Please try after some time.'
            console.error(this.error);
        }
    }
}
</script>
