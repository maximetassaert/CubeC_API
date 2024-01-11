import './App.css'
import LoginFormComponent from "./LoginFormComponent.jsx";
import {Route, Routes} from "react-router-dom";
import MainPage from "./MainPage.jsx";

function App() {

  return (
    <>
        <Routes>
            <Route path='/' element={<MainPage/>} />
            <Route path='login' element={<LoginFormComponent/>} />
        </Routes>
    </>
  )
}

export default App
