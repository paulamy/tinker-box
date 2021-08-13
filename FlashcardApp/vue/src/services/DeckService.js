import axios from 'axios'

export default {

    getDecks() {
      return axios.get('/decks/')
    },

    getDecksByUserId(userId){
        return axios.get(`/decks/user/${userId}`)
    },

    createDeck(userId, payload){
        return axios.post(`/decks/user/${userId}`,payload)
    },

    updateDeck(userId, deckId){
      return axios.put(`/decks/user/${userId}/deck/${deckId}`)
    },

    deleteDeck(userId, deckId){
      return axios.delete(`/decks/user/${userId}/deck/${deckId}`)
    }
  
  }
  