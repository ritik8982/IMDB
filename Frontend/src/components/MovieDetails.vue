<template>
    <div class="text-center">
      <v-dialog  v-model="dialog" width="700px">
        <v-card>
            <v-card-title>{{ name }}</v-card-title>
          <v-card-text>
            {{ description }}
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" block @click="setDialog(false)">Close</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
  </template>


<script>
  export default {
    data () {
      return {
        dialog: true,
      }
    },
    methods:{
        setDialog(payload)
        {
            this.dialog = payload;
            this.$emit('close',false);
        }
    },
    props: ['name', 'description','ShowDialog'],
    emits : ['close'],
  }
</script>