import React, {useEffect, useState} from "react";
import {Box, Typography} from "@mui/material";

import {useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import CartsService from "../Services/CartsService.jsx";

const CartContentComponent = () => {
    const navigate = useNavigate();
    const [myCart, setMyCart] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    const [cookie, setCookie, removeCookie] = useCookies(['bearerToken']);


    useEffect(() => {
        fetchMyCart();
    }, [])
    const fetchMyCart = async () => {
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
                        <Box key={key}>
                            <img src={cartLine.product.image} width="80x"/>
                            <Typography>{cartLine.product.name} : {cartLine.quantity}</Typography>
                        </Box>
                    )
                })
            }
            {isLoading && <>chargement...</>}
        </>
    );
}

export default CartContentComponent;