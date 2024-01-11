import React, {useState} from "react";
import {TextField, Typography,} from "@mui/material";
import { LoadingButton } from "@mui/lab";

import {Link, useNavigate} from "react-router-dom"
import axios, {HttpStatusCode} from 'axios';
import {useCookies} from "react-cookie";

const ProductComponent = (props) => {
    const { product } = props;


    const [cookie, setCookie] = useCookies(['bearerToken']);
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false)


    const handleSubmit = async (event) => {
        event.preventDefault()
        setIsLoading(true)


        setIsLoading(false)
    }

    return (
        <React.Fragment>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <h2>{product.name}</h2>

                <Typography>
                    { product.description }
                </Typography>

                <Typography>
                    12(en dur ) € TTC
                </Typography>

                <LoadingButton variant="outlined" color="secondary" type="submit" loading={isLoading}>Ajouter au panier</LoadingButton>

            </form>
        </React.Fragment>
    );
}

export default ProductComponent;