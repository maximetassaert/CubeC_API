
import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import BackOfficeNavigation from "../Components/BackOfficeNavigation.jsx";
import {useCallback, useEffect, useState} from "react";

import { AgGridReact } from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import SupplierService from "../Services/SupplierService.jsx"; // Theme

const BackOfficeCustomersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([
        { nom: "...", prenom: "...", téléphone: 0}
    ]);

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        { field: "nom",     editable: true,
        },
        { field: "prenom" ,        editable: true
        },
        { field: "téléphone",        editable: true
        }
    ]);

    useEffect(() => {
        async function fetchCustomers() {
            let customers = await SupplierService.findAll(cookie.bearerToken);

            customers = customers.map(supplier => {
                return {id: supplier.id, nom: supplier.LastName, prenom: supplier.FirstName, "téléphone": supplier.MobileNumber}
            })
            setRowData(customers)
        }
        fetchCustomers();
    }, [])

    const onCellValueChanged = useCallback(event => {
        const customerDto = event.data;
        const customer = {id: customerDto.id, firstName: customerDto.prenom, lastName: customerDto.nom, mobileNumber: customerDto.téléphone}

        SupplierService.putCustomer(customer, cookie.bearerToken);
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

export default BackOfficeCustomersPage;