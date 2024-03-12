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

    static async delete(id, token) {
        try {
            const customerResult = await axios.delete(import.meta.env.VITE_API_BASE_URL + '/customer/' + id, this.configHeaders(token));
            return customerResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de suppréssion du produit', error)

            }
        }
    }

    static async post(customer, token) {
        try {
            const customerResult = await axios.post(import.meta.env.VITE_API_BASE_URL + '/customer/', customer, this.configHeaders(token));
            return customerResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de création du client', error)

            }
        }
    }
    
static async createCustomerAndUser(customer, user, token) {
    try {
        // Modifier l'objet user avant de l'envoyer
        user.roles = [{ id: 0, name: "client" }];

        const userResult = await axios.post(import.meta.env.VITE_API_BASE_URL + '/user/', user, this.configHeaders(token));
        customer.userId = userResult.data.id; // Ajoutez l'ID de l'utilisateur au client
        const customerResult = await axios.post(import.meta.env.VITE_API_BASE_URL + '/customer/', customer, this.configHeaders(token));
        return { customer: customerResult.data, user: userResult.data };
    } catch (error) {
        if (error.response?.status === HttpStatusCode.Forbidden) {S
            console.error("ah...")
        } else {
            console.error('Erreur de création du client ou de l\'utilisateur', error)
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