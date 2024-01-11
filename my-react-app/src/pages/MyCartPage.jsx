import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";

import { Link } from "react-router-dom"
import {useCookies} from "react-cookie";
import ProductTableComponent from "../Components/ProductTableComponent.jsx";
import HeaderComponent from "../Components/HeaderComponent.jsx";
import CartContentComponent from "../Components/CarContentComponent.jsx";

const MyCartPage = () => {

    return (
        <>
            <HeaderComponent/>
            <CartContentComponent/>
        </>

    );
}

export default MyCartPage;