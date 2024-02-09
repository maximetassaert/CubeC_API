import axios, {HttpStatusCode} from "axios";

export default class SupplierOrderService {

    static configHeaders(bearerToken) {
        return {
            headers: {
                'Authorization': 'Bearer ' + bearerToken
            }
        }
    }

    static async findAll(token) {

        try {
            const result = await axios.get(import.meta.env.VITE_API_BASE_URL + '/supplierOrder', this.configHeaders(token));
            return result.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("Ah...")
            } else {
                console.error('Erreur de récupération des commandes fournisseurs', error)

            }
        }
    }

    static async updateSupplierOrder(supplierOrder, token) {
        try {
            const result = await axios.put(import.meta.env.VITE_API_BASE_URL + '/supplierOrder/' + supplierOrder.id, supplierOrder, this.configHeaders(token));
            return result.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de mise à jour de la commande fournisseur', error)

            }
        }
    }

    static async createSupplierOrder(supplierOrder, token) {
        try {
            const result = await axios.post(import.meta.env.VITE_API_BASE_URL + '/supplierOrder/', supplierOrder, this.configHeaders(token));
            return result.data;
        } catch (error) {
            console.error(error)
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de création de la commande fournisseur', error)
            }
        }
    }


}