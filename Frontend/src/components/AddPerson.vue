<template>
    <div class="text-center">
        <v-dialog v-model="dialog" width="700px">

            <section v-if="error" style="background-color: white;" class="section-container">
                <div>{{ error }}</div>
                <div>
                    <router-link to="/movies">Back to Movie Listing Page</router-link>
                </div>
            </section>
            <form v-else @submit.prevent="submit" style="background-color: white;">
                <v-text-field v-model="name.value"
                    :label="shouldAddPerson.isActor ? 'Actor Name' : 'Producer Name'"></v-text-field>
                <p v-if="name.isInvalid" :class="{ inValid: name.isInvalid }">Name should be atleast 1 character long.</p>

                <v-text-field v-model="bio.value"
                    :label="shouldAddPerson.isActor ? 'Actor bio' : 'Producer bio'"></v-text-field>
                <p v-if="bio.isInvalid" :class="{ inValid: bio.isInvalid }">bio should be atleast 5 character long.</p>

                <v-text-field type="date" v-model="dob.value" label="Date of Birth"></v-text-field>
                <p v-if="dob.isInvalid" :class="{ inValid: dob.isInvalid }">Dob Cannot be empty.</p>

                <!-- multi select -->
                <v-select v-model="selectedGender.value" :items="availableGender" label="Gender"></v-select>
                <p v-if="selectedGender.isInvalid" :class="{ inValid: selectedGender.isInvalid }">Please select Gender.</p>

                <div class="add-delete-button">
                    <v-btn style="margin:0px 8px 8px 8px;" color="green" @click="handleAdd">Add {{ shouldAddPerson.isActor
                        ?
                        'Actor' : 'Producer' }}</v-btn>
                    <v-btn style="margin:0px 8px 8px 8px;" color="red" @click="handleCancel">Cancel</v-btn>
                </div>
            </form>
        </v-dialog>
    </div>
</template>

<script>
export default {
    props: ['shouldAddPerson'],
    emits: ['setShouldAddPerson', 'setSelectedPerson'],
    data() {
        return {
            dialog: true,
            error: null,
            availableGender: ['Male', 'Female', 'Others'],
            name: {
                value: '',
                isInvalid: false,
            },
            bio: {
                value: '',
                isInvalid: false,
            },
            dob: {
                value: null,
                isInvalid: false,
            },
            selectedGender: {
                value: null,
                isInvalid: false,
            },
        }
    },
    watch: {
        dialog(value) {
            if (value == false) 
            {
                //side effect = if dailog is false then we should emit one event with false value
                const currentValue = this.shouldAddPerson;
                currentValue.value = false;
                this.$emit('setShouldAddPerson', currentValue);
            }
        }
    },
    methods: {
        setDialog(payload) {
            this.dialog = payload;
        },
        async handleAdd() {

            if (this.name.value.length < 1)
                this.name.isInvalid = true;
            else
                this.name.isInvalid = false;

            if (this.bio.value.length < 1)
                this.bio.isInvalid = true;
            else
                this.bio.isInvalid = false;

            if (this.selectedGender.value == null)
                this.selectedGender.isInvalid = true;
            else
                this.selectedGender.isInvalid = false;

            if (this.dob.value == null)
                this.dob.isInvalid = true;
            else
                this.dob.isInvalid = false;

            if (!this.name.isInvalid && !this.bio.isInvalid && !this.dob.isInvalid && !this.selectedGender.isInvalid) {
                const data = { Name: this.name.value, Bio: this.bio.value, DOB: this.dob.value, Gender: this.selectedGender.value };
                data.DOB = this.convertDateFormat(data.DOB);

                try {
                    this.error = null;
                    if (this.shouldAddPerson.isActor) {
                        // call the api to add the actor in db

                        const response = await this.$store.dispatch('AddActor', data);
                        const newlyCreatedActor = `${response.data.id}. ${response.data.name}`;
                        // call the custom event to add the newly created producer as selected actor
                        this.$emit('setSelectedPerson', this.shouldAddPerson.isActor, newlyCreatedActor);
                        this.$store.dispatch('loadActors');
                        this.setDialog(false);
                    }
                    else {
                        // call the api to add the producer in db

                        const response = await this.$store.dispatch('AddProducer', data);
                        const newlyCreatedProducer = `${response.data.id}. ${response.data.name}`;
                        console.log(response);
                        console.log(newlyCreatedProducer);
                        // call the custom event to add the newly created producer as selected producer
                        this.$emit('setSelectedPerson', this.shouldAddPerson.isActor, newlyCreatedProducer);

                        this.$store.dispatch('loadProducers');
                        this.setDialog(false);
                    }
                }
                catch (error) {
                    if (error.message === 'Network Error')
                        this.error = 'Server is down. Please try after some time.'
                    else if (error.response.status == 400) {
                        this.error = error.response.data;
                        console.log(error.response.data);
                    }
                    else
                        this.error = error.message;
                }

            }
        },
        handleCancel() {
            this.setDialog(false);
        },
        convertDateFormat(inputDate) {

            var dateComponents = inputDate.split("-");
            var year = dateComponents[0];
            var month = dateComponents[1];
            var day = dateComponents[2];

            var convertedDate = new Date(year, month - 1, day);

            var formattedDate = convertedDate.getDate().toString().padStart(2, '0') + "/" +
                (convertedDate.getMonth() + 1).toString().padStart(2, '0') + "/" +
                convertedDate.getFullYear();

            return formattedDate;
        }
    },
}
</script>

<style scoped>
.inValid {
    margin-top: -20px;
    margin-bottom: 10px;
    color: red;
}

.add-delete-button {
    display: flex;
    justify-content: flex-end;
    background-color: white;
}
</style>