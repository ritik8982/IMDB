<template>
    <section v-if="error" class="section-container">
        <div>{{ error }}</div>
        <div>
            <router-link to="/movies">Back to Movie Listing Page</router-link>
        </div>
    </section>
    <section v-else style="padding: 1rem;" :class="{ 'popup': movieId }">
        <h2 class="header">{{ movieId ? 'Edit Movie' : 'Add a Movie' }}</h2>
        <form>
            <v-text-field v-model="movieName.value" label="Movie Name" @blur="movieName.isInValid = false"></v-text-field>
            <p v-if="movieName.isInValid" :class="{ inValid: movieName.isInValid }">Movie Name needs to be at least 1
                characters.</p>

            <v-text-field type="number" v-model="yearOfRelease.value" label="Year of Release" @blur="yearOfRelease.isInValid = false"></v-text-field>
            <p v-if="yearOfRelease.isInValid" :class="{ inValid: yearOfRelease.isInValid }">Please enter year of release</p>


            <!-- multi select -->
            <v-row>
                <v-col cols="12" sm="8">
                    <v-select v-model="selectedActors.value" :items="availableActors" multiple label="Actors" @blur="selectedActors.isInValid = false"></v-select>
                    <p v-if="selectedActors.isInValid" :class="{ inValid: selectedActors.isInValid }">Please select Actor.
                    </p>

                </v-col>
                <v-col color="error" cols="12" sm="4">
                    <v-btn @click="setShouldAddPerson({ value: true, isActor: true })"
                        style="width: 100%; height: 56px;">Add
                        Actor</v-btn>
                </v-col>
            </v-row>


            <!-- single select -->
            <v-row>
                <v-col cols="12" sm="8">
                    <v-select v-model="selectedProducer.value" :items="availableProducer" label="Producer" @blur="selectedProducer.isInValid = false"></v-select>
                    <p v-if="selectedProducer.isInValid" :class="{ inValid: selectedProducer.isInValid }">Please select
                        Producer.</p>
                </v-col>
                <v-col color="error" cols="12" sm="4">
                    <v-btn @click="setShouldAddPerson({ value: true, isActor: false })"
                        style="width: 100%; height: 56px;">Add
                        Producer</v-btn>
                </v-col>
            </v-row>

            <!-- multi select -->
            <v-select v-model="selectedGeners.value" :items="availableGenres" multiple label="Genres" @blur="selectedGeners.isInValid = false"></v-select>
            <p v-if="selectedGeners.isInValid" :class="{ inValid: selectedGeners.isInValid }">Please select Genres.</p>


            <v-text-field v-model="plot.value" label="Plot" @blur="plot.isInValid = false"></v-text-field>
            <p v-if="plot.isInValid" :class="{ inValid: plot.isInValid }">Plot needs to be at least 1 characters.</p>


            <v-text-field v-model="poster.value" label="Poster" @blur="poster.isInValid = false"></v-text-field>
            <p v-if="poster.isInValid" :class="{ inValid: poster.isInValid }">Poster needs to be at least 1 characters.</p>


            <v-btn @click="handleSubmit" class="me-4" type="submit"> {{ movieId ? 'Edit Movie' : 'Add Movie' }} </v-btn>
            <v-btn v-if="movieId" @click="$router.push('/movies')"> Cancel</v-btn>
            <v-btn v-else @click="handleReset"> clear</v-btn>
        </form>
        <add-person v-if="shouldAddPerson.value" :shouldAddPerson="shouldAddPerson"
            @setAddActorProducer="setAddActorProducer"
            @setSelectedPerson="handleSetSelectedPerson"></add-person>
    </section>
</template>


