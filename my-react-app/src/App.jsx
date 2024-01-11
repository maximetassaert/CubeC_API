import './App.css'
import LoginFormComponent from "./Components/LoginFormComponent.jsx";
import {Route, Routes} from "react-router-dom";
import MainPage from "./pages/MainPage.jsx";
import MyCartPage from "./pages/MyCartPage.jsx";

function App() {

  return (
    <>
        <Routes>
            <Route path='/' element={<MainPage/>} />
            <Route path='login' element={<LoginFormComponent/>} />
            <Route path='myCart' element={<MyCartPage/>} />
        </Routes>
    </>
  )
}

export default App
