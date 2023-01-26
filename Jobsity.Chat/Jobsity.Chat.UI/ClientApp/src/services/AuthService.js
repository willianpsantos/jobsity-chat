import Service from "./Service";

export default class AuthService extends Service
{
    setAuthKey(id) {
        if (!id) {
            window.sessionStorage.removeItem(this._authKeyName);
            return;
        }

        window.sessionStorage.setItem(this._authKeyName, id);
    }
    
    setToken(token) {
        if (!token) {
            window.sessionStorage.removeItem(this._tokenName);
            return;
        }

        window.sessionStorage.setItem(this._tokenName, token);
    }    

    authenticate(email, password) {
        return new Promise((resolve, reject) => {
            this._allowAnonymous = true;

            this
                .post({ url:'/user/auth', body: { email, password } })
                .then(response => {
                    this._allowAnonymous = false;

                    if( !response || !response.success ) {
                        this.setToken(null);
                        this.setAuthKey(null);
                        return;
                    }

                    this.setToken(response.token);
                    this.setAuthKey(response.id);
                    resolve(response);
                })
                .catch(error => {
                    this._allowAnonymous = false;
                    reject(error);
                });
        });

        
    }
}