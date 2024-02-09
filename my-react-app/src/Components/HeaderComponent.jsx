import React, {useEffect, useState} from "react";
import  ResponsiveAppBar  from "../Components/Navbar.jsx";
import {Button} from "@mui/material";
import {Link, useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import BackOfficeNavigation from "./BackOfficeNavigation.jsx";

const HeaderComponent = () => {
    return (
        <>
            <header>
                <ResponsiveAppBar/>
            </header>
        </>
    );
}

export default HeaderComponent;