import React, {useEffect, useState} from "react";
import {Button} from "@mui/material";

import {Link, useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import BackOfficeNavigation from "./BackOfficeNavigation.jsx";

const HeaderComponent = () => {
    const [isLogged, setIsLogged] = useState(false)
    const [isAdmin, setIsAdmin] = useState(false)

    const navigate = useNavigate();

    const [cookie, setCookie, removeCookie] = useCookies(['bearerToken', 'roles']);


    useEffect(() => {
        if (cookie.bearerToken) {
            setIsLogged(true)
            if (cookie.roles.includes('admin')) setIsAdmin(true)
        } else {
            setIsLogged(false)
        }
    }, [cookie]);

    const handleDisconnect = (event) => {
        event.preventDefault();
        removeCookie('bearerToken')
        removeCookie('roles')
    }

    const handleMyCart = (event) => {
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
                            <li><Link to={'/'}>Accueil</Link></li>
                            <li>
                                <Button onClick={handleDisconnect}>Se déconnecter</Button>
                            </li>
                            <li>
                                <Button onClick={handleMyCart}>Mon panier</Button>
                            </li>

                            {isAdmin && <>
                                <BackOfficeNavigation/>
                            </>}
                        </ul>

                    </>
                }
            </header>
        </>
    );
}

export default HeaderComponent;