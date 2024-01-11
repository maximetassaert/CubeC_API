import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";

import { Link } from "react-router-dom"
import {useCookies} from "react-cookie";
import ProductTableComponent from "../Components/ProductTableComponent.jsx";
import HeaderComponent from "../Components/HeaderComponent.jsx";

const MainPage = () => {

    return (
        <>
            <HeaderComponent/>
            <ProductTableComponent/>
        </>

    );
}

export default MainPage;