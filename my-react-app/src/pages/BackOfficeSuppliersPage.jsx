
import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import BackOfficeNavigation from "../Components/BackOfficeNavigation.jsx";
import {useCallback, useEffect, useState} from "react";

import { AgGridReact } from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie"; // Theme

const BackOfficeSuppliersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([
        { nom: "...", "numéro tva": "...", téléphone: 0}
    ]);

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        { field: "nom",     editable: true,
        },
        { field: "numéro tva" ,        editable: true
        },
        { field: "téléphone",        editable: true
        }
    ]);

    useEffect(() => {
        async function fetchProduct() {
            let suppliers = await SupplierService.findAll();

            suppliers = suppliers.map(supplier => {
                return {id: supplier.id, nom: supplier.CompanyName, "téléphone": supplier.MobileNumber, "numéro tva": supplier.VatNumber}
            })
            setRowData(suppliers)
        }
        fetchProduct();
    }, [])

    const onCellValueChanged = useCallback(event => {
            SupplierService.putSupplier(event.data, cookie.bearerToken);
    }, []
    )

    return (
        <>
            <HeaderComponent/>
            <BackOfficeNavigation/>

            <main>
                <div className="flex flex-wrap">
                    <div  className="ag-theme-quartz" style={{ height: 500 }}>
                        {/* The AG Grid component */}
                        <AgGridReact rowData={rowData} columnDefs={colDefs} onCellValueChanged={onCellValueChanged}/>
                    </div>
                </div>
            </main>
            <Footer/>
        </>

    );
}

export default BackOfficeSuppliersPage;