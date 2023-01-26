export default class Service
{
    constructor(apiUrl) {
        this._apiUrl = apiUrl;
        this._tokenName = "token";
        this._authKeyName = 'auth-key';
        this._allowAnonymous = false;
    }

    toFormData(obj, except = []) {
        const formData = new FormData();
    
        for(let prop in obj) {
            if (except && except.includes(prop))
                continue;

            formData.append(prop, obj[prop]);
        }

        return formData;
    }

    toQueryString(obj, except = []) {
        if (!obj)
            return '';

        const table = [];
    
        for(let prop in obj) {
            if (except && except.includes(prop))
                continue;

            table.push(`${prop}=${obj[prop]}`);
        }

        return '?' + table.join('&');
    }

    isAuthenticated() {
        const token = window.sessionStorage.getItem(this._tokenName);
        return token !== null && token !== undefined && token !== "" && token !== false;
    }

    getToken() {
        return window.sessionStorage.getItem(this._tokenName);
    }

    getAuthKey() {
        return window.sessionStorage.getItem(this._authKeyName);
    }

    post({ url, body, headers }) {        
        return new Promise((resolve, reject) => {
            if(!this.isAuthenticated() && !this._allowAnonymous) {
                resolve([]);
                return;
            }

            if (!headers)
                headers = {};

            headers['Accept'] = "text/plain,application/json"
            headers['Content-Type'] = 'application/json';

            if(this.isAuthenticated()) {
                const token = this.getToken();
                headers['Authorization'] = `Bearer ${token}`;
            }

            const fullUrl = `${this._apiUrl}${url}`;

            fetch(fullUrl, {
                method: 'POST',
                headers,
                body: JSON.stringify(body)
            })
            .then(response => response.json())
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                alert(error);
                reject(error);
            });
        });        
    }

    get({ url, params, headers }) {        
        return new Promise((resolve, reject) => {
            if(!this.isAuthenticated() && !this._allowAnonymous) {
                resolve([]);
                return;
            }

            if (!headers)
                headers = {};

            headers['Accept'] = "text/plain,application/json"
            headers['Content-Type'] = 'application/json';

            if(this.isAuthenticated()) {
                const token = this.getToken();
                headers['Authorization'] = `Bearer ${token}`;
            }
            
            const fullUrl = `${this._apiUrl}${url}${this.toQueryString(params)}`;

            fetch(fullUrl, {
                method: 'GET',
                headers
            })
            .then(response => response.json())
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                alert(error);
                reject(error);
            });
        });        
    }
}