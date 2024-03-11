import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useCallback, useEffect, useState} from "react";

import {AgGridReact} from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import SupplierService from "../Services/SupplierService.jsx";
import {Button, Dialog, DialogActions, DialogContent, Typography} from "@mui/material"; // Theme

const BackOfficeSuppliersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);


    const [rowData, setRowData] = useState([]);

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([
        {
            field: "companyName", headerName: "nom", editable: true,
        },
        {
            field: "vatNumber", headerName: "Numéro de tva", editable: true
        },
        {
            field: "mobileNumber", headerName: "Téléphone", editable: true
        }
    ]);

    useEffect(() => {
        async function fetchSuppliers() {
            let suppliers = await SupplierService.findAll(cookie.bearerToken);
            setRowData(suppliers)
        }

        fetchSuppliers();
    }, [])

    const onCellValueChanged = useCallback(event => {
            const supplier = event.data;
            SupplierService.putSupplier(supplier, cookie.bearerToken);
        }, []
    )

    const handleDelete = async (id) => {
        try {
            await SupplierService.delete(id, cookie.bearerToken)
            const newRowData = rowData.filter(supplier => supplier.id !== id)
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

    const NewEntityModal = () => {
        const [open, setOpen] = useState(false);
        const [newSupplierValue, setNewSupplierValue] = useState({});
        const onClose = () => setOpen(false);
        const onSave = async () => {
            if (!newSupplierValue.companyName || !newSupplierValue.vatNumber || !newSupplierValue.mobileNumber) return;

            const newSupplier = await SupplierService.post(newSupplierValue, cookie.bearerToken);
            setRowData([...rowData, newSupplier])
            onClose();
            setNewSupplierValue('');
        }

        return (
            <>
                <Button onClick={() => setOpen(true)}>Crée un nouveau fournisseur</Button>
                <Dialog open={open} onClose={() => setOpen(false)}>
                    <DialogContent>
                        <Typography>Nom :
                            <input value={newSupplierValue.companyName}
                                   type={'text'}
                                   onChange={
                                       (e) => setNewSupplierValue(() => {
                                           newSupplierValue.companyName = e.target.value;
                                           return newSupplierValue
                                       })}/>
                        </Typography>
                        <Typography>Numéro de TVA :
                            <input value={newSupplierValue.vatNumber}
                                   type={'text'}
                                   onChange={
                                       (e) => setNewSupplierValue(() => {
                                           newSupplierValue.vatNumber = e.target.value;
                                           return newSupplierValue
                                       })}/>
                        </Typography>
                        <Typography>Numéro de téléphone :
                            <input value={newSupplierValue.mobileNumber}
                                   type={'text'}
                                   onChange={
                                       (e) => setNewSupplierValue(() => {
                                           newSupplierValue.mobileNumber = e.target.value;
                                           return newSupplierValue
                                       })}/>
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
    }

    return (
        <>
            <HeaderComponent/>
            <NewEntityModal/>
            <main>
                <div className="flex flex-wrap">
                    <div className="ag-theme-quartz" style={{height: 500}}>
                        <AgGridReact
                            rowData={rowData}
                            columnDefs={[...colDefs,
                                {
                                    headerName: "Action",
                                    field: "action",
                                    width: 100,
                                    cellRenderer: ButtonDeleteRenderer
                                }]}
                            onCellValueChanged={onCellValueChanged}
                        />
                    </div>
                </div>
            </main>
            <Footer/>
        </>

    );
}

export default BackOfficeSuppliersPage;