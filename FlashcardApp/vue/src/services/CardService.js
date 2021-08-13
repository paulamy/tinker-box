import axios from 'axios';

export default {

  createCard(userID, deckID, payload) {
    return axios.post(`/cards/user/${userID}/deck/${deckID}`, payload)
  },

}
