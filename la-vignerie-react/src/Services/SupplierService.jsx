import axios, {HttpStatusCode} from "axios";

export default class SupplierService {

    static async findAll(token) {
        try {
            const supplierResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/supplier', this.configHeaders(token));
            return supplierResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de récupération des fournisseurs', error)

            }
        }
    }

    static async putSupplier(supplier, token) {
        try {
            const supplierResult = await axios.put(import.meta.env.VITE_API_BASE_URL + '/supplier/' + supplier.id, supplier, this.configHeaders(token));
            return supplierResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de mise à jour du fournisseur', error)

            }
        }
    }

    static async delete(id, token) {
        try {
            const result = await axios.delete(import.meta.env.VITE_API_BASE_URL + '/supplier/' + id, this.configHeaders(token));
            return result.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de suppression du fournisseur', error)

            }
        }
    }

    static async post(entity, token) {
        try {
            const result = await axios.post(import.meta.env.VITE_API_BASE_URL + '/supplier/', entity, this.configHeaders(token));
            return result.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de création du fournisseur', error)
            }
        }
    }

    static configHeaders(bearerToken) {
        return {
            headers: {
                'Authorization': 'Bearer ' + bearerToken
            }
        }
    }

}