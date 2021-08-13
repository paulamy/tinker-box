import axios from 'axios'

export default {
    
    getCategories(){
        return axios.get('/categories')
    },
    // /categories/id/:categoryID
    getCategoryById(categoryId){
        return axios.get(`/categories/id/${categoryId}`)
    },
    // /categories/name/:name
    getCategoryByName(categoryName){
        return axios.get(`/categories/name/${categoryName}`)
    }
}