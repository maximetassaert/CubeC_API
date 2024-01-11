import axios from "axios";

export default class ProductsService {

     static async findAll(token){
        let config = {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        }
        try{
            const productsResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/products', config);
            return productsResult.data;
        }catch(error){
            console.error('Erreur de récupération des produits', error)
        }
    }


}