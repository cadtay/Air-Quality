export default {
    buildQueryString(payload) {
        let queryParams = ''

        for (const [key, value] of Object.entries(payload)) {
            if (!queryParams) {
                if (value) {
                    queryParams += `?${key}=${value}`
                }
            } else {
                if (value) {
                    queryParams += `&${key}=${value}`
                }
            }
        }
        return queryParams
    }
}
