import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";

import { Link } from "react-router-dom"
import {useCookies} from "react-cookie";

const MainPage = () => {
    const [isLogged, setIsLogged] = useState(false)

    const [cookie, setCookie, removeCookie] = useCookies(['bearerToken']);


    useEffect(() => {
        if(cookie.bearerToken){
            setIsLogged(true)
        }else{
            setIsLogged(false)
        }
    }, [cookie]);

    const handleDisconnect = (event) =>{
        event.preventDefault();
        removeCookie('bearerToken')
    }

    return (
        <React.Fragment>
            {!isLogged &&
                <Link to="/login">Login here</Link>
            }

            {isLogged &&
                <>
                    <Typography>Vous êtes connecté !</Typography>
                    <Button onClick={handleDisconnect}>Se déconnecter</Button>
                </>
            }

        </React.Fragment>
    );
}

export default MainPage;