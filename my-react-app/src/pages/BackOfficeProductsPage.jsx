import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useCallback, useEffect, useState} from "react";
import { Button } from "@mui/material";
import {AgGridReact} from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import ProductsService from "../Services/ProductsService.jsx";
import {useCookies} from "react-cookie"; // Theme


const BackOfficeProductsPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([
        {nom: "...", Description: "...", Prix: 0, Stock: 0}
    ]);
    const [isModalOpen, setIsModalOpen] = useState(false); // Etat pour contrôler l'ouverture de la modal


    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        {
            field: "nom", editable: true,
        },
        {
            field: "description", editable: true
        },
        {
            field: "prix", editable: true
        },
        {
            field: "stock"
        }, {
            field: "image", editable: true
        },         {
            field: 'couleur',
            editable: true,
            cellEditor: 'agSelectCellEditor',
            cellEditorParams: {
              values: [
                'Rosé',
                'Rouge',
                'Blanc'],
            },
          },
          {
            field: 'famille',
            editable: true,
            cellEditor: 'agSelectCellEditor',
            cellEditorParams: {
              values: [
                'Pétillant',
                'Plat'],
            },
          },

    ]);

    useEffect(() => {
        async function fetchProduct() {
            let products = await ProductsService.findAll();

            products = products.map(product => {
                return {
                    id: product.id,
                    nom: product.name,
                    description: product.description,
                    prix: product.price,
                    image: product.image,
                    stock: product.stockValue,
                    couleur: product.color,
                    famille: product.family
                }
            })
            setRowData(products)
        }

        fetchProduct();
    }, [])

    const onCellValueChanged = useCallback(event => {
            const productDto = event.data;
            console.log(productDto);
            const product = {
                id: productDto.id,
                name: productDto.nom,
                description: productDto.description,
                price: productDto.prix,
                image: productDto.image,
                stockValue: productDto.stock,
                color: productDto.couleur,
                family: productDto.famille
            }
            ProductsService.putProduct(product, cookie.bearerToken);
        }, []
    )

    return (
        <>
            <HeaderComponent/>

            <Button variant="contained" color="secondary">
                Ouvrir la modal
            </Button>
            <main>
                <div className="flex flex-wrap">
                    <div className="ag-theme-quartz" style={{height: 500}}>
                        {/* The AG Grid component */}
                        <AgGridReact rowData={rowData} columnDefs={colDefs} onCellValueChanged={onCellValueChanged}/>
                    </div>
                </div>
                    
            </main>
            <Footer/>
        </>

    );
}

export default BackOfficeProductsPage;