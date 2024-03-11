import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useCallback, useEffect, useState} from "react";

import {AgGridReact} from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import CustomerService from "../Services/CustomerService.jsx"; // Theme

const BackOfficeCustomersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([
        {nom: "...", prenom: "...", téléphone: ""}
    ]);

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        {
            field: "nom", editable: true,
        },
        {
            field: "prenom", editable: true
        },
        {
            field: "téléphone", editable: true
        }
    ]);

    useEffect(() => {
        async function fetchCustomers() {
            let customers = await CustomerService.findAll(cookie.bearerToken);

            customers = customers.map(customer => {
                return {
                    id: customer.id,
                    nom: customer.lastName,
                    prenom: customer.firstName,
                    "téléphone": customer.mobileNumber
                }
            })
            setRowData(customers)
        }

        fetchCustomers();
    }, [])

    const onCellValueChanged = useCallback(event => {
            const customerDto = event.data;
            const customer = {
                id: customerDto.id,
                firstName: customerDto.prenom,
                lastName: customerDto.nom,
                mobileNumber: customerDto.téléphone
            }

            CustomerService.putCustomer(customer, cookie.bearerToken);
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

export default BackOfficeCustomersPage;