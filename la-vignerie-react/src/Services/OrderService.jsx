import axios, {HttpStatusCode} from "axios";

export default class OrderService {

    static configHeaders(bearerToken) {
        return {
            headers: {
                'Authorization': 'Bearer ' + bearerToken
            }
        }
    }

    static async createOrder(orderDto, token) {
        try {
            const result = await axios.post(import.meta.env.VITE_API_BASE_URL + '/order/', orderDto, this.configHeaders(token));
            return result.data;
        } catch (error) {
            console.error(error)
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de création de la commande', error)
                throw error;
            }
        }
    }


}