import React, {useEffect, useState} from "react";
import { Typography, Button } from "@mui/material";

import {Link, useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import CartsService from "../Services/CartsService.jsx";
import ProductComponent from "./ProductComponent.jsx";

const CartContentComponent = () => {
    const navigate = useNavigate();
    const [myCart, setMyCart] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    const [cookie, setCookie, removeCookie] = useCookies(['bearerToken']);


    useEffect(() => {
        fetchMyCart();
    }, [])
    const fetchMyCart = async () =>{
        const myCart = await CartsService.loadCart(0, cookie.bearerToken);
        setMyCart(myCart)
        setIsLoading(false)
    }


    return (
        <>
            <Typography>Mon panier :</Typography>
            {!isLoading && !myCart &&
                <>Vous n'avez pas encore de panier : Ajouter des produits avant de passer au paimeent :) </>
            }
            {!isLoading && myCart && myCart.cartLines.length === 0 &&
                <>Votre panier est vide</>
            }
            {!isLoading && myCart && myCart.cartLines.length > 0 &&
                myCart.cartLines.map((cartLine, key) => {
                    return (
                        <>
                            <img src={cartLine.product.image} width="80x"/>
                            <Typography key={key} >{cartLine.product.name} : {cartLine.quantity}</Typography>
                        </>
                    )
                })
            }
            {isLoading && <>chargement...</>}
        </>
    );
}

export default CartContentComponent;