
import HeaderComponent from "../Components/HeaderComponent.jsx";
import Footer from "../Components/Footer.jsx";
import BackOfficeNavigation from "../Components/BackOfficeNavigation.jsx";

const BackOfficeMainPage = () => {

    return (
        <>
            <HeaderComponent/>
            <BackOfficeNavigation/>

            <main>
                <div className="flex flex-wrap">
                </div>
            </main>
            <Footer/>
        </>

    );
}

export default BackOfficeMainPage;