import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";
import  ResponsiveAppBar  from "../Components/Navbar.jsx";
import {Link, useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import ProductTableComponent from "../Components/ProductTableComponent.jsx";

const HeaderComponent = () => {


    return (
        <>
            <header>
            {/* {!isLogged &&
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
            } */}
            <ResponsiveAppBar/>
            </header>
        </>
    );
}

export default HeaderComponent;