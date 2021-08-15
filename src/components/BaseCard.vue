<template>
  <div class="card" style="width: 18rem;">
    <div class="card-body">
      <h5 class="card-title">{{title}}</h5>
      <p class="card-text">Locations: {{locations}}</p>
      <p class="card-text">Last Updated: {{cardText | formatDate}}</p>
      <router-link @click.native="setCity" to="">More Info</router-link>
    </div>
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import router from "@/router";

export default {
  name: "BaseCard",
  props: {
    title: String,
    cardText: String,
    locations: Number,
  },
  filters: {
    formatDate: function(value) {
      return new Date(value).toString()
    },
  },
  methods: {
    async setCity() {
      try {
        await this.$store.dispatch('setSelectedCity', this.title)
        await router.push({name: 'moreinfo'})
      } catch (err) {
        throw err
      }
    },
  },
  computed: {
    ...mapActions(['setSelectedCity'])
  },
}
</script>

<style scoped>

</style>
