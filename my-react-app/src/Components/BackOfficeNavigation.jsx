import React from "react";
import {Link} from "react-router-dom";


const BackOfficeNavigation = () => {


    return (
        <React.Fragment>
            <ul>
                <li><Link to={'/backoffice/products'}>Gestion des produits</Link></li>
                <li>Inventaire</li>
                <li><Link to={'/backoffice/suppliers'}>Gestion des fournisseurs</Link></li>
                <li><Link to={'/backoffice/suppliersOrders'}>Commandes fournisseurs</Link></li>
                <li><Link to={'/backoffice/customers'}>Gestion des clients</Link></li>
                <li>Commandes clients</li>
            </ul>
        </React.Fragment>
    );
}

export default BackOfficeNavigation;