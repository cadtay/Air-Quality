<template>
  <b-container class="p-4">
    <div>
      <div v-for="info in latestInfo" :key="info.index">
        <b-row>
          <div class="d-flex justify-content-center align-items-center flex-column">
            <p>Location: {{info.location}}</p>
          </div>
          <div class="d-flex justify-content-center align-items-center flex-column">
            <p>Latitude: {{info.coordinates.latitude}}</p>
            <p>Longitude: {{info.coordinates.longitude}}</p>
          </div>
        </b-row>
        <div v-for="measurement in info.measurements" :key="measurement.index">
          <b-row>
            <table class="table table-striped">
              <thead class="thead-dark">
              <tr>
                <th scope="col">Value</th>
                <th scope="col">Unit</th>
                <th scope="col">Parameter</th>
                <th scope="col">Last Updated</th>
              </tr>
              </thead>
              <tbody>
              <tr>
                <td class="w-25">{{ measurement.value }}</td>
                <td class="w-25">{{ measurement.unit }}</td>
                <td class="w-25">{{ measurement.parameter }}</td>
                <td class="w-25">{{ measurement.lastUpdated }}</td>
              </tr>
              </tbody>
            </table>
          </b-row>
        </div>
      </div>
    </div>
  </b-container>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';

export default {
  name: "MoreInfo",
  data() {
    return {
    }
  },
  async mounted() {
    try {
      await this.$store.dispatch('getLatestCityLocationInfo', this.city)
    } catch (err) {
      throw err
    }
  },
  methods: {
  },
  computed: {
    ...mapActions(['getLatestCityLocationInfo']),
    ...mapGetters({
      city: 'getSelectedCity',
      latestInfo: 'getCityLocationInfo'
    })
  },
}
</script>

<style scoped>

</style>
