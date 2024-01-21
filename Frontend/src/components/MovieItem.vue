<template>
    <v-col cols='10' md="4" lg="3" sm="6">
        <v-card :variant="variant">
            <v-card-item>
                <div class="card-body">
                    <img :src="logo" :alt="name + ' poster'" height="200px" width="100%" />
                    <div class="text-h6 mb-1">
                        {{ name }}
                    </div>
                    <div class="text-caption">{{ description }}</div>
                </div>
            </v-card-item>

            <v-card-actions>
                <div class="explore-edit-button-color"><v-btn @click="SetShowMovieDetails(true)">Explore <i
                            class="material-icons">arrow_right_alt</i></v-btn></div>
                <div>
                    <router-link :to="'/movies/' + id + '/edit'">
                        <v-btn><i class="material-icons explore-edit-button-color">edit_square</i></v-btn>
                    </router-link>
                    <v-btn @click="handleDelete(id)"><i class="material-icons" style="color: red;">delete</i></v-btn>
                </div>
            </v-card-actions>
        </v-card>
        <movie-details v-if="ShowMovieDetails" @close="SetShowMovieDetails" :name="name" :description="description">
        </movie-details>
    </v-col>
</template>

<script>

import 'material-icons/iconfont/material-icons.css';
import MovieDetails from './MovieDetails.vue';

export default {
    props: ['id', 'name', 'description', 'logo'],
    components: {
        MovieDetails
    },
    data() {
        return {
            ShowMovieDetails: false,
        }
    },
    methods: {
        SetShowMovieDetails(payload) {
            this.ShowMovieDetails = payload;
        },
        async handleDelete(id) {
            const result = confirm("Are you sure you want to delete " + this.name);
            if (result) {
                try {
                    await this.$store.dispatch('DeleteMovie', id);
                    this.$store.dispatch('loadMovies');
                }
                catch (error) {
                    if (error.message === 'Network Error')
                        this.error = 'Server is down. Please try after some time.'

                    alert(error);
                    console.error(this.error);
                }
            }
        }
    }
}
</script>


<style scoped>
.card-body {
    height: 500px;
}

.explore-edit-button-color {
    color: rgb(29, 74, 197);
}

.text-caption {
    /* font-size: large !important; */
}

.v-card-actions {
    display: flex;
    justify-content: space-between !important;
}
</style>
