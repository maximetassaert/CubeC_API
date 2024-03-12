import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useCallback, useEffect, useState} from "react";

import {AgGridReact} from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import CustomerService from "../Services/CustomerService.jsx"; // Theme
import {Button, Dialog, DialogActions, DialogContent, Typography} from "@mui/material";

const BackOfficeCustomersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([
        {nom: "...", prenom: "...", téléphone: ""}
    ]);

    const handleDelete = async (id) => {
        try {
            await CustomerService.delete(id, cookie.bearerToken)
            const newRowData = rowData.filter(customer => customer.id !== id)
            setRowData(newRowData)
        } catch (error) {
            alert(error.response.data)
        }
    }
    const ButtonDeleteRenderer = (props) => {
        const {id} = props.data;

        return (
            <>
                <Button onClick={() => handleDelete(id)}>
                    <img
                        width="20"
                        height="20"
                        src="https://img.icons8.com/emoji/48/cross-mark-emoji.png"
                        alt="cross-mark-emoji"/>
                </Button>
            </>
        );
    }

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        {
            headerName: "nom", field: "lastName", editable: true,
        },
        {
            headerName: "prenom", field: 'firstName', editable: true
        },
        {
            headerName: "téléphone", field: 'mobileNumber', editable: true
        }
    ]);

    useEffect(() => {
        async function fetchCustomers() {
            let customers = await CustomerService.findAll(cookie.bearerToken);

            setRowData(customers)
        }

        fetchCustomers();
    }, [])

    const onCellValueChanged = useCallback(event => {
            const customerDto = event.data;


            CustomerService.putCustomer(customerDto, cookie.bearerToken);
        }, []
    )

    const NewEntityModal = () => {
        const [open, setOpen] = useState(false);
        const [newCustomerValue, setNewCustomerValue] = useState({
            lastName: "",
            firstName: "",
            birthdate: "",
            mobileNumber: "",
            mail: "",
            password: ""
        });
    
        const onClose = () => setOpen(false);
    
        const onSave = async () => {
            // Vérifiez ici si tous les champs nécessaires sont remplis
            // et effectuez les actions d'enregistrement nécessaires
            if (!newCustomerValue.lastName || !newCustomerValue.firstName || !newCustomerValue.birthdate || !newCustomerValue.mobileNumber || !newCustomerValue.mail || !newCustomerValue.password) {
                return;
            }
    
            // Effectuez votre logique d'enregistrement ici
            try {
                const userData = {
                    mail: newCustomerValue.mail,
                    password: newCustomerValue.password,
                };
    
                const { customer, user } = await CustomerService.createCustomerAndUser(newCustomerValue, userData, cookie.bearerToken);
                setRowData([...rowData, customer]);
                onClose();
                setNewCustomerValue({
                    lastName: "",
                    firstName: "",
                    birthdate: "",
                    mobileNumber: "",
                    mail: "",
                    password: ""
                });
            } catch (error) {
                console.error("Error saving customer:", error);
                // Gérer les erreurs d'enregistrement ici
            }
        };
    
        const handleChange = (e, field) => {
            const value = e.target.value;
            setNewCustomerValue(prevState => ({
                ...prevState,
                [field]: value
            }));
        };
    
        return (
            <>
                <Button onClick={() => setOpen(true)}>Créer un nouveau client</Button>
                <Dialog open={open} onClose={onClose}>
                    <DialogContent>
                        <Typography>Nom :
                            <input value={newCustomerValue.lastName} type="text" onChange={(e) => handleChange(e, 'lastName')} />
                        </Typography>
                        <Typography>Prénom :
                            <input value={newCustomerValue.firstName} type="text" onChange={(e) => handleChange(e, 'firstName')} />
                        </Typography>
                        <Typography>Date de naissance :
                            <input value={newCustomerValue.birthdate} type="date" onChange={(e) => handleChange(e, 'birthdate')} />
                        </Typography>
                        <Typography>Numero de téléphone :
                            <input value={newCustomerValue.mobileNumber} type="number" onChange={(e) => handleChange(e, 'mobileNumber')} />
                        </Typography>
                        <Typography>Email :
                            <input value={newCustomerValue.mail} type="email" onChange={(e) => handleChange(e, 'mail')} />
                        </Typography>
                        <Typography>Mot de passe :
                            <input value={newCustomerValue.password} type="password" onChange={(e) => handleChange(e, 'password')} />
                        </Typography>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={onClose} color="primary">
                            Quitter
                        </Button>
                        <Button onClick={onSave} color="primary">
                            Enregistrer
                        </Button>
                    </DialogActions>
                </Dialog>
            </>
        );
    };
    


    return (
        <>
            <HeaderComponent/>
            <NewEntityModal/>
            <main>
                <div className="flex flex-wrap">
                    <div className="ag-theme-quartz" style={{height: 500}}>
                        {/* The AG Grid component */}
                        <AgGridReact rowData={rowData} columnDefs={[...colDefs,
                                {
                                    headerName: "Action",
                                    field: "action",
                                    width: 100,
                                    cellRenderer: ButtonDeleteRenderer
                                }]} onCellValueChanged={onCellValueChanged}/>
                    </div>
                </div>
            </main>
            <Footer/>
        </>

    );
}

export default BackOfficeCustomersPage;