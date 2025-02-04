import './App.css'
import LoginFormComponent from "./Components/LoginFormComponent.jsx";
import {Route, Routes} from "react-router-dom";
import MainPage from "./pages/MainPage.jsx";
import MyCartPage from "./pages/MyCartPage.jsx";
import BackOfficeMainPage from "./pages/BackOfficeMainPage.jsx";
import BackOfficeProductsPage from "./pages/BackOfficeProductsPage.jsx";
import BackOfficeSuppliersPage from "./pages/BackOfficeSuppliersPage.jsx";
import BackOfficeCustomersPage from "./pages/BackOfficeCustomersPage.jsx";
import BackOfficeInventoryPage from "./pages/BackOfficeInventoryPage.jsx";
import SignUpForm from "./Components/SignUpForm.jsx";
import BackOfficeSuppliersOrdersPage from "./pages/BackOfficeSuppliersOrdersPage.jsx";

function App() {

    return (
        <>
            <Routes>
                <Route path='/*' element={<MainPage/>}/>
                <Route path='login' element={<LoginFormComponent/>}/>
                <Route path='Register' element={<SignUpForm/>}/>
                <Route path='myCart' element={<MyCartPage/>}/>
                <Route path='BackOffice' element={<BackOfficeMainPage/>}/>
                <Route path='/backoffice/products' element={<BackOfficeProductsPage/>}/>
                <Route path='/backoffice/inventory' element={<BackOfficeInventoryPage/>}/>
                <Route path='/backoffice/suppliers' element={<BackOfficeSuppliersPage/>}/>
                <Route path='/backoffice/customers' element={<BackOfficeCustomersPage/>}/>
                <Route path='/backoffice/suppliersOrders' element={<BackOfficeSuppliersOrdersPage/>}/>


            </Routes>
        </>
    )
}

export default App
