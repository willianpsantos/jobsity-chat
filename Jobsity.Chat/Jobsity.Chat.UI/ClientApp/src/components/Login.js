import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import AuthService from '../services/AuthService';
import { settings } from '../settings';
import "../css/login.css";

export function Login(props) {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [ signingIn, setSigningIn ] = useState(false);

    const authService = new AuthService(settings.apiUrl);

    const signIn = (e) => {
        setSigningIn(true);
        
        authService
            .authenticate(email, password)
            .then(response => {
                setSigningIn(false);

                if (!response || !response.success) {
                    const msg = response.errors.join('\n');
                    alert(msg);
                    return;
                }

                navigate('/');
            })
            .catch(error => {
                setSigningIn(false);
            });
    }

    return (
        <div className="login-container">
            <form id="frmLogin">
                <div className="row">
                    <div className="form-group">
                        <label> E-mail </label>

                        <input
                            id="email"
                            name="email"
                            type="email"
                            className="form-control"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)} />
                    </div>
                </div>

                <div className="row">
                    <div className="form-group">
                        <label> Password </label>

                        <input
                            id="password"
                            name="password"
                            type="password"
                            className="form-control"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)} />
                    </div>
                </div>

                <div className="row">
                    <button type="button" 
                            className="btn btn-success" 
                            disabled={signingIn}
                            onClick={signIn}>
                        {signingIn ? 'Signing In ...' : 'Sign In' } 
                    </button>
                </div>
            </form>
        </div>
    );
}