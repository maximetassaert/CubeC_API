import './App.css'
import LoginFormComponent from "./Components/LoginFormComponent.jsx";
import {Route, Routes} from "react-router-dom";
import MainPage from "./pages/MainPage.jsx";
import MyCartPage from "./pages/MyCartPage.jsx";
import BackOfficeMainPage from "./pages/BackOfficeMainPage.jsx";
import BackOfficeProductsPage from "./pages/BackOfficeProductsPage.jsx";

function App() {

  return (
    <>
        <Routes>
            <Route path='/' element={<MainPage/>} />
            <Route path='login' element={<LoginFormComponent/>} />
            <Route path='myCart' element={<MyCartPage/>} />
            <Route path='BackOffice' element={<BackOfficeMainPage/>}/>
            <Route path='/backoffice/products' element={<BackOfficeProductsPage/>}/>

        </Routes>
    </>
  )
}

export default App
