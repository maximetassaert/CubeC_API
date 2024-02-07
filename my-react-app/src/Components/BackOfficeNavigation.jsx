import React from "react";
import {Link} from "react-router-dom";


const BackOfficeNavigation = () => {


    return (
        <React.Fragment >
            <ul>
                <li><Link to={'/backoffice/products'}>Gestion des produits</Link></li>
                <li>Inventaire</li>
                <li>Gestion des produits</li>
                <li>Commandes fournisseurs</li>
                <li>Clients</li>
                <li>Commandes clients</li>
            </ul>
        </React.Fragment>
    );
}

export default BackOfficeNavigation;