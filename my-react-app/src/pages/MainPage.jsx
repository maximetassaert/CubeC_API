import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";

import { Link } from "react-router-dom"
import {useCookies} from "react-cookie";
import ProductTableComponent from "../Components/ProductTableComponent.jsx";
import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";

const MainPage = () => {

    return (
        <>
            <HeaderComponent/>
            <main>
                <div className="flex flex-wrap">
                    <ProductTableComponent/>
                </div>
            </main>
            <Footer/>
        </>

    );
}

export default MainPage;