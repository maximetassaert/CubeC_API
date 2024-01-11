import axios, {HttpStatusCode} from "axios";

export default class CartsService {

    static async loadCart(cartId, token){
        let config = {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        }

        try{
            const cartResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/carts/' + cartId, config);
            return cartResult.data;
        }catch(error){
            if(error.response.status === HttpStatusCode.Forbidden){
                console.error("Vous n'avez pas le droit de récupérer ce panier...")
            }else{
                console.error('Erreur de récupération du panier', error)

            }
        }
    }

    static async addProductToCart(cartId, token){
        let config = {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        }
        let cartUrl = '';
        if(cartId !== 0){
            cartUrl = '/carts/' + cartId
        }else{
            cartUrl = '/carts/myCart';

        }
        try{
            const cartResult = await axios.put(import.meta.env.VITE_API_BASE_URL + cartUrl, config);
            return cartResult.data;
        }catch(error){
            if(error.response.status === HttpStatusCode.Forbidden){
                console.error("Vous n'avez pas le droit de récupérer ce panier...")
            }else{
                console.error('Erreur de récupération du panier', error)

            }
        }
    }


}