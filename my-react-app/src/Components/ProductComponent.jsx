import React, {useState} from "react";
import {Typography,} from "@mui/material";
import {LoadingButton} from "@mui/lab";

import {useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import CartsService from "../Services/CartsService.jsx";


const ProductComponent = (props) => {
    const {product} = props;


    const [cookie, setCookie] = useCookies(['bearerToken', 'cart']);
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false)


    const handleSubmit = async (event) => {
        event.preventDefault()
        setIsLoading(true)

        const formData = Array.from(event.target.elements)
            .filter(el => el.name)
            .reduce((a, b) => ({...a, [b.name]: b.value}), {});

        const productToAdd = {
            productId: formData.productId,
            quantity: 1
        }

        const currentCart = cookie.cart;
        if (!currentCart) {
            const currentCart = {
                customerId: 0,
                cartLines: [productToAdd],
            }
            const cart = await CartsService.createCart(currentCart, cookie.bearerToken)
            if (cart) setCookie('cart', cart)
        } else {
            const productInCart = currentCart.cartLines.find(product => product.productId == productToAdd.productId)

            if (productInCart) {
                productInCart.quantity += 1;
            } else {
                currentCart.cartLines.push(productToAdd)
            }
            const cart = await CartsService.updateCart(currentCart, cookie.bearerToken)
            setCookie('cart', cart)
        }

        setIsLoading(false)
    }

    return (
        <React.Fragment>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <h2>{product.name}</h2>

                <Typography>
                    {product.description}
                </Typography>
                <img src={product.image} width="80px"/>
                <Typography>
                    {product.price} € TTC
                </Typography>

                <LoadingButton variant="outlined" color="secondary" type="submit" loading={isLoading}>Ajouter au
                    panier</LoadingButton>
                <input hidden value={product.id} readOnly name="productId"/>
            </form>
        </React.Fragment>
    );
}

export default ProductComponent;