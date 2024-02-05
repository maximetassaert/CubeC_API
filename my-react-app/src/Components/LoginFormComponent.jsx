import React, {useState} from "react";
import {TextField, Typography,} from "@mui/material";
import { LoadingButton } from "@mui/lab";

import {Link, useNavigate} from "react-router-dom"
import axios, {HttpStatusCode} from 'axios';
import {useCookies} from "react-cookie";

const LoginFormComponent = () => {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [emailError, setEmailError] = useState(false)
    const [passwordError, setPasswordError] = useState(false)
    const [credentialsError, setCredentialsError] = useState(false)

    const [isLoading, setIsLoading] = useState(false)

    const [cookie, setCookie] = useCookies(['bearerToken']);
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault()

        setEmailError(false)
        setPasswordError(false)

        if (email === '') {
            setEmailError(true)
        }
        if (password === '') {
            setPasswordError(true)
        }

        if (email && password) {
            setIsLoading(true)
            try{
                const result = await axios.post(import.meta.env.VITE_API_BASE_URL + '/auth/login', {email, password})

                if(result.data.bearerToken){
                    setCredentialsError(false)
                    setCookie('bearerToken', result.data.bearerToken)
                    setCookie('roles', result.data.roles)

                    navigate('/')
                }

            }catch(error){
                if(error.response.status === HttpStatusCode.Unauthorized){
                    setCredentialsError(true)
                }else{
                    console.error(error)
                }
            }

            setIsLoading(false)

        }
    }

    return (
        <React.Fragment>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <h2>Login</h2>
                {credentialsError &&
                    <Typography>
                        Vos identifiants ne correspondent à aucun compte enregistré
                    </Typography>
                }
                <TextField
                    label="Email"
                    onChange={e => setEmail(e.target.value)}
                    required
                    variant="outlined"
                    color="secondary"
                    type="email"
                    sx={{mb: 3}}
                    fullWidth
                    value={email}
                    error={emailError}
                />
                <TextField
                    label="Password"
                    onChange={e => setPassword(e.target.value)}
                    required
                    variant="outlined"
                    color="secondary"
                    type="password"
                    value={password}
                    error={passwordError}
                    fullWidth
                    sx={{mb: 3}}
                />
                <LoadingButton variant="outlined" color="secondary" type="submit" loading={isLoading}>Login</LoadingButton>

            </form>
            <small>Need an account? <Link to="/">Register here</Link></small>
        </React.Fragment>
    );
}

export default LoginFormComponent;