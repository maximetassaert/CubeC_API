import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import {useEffect, useState} from "react";
import "ag-grid-community/styles/ag-grid.css"; // Core CSS
import "ag-grid-community/styles/ag-theme-quartz.css";
import {useCookies} from "react-cookie";
import SupplierOrderService from "../Services/SupplierOrderService.jsx";
import {Box, Typography} from "@mui/material";
import SupplierService from "../Services/SupplierService.jsx";
import moment from "moment";
import {LoadingButton} from "@mui/lab";
import {useNavigate} from "react-router-dom";

const BackOfficeSuppliersOrdersPage = () => {
    const [cookie, setCookie] = useCookies(['bearerToken']);
    const navigate = useNavigate();
    const [suppliersOrders, setSuppliersOrders] = useState([]);
    const [isLoading, setIsLoading] = useState(false)
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

    return (
        <>
            <HeaderComponent/>

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