<script>
import AddPerson from '../AddPerson.vue';
export default {
    components: {
        AddPerson
    },
    props: ['movieId'],
    computed: {
        availableActors() {
            return this.$store.getters.GetActors.map((actor) => `${actor.id}. ${actor.name}`);
        },
        availableProducer() {
            return this.$store.getters.GetProducers.map((producer) => `${producer.id}. ${producer.name}`);
        },
        availableGenres() {
            return this.$store.getters.GetGenres.map((genre) => `${genre.id}. ${genre.name}`);
        }
    },
    data() {
        return {
            shouldAddPerson: {
                value: false,
                isActor: false,
            },
            error: null,
            movieName: {
                value: '',
                isInValid: false,
            },
            yearOfRelease: {
                value: null,
                isInValid: false,
            },
            selectedActors: {
                value: [],
                isInValid: false,
            },
            selectedProducer: {
                value: null,
                isInValid: false,
            },
            selectedGeners: {
                value: [],
                isInValid: false,
            },
            plot: {
                value: '',
                isInValid: false,
            },
            poster: {
                value: '',
                isInValid: false,
            },
        }
    },
    methods: {
        setShouldAddPerson(payload) {
            this.shouldAddPerson = payload;
        },
        handleSetSelectedPerson(isActor, newlyCreatedEntity) {
            //'18. actorName' ,when we create the actor  we get actorId and actorName in response
            // we can use at build newlyCreatedActor and will emit this a custom event setSelectedActor ,
            if (isActor)
                this.selectedActors.value.push(newlyCreatedEntity);
            else
                this.selectedProducer.value = newlyCreatedEntity;
        },
        handleReset() {
            this.movieName = {
                value: '',
                isInValid: false,
            },
                this.yearOfRelease = {
                    value: null,
                    isInValid: false,
                },
                this.selectedActors = {
                    value: [],
                    isInValid: false,
                },
                this.selectedProducer = {
                    value: null,
                    isInValid: false,
                },
                this.selectedGeners = {
                    value: [],
                    isInValid: false,
                },
                this.plot = {
                    value: '',
                    isInValid: false,
                },
                this.poster = {
                    value: '',
                    isInValid: false,
                }
        },
        async handleSubmit(event) {
            event.preventDefault();
            if (this.movieName.value.length < 1)
                this.movieName.isInValid = true;
            else
                this.movieName.isInValid = false;

            if (!this.yearOfRelease.value)
                this.yearOfRelease.isInValid = true;
            else
                this.yearOfRelease.isInValid = false;

            if (this.selectedActors.value.length < 1)
                this.selectedActors.isInValid = true;
            else
                this.selectedActors.isInValid = false;

            if (this.selectedProducer.value == null)
                this.selectedProducer.isInValid = true;
            else
                this.selectedProducer.isInValid = false;

            if (this.selectedGeners.value.length < 1)
                this.selectedGeners.isInValid = true;
            else
                this.selectedGeners.isInValid = false;

            if (this.poster.value.length < 1)
                this.poster.isInValid = true;
            else
                this.poster.isInValid = false;

            if (this.plot.value.length < 1)
                this.plot.isInValid = true;
            else
                this.plot.isInValid = false;

            if (!this.movieName.isInValid && !this.yearOfRelease.isInValid && !this.selectedActors.isInValid
                && !this.selectedProducer.isInValid && !this.selectedGeners.isInValid && !this.poster.isInValid && !this.plot.isInValid) {
                const allActors = this.$store.getters.GetActors;

                //getting actorIds from the selectedActor
                const actorIds = allActors.filter((actor) => {
                    const currentActor = `${actor.id}. ${actor.name}`;
                    //if the currentActor is in selected actors then take it
                    return this.selectedActors.value.find(selectedActor => selectedActor === currentActor) != null;
                }).map(actor => actor.id).join(',');

                //getting producerId from the selectedProducer
                const allProducer = this.$store.getters.GetProducers;

                const producerId = allProducer.find((producer) => {
                    const currentProducer = `${producer.id}. ${producer.name}`;
                    //if the currentProducer is selected producers then take it
                    console.log(currentProducer);
                    return this.selectedProducer.value === currentProducer;
                }).id;

                //getting genreIds from the selectedGenre
                const allGenres = this.$store.getters.GetGenres;
                const genreIds = allGenres.filter((genre) => {
                    const currentGenre = `${genre.id}. ${genre.name}`;
                    //if the currentGenre is in selected genres then take it
                    return this.selectedGeners.value.find(selectedGenre => selectedGenre === currentGenre) != null;
                }).map(genre => genre.id).join(',');


                const movieData = {
                    Name: this.movieName.value,
                    YearOfRelease: this.yearOfRelease.value,
                    Plot: this.plot.value,
                    CoverImage: this.poster.value,
                    ProducerId: producerId,
                    ActorIds: actorIds,
                    GenreIds: genreIds,
                }

                try {
                    if (this.movieId) {
                        //in put we want 'movieId' as well as the movieData , but can send only argument while dispatching an action
                        movieData['movieId'] = this.movieId;
                        await this.$store.dispatch('UpdateMovie', movieData);
                    }
                    else
                        await this.$store.dispatch('CreateMovie', movieData);

                    this.$router.push('/movies');
                    this.$store.dispatch('loadMovies');
                }
                catch (error) {
                    if (error.response.status == 400) {
                        this.error = error.response.data;
                        console.log(error.response.data);
                    }
                    else
                        this.error = 'SOMETHING WENT WRONG, PLESE TRY AFTER SOMETIME';
                }
            }
        }
    },
    async created() {

        try {
            await this.$store.dispatch('loadActors');
            await this.$store.dispatch('loadProducers');
            await this.$store.dispatch('loadGenres');
        }
        catch (error) {
            if (error.response.status == 400) {
                this.error = error.response.data;
                console.log(error.response.data);
            }
            else {
                this.error = error.message;
            }
            return;
        }

        //if the route has 'movieId' means it is accessed on 'movies/:movieId/edit'  , means the data should be already filed in the textboxes
        // so get the data from db and update the local state


        if (this.movieId) {


            try {
                const movie = await this.$store.dispatch('loadMovie', this.movieId);
                console.log(movie);
                this.movieName.value = movie.name;
                this.yearOfRelease.value = movie.yearOfRelease
                this.plot.value = movie.plot;
                this.poster.value = movie.coverImage;

                // set selectedProducer for edit movie
                const allProducer = this.$store.getters.GetProducers;
                let selectedproducer;
                allProducer.find((producer) => {
                    if (producer.id == movie.producerId) {
                        selectedproducer = `${producer.id}. ${producer.name}`
                    }
                });
                this.selectedProducer.value = selectedproducer;


                // set selectedActor for edit movie
                const moviesActorIds = movie.actors.map(actor => actor.id);
                const allActors = this.$store.getters.GetActors;


                const selectedActors = allActors.filter((actor) => {
                    return moviesActorIds.find((actorId) => actorId == actor.id) != null;
                }).map((actor) => `${actor.id}. ${actor.name}`);
 
                this.selectedActors.value = selectedActors;

                //set selectedGenre for edit movie
                const moviesGenreIds = movie.genres.map(actor => actor.id);
                const allGenres = this.$store.getters.GetGenres;

                const selectedGenres = allGenres.filter((genre) => {
                    return moviesGenreIds.find((genreId) => genreId == genre.id) != null;
                }).map((genre) => `${genre.id}. ${genre.name}`);

                this.selectedGeners.value = selectedGenres;
            }
            catch (error) {
                if (error.response.status == 400) {
                    this.error = error.response.data;
                    console.log(error.response.data);
                }
                else
                    this.error = error.message;
            }
        }
    },
}
</script>



<style scoped>
.v-btn {
    background: lightgrey;
    color: black;
}

.inValid {
    margin-top: -20px;
    margin-bottom: 10px;
    color: red;
}

.header {
    margin: 25px;
}

.popup {
    background-color: white;
}
</style>