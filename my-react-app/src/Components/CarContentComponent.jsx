import React, {useEffect, useState} from "react";
import {Box, Typography} from "@mui/material";

import {useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import CartsService from "../Services/CartsService.jsx";
import {LoadingButton} from "@mui/lab";
import OrderService from "../Services/OrderService.jsx";

const CartContentComponent = () => {
    const navigate = useNavigate();
    const [myCart, setMyCart] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [isOrderValidated, setIsOrderValidated] = useState(false);

    const [orderCreationLoading, setOrderCreationLoading] = useState(false);


    const [cookie, setCookie, removeCookie] = useCookies(['bearerToken', 'cart']);


    useEffect(() => {
        fetchMyCart();
    }, [])
    const fetchMyCart = async () => {
        const myCart = await CartsService.loadCart(0, cookie.bearerToken);
        setMyCart(myCart)
        setIsLoading(false)
    }

    const handleCartValidation = () => {
        createOrderFromCart()
    }

    const createOrderFromCart = async () => {
        setOrderCreationLoading(true)
        const orderDto = {customerId: myCart.customerId, cartId: myCart.id};
        await OrderService.createOrder(orderDto, cookie.bearerToken)
        setIsOrderValidated(true)
        setOrderCreationLoading(false);
        setMyCart(null)
        removeCookie('cart')
    }


    return (
        <>
            {isOrderValidated && <Typography>Votre commande a bien été validée !</Typography>}
            {!isOrderValidated &&
                <Typography>Mon panier :</Typography>
            }
            {!isOrderValidated && !isLoading && !myCart &&
                <>Vous n'avez pas encore de panier : Ajouter des produits avant de passer au paiement :) </>
            }
            {!isOrderValidated && !isLoading && myCart && myCart.cartLines.length === 0 &&
                <>Votre panier est vide</>
            }
            {!isOrderValidated && !isLoading && myCart && myCart.cartLines.length > 0 && <>
                {myCart.cartLines.map((cartLine, key) => {
                    return (
                        <Box key={key}>
                            <img src={cartLine.product.image} width="80x"/>
                            <Typography>{cartLine.product.name} : {cartLine.quantity}</Typography>
                        </Box>
                    )
                })
                }

                <LoadingButton onClick={handleCartValidation} loading={orderCreationLoading}>
                    Valider mon panier et passer ma commande
                </LoadingButton>

            </>}
            {isLoading && <>chargement...</>}
        </>
    );
}

export default CartContentComponent;