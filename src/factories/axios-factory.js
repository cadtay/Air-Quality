import axios from 'axios'

const axiosFactory = {
    createRequest(baseUrl) {
        const request = axios.create({baseURL: baseUrl});

        request.interceptors.response.use(
            res => res
        )
        return request
    }
}

export default axiosFactory;
