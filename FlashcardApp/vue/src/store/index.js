import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));
if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    mySessions: [] ,
    myDecks : [],
    publicDecks : [],
    categories: [],
  },
  mutations: {
    SET_MY_SESSIONS(state, mySessions){
      state.mySessions = mySessions;
    },
    SET_MY_DECKS(state, myDecks){
      state.myDecks = myDecks;
    },
    SET_INITIAL_PUBLIC_DECKS(state, publicDecks){
      state.publicDecks = publicDecks;
    },
    SET_PUBLIC_DECKS(state, publicDecks){
      let localUserId = JSON.parse(localStorage.getItem('user')).userId;
      state.publicDecks = publicDecks.filter((deck) => deck.userID != localUserId);
      // state.publicDecks = publicDecks
    },
    SET_CATEGORIES(state, categories) {
      state.categories = categories;
    },
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    }
  }
})
