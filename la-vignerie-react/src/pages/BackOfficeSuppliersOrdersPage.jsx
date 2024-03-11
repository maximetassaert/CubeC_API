import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useEffect, useState} from "react";
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import SupplierOrderService from "../Services/SupplierOrderService.jsx";
import {Box, Button, Dialog, DialogActions, DialogContent, Typography} from "@mui/material";
import Select from 'react-select';
import SupplierService from "../Services/SupplierService.jsx";
import moment from "moment";
import {LoadingButton} from "@mui/lab";
import {useNavigate} from "react-router-dom";
import ProductsService from "../Services/ProductsService.jsx";

const BackOfficeSuppliersOrdersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);
    const navigate = useNavigate();
    const [suppliersOrders, setSuppliersOrders] = useState([]);
    const [suppliers, setSuppliers] = useState([]);
    const [products, setProducts] = useState([]);
    const [supplierProducts, setSupplierProducts] = useState([]);

    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        async function fetchSuppliersOrders() {
            let suppliersOrders = SupplierOrderService.findAll(cookie.bearerToken);
            const suppliers = await SupplierService.findAll(cookie.bearerToken);
            suppliersOrders = await suppliersOrders;
            suppliersOrders.map((supplierOrder => {
                const supplier = suppliers.find(supplier => supplier.id == supplierOrder.supplierId)
                supplierOrder.createDate = moment(supplierOrder.createDate).format('DD/MM/YYYY')
                return supplierOrder.supplier = supplier;
            }));


            setSuppliersOrders(suppliersOrders)
            setSuppliers(suppliers)

            async function fetchProducts() {
                const products = await ProductsService.findAll();
                setProducts(products)
                setSupplierProducts(products.map(product => {
                    return {value: product.id, label: product.name}
                }))
            }

            fetchProducts();

        }

        fetchSuppliersOrders();
    }, [])

    const deliveredAction = async (supplierOrder) => {
        setIsLoading(true)
        supplierOrder.delivered = true
        await SupplierOrderService.updateSupplierOrder(supplierOrder, cookie.bearerToken);

        setIsLoading(false)
        //navigate(0)
    }


    const handleDelete = async (id) => {
        try {
            await SupplierOrderService.delete(id, cookie.bearerToken)
            const newRowData = suppliersOrders.filter(supplier => supplier.id !== id)
            setSuppliersOrders(newRowData)
        } catch (error) {
            alert(error.response.data)
        }
    }

    const NewEntityModal = () => {
        const [open, setOpen] = useState(false);
        const [newSupplierValue, setNewSupplierValue] = useState({});
        const onClose = () => setOpen(false);
        const onSave = async () => {
            if (!newSupplierValue.supplierId || !newSupplierValue.supplierOrderLinesCreateDto) return;

            const newSupplier = await SupplierOrderService.post(newSupplierValue, cookie.bearerToken);
            const supplier = suppliers.find(supplier => supplier.id == newSupplier.supplierId)
            newSupplier.createDate = moment(newSupplier.createDate).format('DD/MM/YYYY')
            newSupplier.supplier = supplier;
            console.log(newSupplier)
            setSuppliersOrders([...suppliersOrders, newSupplier])
            onClose();
            setNewSupplierValue('');
        }

        return (
            <>
                <Button onClick={() => setOpen(true)}>Crée une nouvelle commande fournisseur</Button>
                <Dialog open={open} onClose={() => setOpen(false)}>
                    <DialogContent className={"modalSupplierOrder"}>
                        <Typography>Fournisseur :
                            <select value={newSupplierValue.supplierId}
                                    onChange={
                                        (e) => setNewSupplierValue(() => {
                                            newSupplierValue.supplierId = e.target.value;
                                            return newSupplierValue
                                        })}
                            >
                                <option value={''}>...</option>
                                {suppliers && suppliers.map((supplier, key) => {
                                    return <option key={key} value={supplier.id}>{supplier.companyName}</option>
                                })}
                            </select>
                        </Typography>
                        {supplierProducts.length > 0 &&
                            <Typography>Produits :
                                <Select className={'multiSelect'} isMulti options={supplierProducts}
                                        closeMenuOnSelect={false}
                                        onChange={
                                            (products) => {
                                                newSupplierValue.supplierOrderLinesCreateDto = products.map(product => {
                                                    return {productId: product.value, quantity: 1}
                                                })
                                                setNewSupplierValue(newSupplierValue)
                                            }}
                                />
                            </Typography>
                        }


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
                    {suppliersOrders.length > 0 &&
                        suppliersOrders.map((supplierOrder, key) => {
                            return <Box key={key} style={{border: '1px solid black'}}>
                                <Typography><strong>{supplierOrder.supplier.companyName}</strong></Typography>
                                <Typography>{supplierOrder.createDate}</Typography>
                                {supplierOrder.supplierOrderLines.map((supplierOrderLine, keyS) => {
                                    return <Box
                                        key={keyS}>{supplierOrderLine.quantity + " * " + supplierOrderLine.product.name}</Box>
                                })

                                }
                                {supplierOrder.delivered && <>Livrée</>}
                                {!supplierOrder.delivered &&
                                    <LoadingButton loading={isLoading} onClick={() => deliveredAction(supplierOrder)}>
                                        Non Livrée
                                    </LoadingButton>}
                                <Box>
                                    <Button onClick={() => handleDelete(supplierOrder.id)}>
                                        <img
                                            width="20"
                                            height="20"
                                            src="https://img.icons8.com/emoji/48/cross-mark-emoji.png"
                                            alt="cross-mark-emoji"/>
                                    </Button>
                                </Box>
                            </Box>
                        })
                    }
                </div>
            </main>
            <Footer/>
        </>

    );
}
export default BackOfficeSuppliersOrdersPage;