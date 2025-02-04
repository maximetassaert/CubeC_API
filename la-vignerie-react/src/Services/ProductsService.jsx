import axios, {HttpStatusCode} from "axios";

export default class ProductsService {

    static async findAll() {
        try {

            const productsResult = await axios.get(import.meta.env.VITE_API_BASE_URL + '/product');

            return productsResult.data;
        } catch (error) {
            console.error('Erreur de récupération des produits', error)
        }
    }

    static async putProduct(product, token) {
        try {
            const productResult = await axios.put(import.meta.env.VITE_API_BASE_URL + '/product/' + product.id, product, this.configHeaders(token));
            return productResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de mise à jour du produit', error)

            }
        }
    }

    static async delete(id, token) {
        try {
            const productResult = await axios.delete(import.meta.env.VITE_API_BASE_URL + '/product/' + id, this.configHeaders(token));
            return productResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de suppréssion du produit', error)

            }
        }
    }

    static async post(product, token) {
        try {
            const productResult = await axios.post(import.meta.env.VITE_API_BASE_URL + '/product/', product, this.configHeaders(token));
            return productResult.data;
        } catch (error) {
            if (error.response?.status === HttpStatusCode.Forbidden) {
                console.error("ah...")
            } else {
                console.error('Erreur de création du produit', error)

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