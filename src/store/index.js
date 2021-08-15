import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";
import AirQualityService from '@/services/air-quality-service'
import BuildQueryString from "@/helpers/build-query-string";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    countries: [],
    cities: {},
    cityLocationInfo: [],
    selectedCountry: null,
    selectedCity: null,
    cityParameters: {}
  },
  mutations: {
    setCountries(state, payload) {
      state.countries = payload
    },
    setCities(state, payload) {
      state.cities = payload
    },
    setCityLocationInfo(state, payload) {
      state.cityLocationInfo = payload
    },
    setSelectedCountry(state, payload) {
      state.selectedCountry = payload
    },
    setSelectedCity(state, payload) {
      state.selectedCity = payload
    },
    setCityParameters(state, payload) {
      state.cityParameters = Object.assign(state.cityParameters, payload)
    }
  },
  actions: {
    async getCountries(state) {
      return new Promise((resolve, reject) => {
        AirQualityService.getCountries().then((resp) => {
          const countries = resp.data.results
          state.commit('setCountries', countries)
          resolve(countries)
        }).catch((err) => {
          reject(err)
        })
      })
    },
    async getCities(state, payload) {
      state.commit('setCityParameters', payload)
      const queryString = BuildQueryString.buildQueryString(this.state.cityParameters)

      return new Promise((resolve, reject) => {
        AirQualityService.getCities(queryString).then((resp) => {
          const cities = resp.data
          state.commit('setCities', cities)
          resolve(cities)
        }).catch((err) => {
          reject(err)
        })
      })
    },
    async getLatestCityLocationInfo(state, payload) {
      return new Promise((resolve, reject) => {
        AirQualityService.getlatestByCity(payload).then((resp) => {
          const locationInfo = resp.data.results
          state.commit('setCityLocationInfo', locationInfo)
          resolve(locationInfo)
        }).catch((err) => reject(err))
      })
    },
    async setSelectedCountry(state, payload) {
      state.commit('setSelectedCountry', payload)
    },
    async setSelectedCity(state, payload) {
      state.commit('setSelectedCity', payload)
    },
    async setCityParameters(state, payload) {
      state.commit('setCityParameters', payload)
    },
  },
  getters: {
    getCountries: state => state.countries,
    getCities: state => state.cities,
    getCityLocationInfo: state => state.cityLocationInfo,
    getSelectedCountry: state => state.selectedCountry,
    getSelectedCity: state => state.selectedCity,
    getCityParameters: state => state.cityParameters
  },
  plugins: [createPersistedState({ storage: window.sessionStorage })]
});
