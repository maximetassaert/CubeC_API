import React, {useEffect, useState} from "react";
import {TextField, Typography,} from "@mui/material";
import { LoadingButton } from "@mui/lab";

import {Link, useNavigate} from "react-router-dom"
import axios, {HttpStatusCode} from 'axios';
import {useCookies} from "react-cookie";
import ProductComponent from "./ProductComponent.jsx";
import ProductsService from "../Services/ProductsService.jsx";

const ProductTableComponent = () => {
    const [products, setProducts] = useState([])


    const [cookie, setCookie] = useCookies(['bearerToken']);
    const [isLoading, setIsLoading] = useState(false)

    const fetchProducts = async () =>{
        const productsr = await ProductsService.findAll(cookie.bearerToken);
        setProducts(productsr)
    }
    useEffect( () => {
        fetchProducts();

    }, []);

    return (
        <>
            {products.length === 0 && <>
                Chargement des produits ...
            </>}
            { products.length > 0 && products.map((product, key) => {
                return (
                        <ProductComponent key={key} product={product} />
                )
                })}
        </>
    );
}

export default ProductTableComponent;