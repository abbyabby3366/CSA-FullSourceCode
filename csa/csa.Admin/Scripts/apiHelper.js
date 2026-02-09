class ApiHelper {    
    static headers = {
        'Authorization': 'basic DSJDFS9t4oCJutYFVymbtXnK8k8oyQ5V'
    };

    static download(url) {
        return axios.get(url, {
            responseType: 'blob',
            headers: this.headers, validateStatus: function (status) {
                return status >= 200 && status < 500
            }
        })
    }

    static post(url, data) {
        return axios.post(url, data, {
            headers: this.headers, validateStatus: function (status) {
                return status >= 200 && status < 500
            }
        })
    }

    static postFormData(url, data) {
        return axios.post(url, data, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }, validateStatus: function (status) {
                return status >= 200 && status < 500
            }
        })
    }

    static createSign(params) {
        const sortedKeys = Object.keys(params).sort();
        let message = ''
        for (const key of sortedKeys) {
            const value = params[key]
            message += '&'
            message += `${key}=${value}`
        }
        message = message.substring(1);
        return this.generateHashing(message)
    }

    static generateHashing(message) {
        const signKey = 'BRG#h6'
        const secretKeyBytes = CryptoJS.enc.Utf8.parse(signKey)
        const dataBytes = CryptoJS.enc.Utf8.parse(message)
        const hmacSha256 = CryptoJS.HmacSHA256(dataBytes, secretKeyBytes)
        const digest = hmacSha256.toString(CryptoJS.enc.Hex)

        return digest
    }
}