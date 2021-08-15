import AxiosFactory from "@/factories/axios-factory";

const axiosRequestFactory = AxiosFactory.createRequest(process.env.VUE_APP_AIR_QUALITY_API)

export default {
    async getCountries() {
        return new Promise((resolve, reject) => {
            return axiosRequestFactory.get('/v1/countries').then((resp) => {
                resolve(resp)
            }).catch((error) => {
                reject(error)
            })
        })
    },
    async getCities(queryString) {
        return new Promise((resolve, reject) => {
            return axiosRequestFactory.get(`/v1/cities${queryString}`).then((resp) => {
                resolve(resp)
            }).catch((error) => {
                reject(error)
            })
        })
    },
    async getlatestByCity(city) {
        return new Promise((resolve, reject) => {
            return axiosRequestFactory.get(`/v1/airquality?city=${city}`).then((resp) => {
                resolve(resp)
            }).catch((error) => {
                reject(error)
            })
        })
    }
}
