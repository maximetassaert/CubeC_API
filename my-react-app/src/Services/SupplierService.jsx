import axios, {HttpStatusCode} from "axios";

export default class ProductsService {

     static async findAll(token){
        try{
            const supplierResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/suppliers', this.configHeaders(token));
            return supplierResult.data;
        }catch(error){
            if(error.response?.status === HttpStatusCode.Forbidden){
                console.error("ah...")
            }else{
                console.error('Erreur de récupération des fournisseurs', error)

            }
        }
    }

    static async putProduct(supplier, token){
            try{
                const supplierResult = await axios.put(import.meta.env.VITE_API_BASE_URL + '/supplier/', supplier, this.configHeaders(token));
                return supplierResult.data;
            }catch(error){
                if(error.response?.status === HttpStatusCode.Forbidden){
                    console.error("ah...")
                }else{
                    console.error('Erreur de mise à jour du fournisseur', error)

                }
            }
     }
    static configHeaders(bearerToken)  {
        return {
            headers: {
                'Authorization': 'Bearer ' + bearerToken
            }
        }
    }

}