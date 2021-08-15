<template>
  <b-container class="container">
    <div class="d-flex justify-content-center flex-column align-items-center">
      <h1 class="mt-3">Welcome to Air Quality API</h1>
      <p>Here you can find air quality information by different locations</p>
    </div>
    <div class="d-flex justify-content-center">
      <b-dropdown id="dropdown-1" :text="selectedCountry || 'Select Country'" class="m-md-2">
        <div v-for="item in countries" :key="item.index">
          <b-dropdown-item @click="getAllCitiesAndSetCountry(item.code, item.name)">{{item.name}}</b-dropdown-item>
        </div>
      </b-dropdown>
    </div>
    <b-container class="w-50 mt-3" v-if="cities.results && cities.results.length > 0">
      <b-row>
        <div class="d-flex justify-content-center">
          <p class="d-flex justify-content-center flex-column">Sort:</p>
          <b-dropdown id="dropdown-1" :text="this.cityParameters.sort || 'Asc'" class="m-md-2">
            <b-dropdown-item @click="setSort('Asc')">Asc</b-dropdown-item>
            <b-dropdown-item @click="setSort('Desc')">Desc</b-dropdown-item>
          </b-dropdown>
          <p class="d-flex justify-content-center flex-column">Items Per Page:</p>
          <b-dropdown id="dropdown-1" :text="this.cityParameters.limit || '100'" class="m-md-2">
            <b-dropdown-item @click="setCitiesPerPage('5')">5</b-dropdown-item>
            <b-dropdown-item @click="setCitiesPerPage('10')">10</b-dropdown-item>
            <b-dropdown-item @click="setCitiesPerPage('25')">25</b-dropdown-item>
            <b-dropdown-item @click="setCitiesPerPage('50')">50</b-dropdown-item>
            <b-dropdown-item @click="setCitiesPerPage('100')">100</b-dropdown-item>
          </b-dropdown>
        </div>
      </b-row>
      <div>
        <b-row>
          <b-col class="d-flex justify-content-center p-3" col sm="12" col md="6" v-for="item in cities.results" :key="item.index">
            <BaseCard
              :title=item.city
              :card-text="item.lastUpdated"
              :locations="item.locations"
              router-link="/moreinfo"
            />
          </b-col>
        </b-row>
      </div>
      <div class="d-flex justify-content-center">
        <b-pagination
            v-model="currentPage"
            :total-rows="this.cities.meta.found"
            :per-page="this.cityParameters.limit"
            aria-controls="my-table"
            @input="loadMoreCities"
        ></b-pagination>
      </div>
    </b-container>
  </b-container>
</template>

<script>
import BaseCard from "@/components/BaseCard";
import { mapActions, mapGetters } from 'vuex';

export default {
  name: "Home",
  data() {
    return {
      currentPage: 1
    }
  },
  components: {
    BaseCard
  },
  async mounted() {
    try {
      await this.$store.dispatch('getCountries')
    } catch (err) {
      console.log(err);
      throw err;
    }
  },
  methods: {
    async getAllCitiesAndSetCountry(cityCode, country) {
      try {
        const queryParams = {
          country: cityCode
        }

        await this.$store.dispatch('getCities', queryParams)
        await this.$store.dispatch('setSelectedCountry', country)
      } catch (err) {
        console.log(err)
        throw err;
      }
    },
    async setCitiesPerPage(limit) {
      try {
        const queryParams = {
          limit: limit
        }

        await this.$store.dispatch('getCities', queryParams)
      } catch (err) {
        console.log(err)
        throw err
      }
    },
    async setSort(sortOrder) {
      try {
          const queryParams = {
            sort: sortOrder
          }

        await this.$store.dispatch('getCities', queryParams)
      } catch (err) {
        throw err
      }
    },
    async loadMoreCities() {
      try {
        const queryParams = {
          page: this.currentPage
        }

        await this.$store.dispatch('getCities', queryParams)
      } catch (err) {
        throw err;
      }
    },
  },
  computed: {
    ...mapActions(['getCountries', 'getCities', 'setSelectedCountry']),
    ...mapGetters({
      countries: 'getCountries',
      cities: 'getCities',
      selectedCountry: 'getSelectedCountry',
      cityParameters: 'getCityParameters'
    })
  }
};
</script>

<style lang="scss">
.dropdown-menu {
  max-height: 280px;
  overflow-y: scroll;
}
</style>
