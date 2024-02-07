import axios, {HttpStatusCode} from "axios";

export default class CartsService {

    static configHeaders(bearerToken)  {
        return {
            headers: {
                'Authorization': 'Bearer ' + bearerToken
            }
        }
    }
    static async loadCart(cartId, token){

        try{
            const cartResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/cart/' + cartId, this.configHeaders(token));
            return cartResult.data;
        }catch(error){
            if(error.response.status === HttpStatusCode.Forbidden){
                console.error("Vous n'avez pas le droit de récupérer ce panier...")
            }else{
                console.error('Erreur de récupération du panier', error)

            }
        }
    }

    static async updateCart(cart, token){
        try{
            const cartResult = await axios.put(import.meta.env.VITE_API_BASE_URL + '/cart/', cart, this.configHeaders(token));
            return cartResult.data;
        }catch(error){
            if(error.response.status === HttpStatusCode.Forbidden){
                console.error("ah...")
            }else{
                console.error('Erreur de récupération du panier', error)

            }
        }
    }

    static async createCart(cart, token){
        try{
            const cartResult = await axios.post(import.meta.env.VITE_API_BASE_URL + '/cart/', cart, this.configHeaders(token));
            return cartResult.data;
        }catch(error){
            console.error(error)
            if(error.response.status === HttpStatusCode.Forbidden){
                console.error("ah...")
            }else{
                console.error('Erreur de récupération du panier', error)
            }
        }
    }



}