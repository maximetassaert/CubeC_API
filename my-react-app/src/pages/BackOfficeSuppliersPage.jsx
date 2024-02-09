import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useCallback, useEffect, useState} from "react";

import {AgGridReact} from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import SupplierService from "../Services/SupplierService.jsx"; // Theme

const BackOfficeSuppliersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([
        {nom: "...", "numéro tva": "...", téléphone: 0}
    ]);

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        {
            field: "nom", editable: true,
        },
        {
            field: "numéro tva", editable: true
        },
        {
            field: "téléphone", editable: true
        }
    ]);

    useEffect(() => {
        async function fetchSuppliers() {
            let suppliers = await SupplierService.findAll(cookie.bearerToken);

            suppliers = suppliers.map(supplier => {
                return {
                    id: supplier.id,
                    nom: supplier.CompanyName,
                    "téléphone": supplier.MobileNumber,
                    "numéro_tva": supplier.VatNumber
                }
            })
            setRowData(suppliers)
        }

        fetchSuppliers();
    }, [])

    const onCellValueChanged = useCallback(event => {
            const supplierDto = event.data;
            const supplier = {
                id: supplierDto.id,
                companyName: supplierDto.nom,
                mobileNumber: supplierDto.téléphone,
                vatNumber: supplierDto.numéro_tva
            }

            SupplierService.putSupplier(supplier, cookie.bearerToken);
        }, []
    )

    return (
        <>
            <HeaderComponent/>

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

export default BackOfficeSuppliersPage;