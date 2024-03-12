import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useCallback, useEffect, useState} from "react";
import {Button, Dialog, DialogActions, DialogContent, Typography} from "@mui/material";
import {AgGridReact} from 'ag-grid-react'; // React Grid Logic
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import ProductsService from "../Services/ProductsService.jsx";
import {useCookies} from "react-cookie";
import SupplierService from "../Services/SupplierService.jsx"; // Theme


const BackOfficeProductsPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);
    const [suppliers, setSuppliers] = useState([]);

    const [rowData, setRowData] = useState([]);

    // Column Definitions: Defines & controls grid columns.
    const [colDefs, setColDefs] = useState([]);

    useEffect(() => {
        async function fetchProduct() {
            let products = await ProductsService.findAll();
            products = products.map(product => {
                product.supplier = product.supplier.companyName;
                return product;
            })
            setRowData(products)

            let suppliers = await SupplierService.findAll(cookie.bearerToken);
            setSuppliers(suppliers)
            setColDefs([{
                field: "name", headerName: "Nom", editable: true,
            },
                {
                    field: "description", headerName: "Description", editable: true
                },
                {
                    field: "price", headerName: "Prix", editable: true, cellDataType: 'number'
                },
                {
                    field: "stockValue", headerName: "Stock", cellDataType: 'number'
                }, {
                    field: "image", headerName: "Image", editable: true
                }, {
                    field: 'color',
                    editable: true,
                    headerName: "Couleur",
                    cellEditor: 'agSelectCellEditor',
                    cellEditorParams: {
                        values: [
                            'Rosé',
                            'Rouge',
                            'Blanc'],
                    },
                },
                {
                    field: 'family',
                    editable: true,
                    headerName: "Famille",
                    cellEditor: 'agSelectCellEditor',
                    cellEditorParams: {
                        values: [
                            'Pétillant',
                            'Plat'],
                    },
                },
                {
                    field: 'supplier',
                    editable: true,
                    headerName: "Fournisseur",
                    cellEditor: 'agSelectCellEditor',
                    cellEditorParams: {
                        values: suppliers.map(supplier => supplier.companyName),
                    },
                },])
        }

        fetchProduct();
    }, [])

    const onCellValueChanged = useCallback(event => {
            const dto = event.data;
            const entity = {
                ...dto,
                supplierId: suppliers.find(supplier => supplier.companyName === dto.supplier)?.id,
            }
            ProductsService.putProduct(entity, cookie.bearerToken);
        }, []
    )

    const handleDelete = async (id) => {
        try {
            await ProductsService.delete(id, cookie.bearerToken)
            const newRowData = rowData.filter(product => product.id !== id)
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
        const [newProductValue, setNewProductValue] = useState({});
        const onClose = () => setOpen(false);
        const onSave = async () => {
            if (!newProductValue.supplierId || !newProductValue.name || !newProductValue.description || !newProductValue.price || !newProductValue.stockValue || !newProductValue.image || !newProductValue.color || !newProductValue.family) return;
            newProductValue.supplierId = +newProductValue.supplierId;

            const newProduct = await ProductsService.post(newProductValue, cookie.bearerToken);
            newProduct.supplier = newProduct.supplier.companyName;
            setRowData([...rowData, newProduct])
            onClose();
            setNewProductValue('');
        }

        return (
            <>
                <Button onClick={() => setOpen(true)}>Crée un nouveau produit</Button>
                <Dialog open={open} onClose={() => setOpen(false)}>
                    <DialogContent>
                        <Typography>Nom :
                            <input value={newProductValue.name}
                                   type={'text'}
                                   onChange={
                                       (e) => setNewProductValue(() => {
                                           newProductValue.name = e.target.value;
                                           return newProductValue
                                       })}/>
                        </Typography>
                        <Typography>Description :
                            <input value={newProductValue.description}
                                   type={'text'}
                                   onChange={
                                       (e) => setNewProductValue(() => {
                                           newProductValue.description = e.target.value;
                                           return newProductValue
                                       })}/>
                        </Typography>
                        <Typography>Prix :
                            <input value={newProductValue.price}
                                   type={'number'}
                                   onChange={
                                       (e) => setNewProductValue(() => {
                                           newProductValue.price = e.target.value;
                                           return newProductValue
                                       })}/>
                        </Typography>
                        <Typography>Stock :
                            <input value={newProductValue.stockValue}
                                   type={'number'}
                                   onChange={
                                       (e) => setNewProductValue(() => {
                                           newProductValue.stockValue = e.target.value;
                                           return newProductValue
                                       })}/>
                        </Typography>
                        <Typography>Image :
                            <input value={newProductValue.image}
                                   type={'text'}
                                   onChange={
                                       (e) => setNewProductValue(() => {
                                           newProductValue.image = e.target.value;
                                           return newProductValue
                                       })}/>
                        </Typography>
                        <Typography>Couleur :
                            <select value={newProductValue.color}
                                    onChange={
                                        (e) => setNewProductValue(() => {
                                            newProductValue.color = e.target.value;
                                            return newProductValue
                                        })}
                            >
                                <option value={''}>...</option>
                                <option value={'Rosé'}>Rosé</option>
                                <option value={'Rouge'}>Rouge</option>
                                <option value={'Blanc'}>Blanc</option>
                            </select>
                        </Typography>
                        <Typography>Famille :
                            <select value={newProductValue.family}
                                    onChange={
                                        (e) => setNewProductValue(() => {
                                            newProductValue.family = e.target.value;
                                            return newProductValue
                                        })}
                            >
                                <option value={''}>...</option>
                                <option value={'Pétillant'}>Pétillant</option>
                                <option value={'Plat'}>Plat</option>
                            </select>
                        </Typography>
                        <Typography>Fournisseur :
                            <select value={newProductValue.supplierId}
                                    onChange={
                                        (e) => setNewProductValue(() => {
                                            newProductValue.supplierId = e.target.value;
                                            return newProductValue
                                        })}
                            >
                                <option value={''}>...</option>
                                {suppliers && suppliers.map((supplier, key) => {
                                    return <option key={key} value={supplier.id}>{supplier.companyName}</option>
                                })}
                            </select>
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

export default BackOfficeProductsPage;