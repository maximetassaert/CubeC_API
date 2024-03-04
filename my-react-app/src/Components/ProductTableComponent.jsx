import React, {useEffect, useState} from "react";
import {Typography,} from "@mui/material";
import {useCookies} from "react-cookie";
import ProductComponent from "./ProductComponent.jsx";
import ProductsService from "../Services/ProductsService.jsx";

const ProductTableComponent = () => {
    const [products, setProducts] = useState([])
    const [productsFiltered, setProductsFiltered] = useState([])
    const [searchValue, setSearchValue] = useState('')
    const [priceOrderValue, setPriceOrderValue] = useState('')
    const [colorValue, setColorValue] = useState('')
    const [familyValue, setFamilyValue] = useState('')


    const [cookie, setCookie] = useCookies(['bearerToken']);
    const [isLoading, setIsLoading] = useState(false)

    const fetchProducts = async () => {
        const productsr = await ProductsService.findAll(cookie.bearerToken);
        setProducts(productsr)
        setProductsFiltered(productsr)
    }
    useEffect(() => {
        fetchProducts();

    }, []);

    useEffect(() => {
        handleSearch()

    }, [searchValue, products, colorValue, familyValue])

    const handleSearch = () => {
        if (searchValue.trim() === '') setProductsFiltered(products);

        let tmpProducts = products.filter(product => product.name.toLowerCase().includes(searchValue.toLowerCase()))
        if (colorValue !== '') tmpProducts = tmpProducts.filter(product => product.color.toLowerCase() === colorValue.toLowerCase())
        if (familyValue !== '') tmpProducts = tmpProducts.filter(product => product.family.toLowerCase() === familyValue.toLowerCase())

        handleSort(tmpProducts)
    }

    const handleSort = (arrayToSort) => {
        const tmpProducts = arrayToSort.sort((productA, productB) => {
            const returnNumber = priceOrderValue.trim() === 'asc' ? -1 : 1;
            if (productA.price === productB.price) return 0;

            if (productA.price < productB.price) return returnNumber;
            else return -returnNumber;
        })

        setProductsFiltered(tmpProducts)
    }

    useEffect(() => {
        if (priceOrderValue.trim() === '') {
            handleSearch()
            return;
        }

        handleSort([...productsFiltered]);
    }, [priceOrderValue])

    return (
        <>
            <Typography>Recherche :
                <input value={searchValue}
                       onChange={e => setSearchValue(e.target.value)}></input>
            </Typography>

            <Typography>Tri :
                <select value={priceOrderValue} onChange={e => setPriceOrderValue(e.target.value)}>
                    <option value={''}>...</option>
                    <option value={'asc'}>Prix croissant</option>
                    <option value={'desc'}>Prix décroissant</option>
                </select>
            </Typography>

            <Typography>Couleur :
                <select value={colorValue} onChange={e => setColorValue(e.target.value)}>
                    <option value={''}>...</option>
                    <option value={'Rosé'}>Rosé</option>
                    <option value={'Rouge'}>Rouge</option>
                    <option value={'Blanc'}>Blanc</option>
                </select>
            </Typography>

            <Typography>Famille :
                <select value={familyValue} onChange={e => setFamilyValue(e.target.value)}>
                    <option value={''}>...</option>
                    <option value={'Pétillant'}>Pétillant</option>
                    <option value={'Plat'}>Plat</option>
                </select>
            </Typography>


            {products.length === 0 && <>
                Chargement des produits ...
            </>}
            {productsFiltered.length > 0 && productsFiltered.map((product, key) => {
                return (
                    <ProductComponent key={key} product={product}/>
                )
            })}
        </>
    );
}

export default ProductTableComponent;