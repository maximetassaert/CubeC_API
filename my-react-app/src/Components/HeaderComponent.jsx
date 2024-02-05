import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";

import {Link, useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import ProductTableComponent from "../Components/ProductTableComponent.jsx";

const HeaderComponent = () => {
    const [isLogged, setIsLogged] = useState(false)
    const navigate = useNavigate();

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
        removeCookie('roles')
    }

    const handleMyCart = (event) =>{
        event.preventDefault();
        navigate('/myCart')
    }

    return (
        <>
            <header>
            {!isLogged &&
                <Link to="/login">Login here</Link>
            }

            {isLogged &&
                <>
                <ul>
                    <li>
                        <Button onClick={handleDisconnect}>Se déconnecter</Button>
                    </li>
                    <li>
                        <Button onClick={handleMyCart}>Mon panier</Button>
                    </li>

                </ul>

                </>
            }
            </header>
        </>
    );
}

export default HeaderComponent;