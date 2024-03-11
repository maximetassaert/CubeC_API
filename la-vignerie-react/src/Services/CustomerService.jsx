import axios, {HttpStatusCode} from "axios";

export default class CustomerService {

    static async findAll(token) {
        try {
            const customerResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/customer', this.configHeaders(token));
            return customerResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de récupération des clients', error)

            }
        }
    }

    static async putCustomer(customer, token) {
        try {
            const customerResult = await axios.put(import.meta.env.VITE_API_BASE_URL + '/customer/' + customer.id, customer, this.configHeaders(token));
            return customerResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de mise à jour du client', error)

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