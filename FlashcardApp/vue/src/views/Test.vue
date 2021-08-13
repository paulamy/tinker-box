<template>
  <div class="columns is-centered">
    <div class="column is-10">
      <div class="columns is-flex is-centered">
        <div class="column"></div>
        <div id="search-bar" class="column pt-6">
          <div class="control select is-flex">
            <select v-model="selectedCategory" style="width: 420px">
                <option value="">Filter By Category</option>
                <option v-for="category in categories" v-bind:key="category.categoryID">{{category.categoryName}}</option>
              </select>
          </div>
        </div>
        <div class="column"></div>
      </div>
      <div class="tile is-ancestor pt-3">
        <div id="myDecks" class="tile is-parent is-vertical">
          <div class="tile is-child box has-background-info-light">
            <h1 class="title is-3 has-text-grey-dark">My Decks</h1>
            <div v-for="deck in myDecks" v-bind:key="deck.deckID">
              <deck-component
                class="mt-5 mb-5"
                v-bind:deckName="deck.name"
                v-bind:deckId="deck.deckID"
                flashcardType="testFlashcard"
              ></deck-component>
            </div>
          </div>
        </div>
        <div id="trendingDecks" class="tile is-parent is-vertical">
          <div class="tile is-child box has-background-info-light">
            <h1 class="title is-3 has-text-grey-dark">Trending Decks</h1>
            <div v-for="deck in publicDecks" v-bind:key="deck.deckID">
              <deck-component
                class="mt-5 mb-5"
                v-bind:deckId="deck.deckID"
                v-bind:deckName="deck.name"
                flashcardType="testFlashcard"
              ></deck-component>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
 
</template>

<script>
import User from "../store/index"
import DeckComponent from "../components/DeckComponent.vue";
import DeckService from "../services/DeckService";
import CategoryService from "@/services/CategoryService";

export default {
  components: { DeckComponent },
  name: "test",
  data() {
    return {
      userID: User.state.user.userId,
      selectedCategory: ""
    }
  },
  computed: {
    myDecks: {
     get() {
        if(!this.selectedCategory) return this.$store.state.myDecks
        else return this.$store.state.myDecks.filter(deck => {
          return deck.categoryName == this.selectedCategory;
        })
      }
    },
    publicDecks: {
      get() {
        if(!this.selectedCategory) return this.$store.state.publicDecks
        else return this.$store.state.publicDecks.filter(deck => {
          return deck.categoryName == this.selectedCategory;
        })
      }
    },
    categories: {
      get() {
        return this.$store.state.categories
      }
    }
  },
  created() {
    // let userId = JSON.parse(localStorage.getItem("user")).userId;
    // let userId = this.$store.state.user
    DeckService.getDecksByUserId(this.userID).then((response) => {
      // console.log(response.data);
      this.$store.commit("SET_MY_DECKS", response.data);
    });
    DeckService.getDecks().then((response) => {
      this.$store.commit("SET_PUBLIC_DECKS", response.data);
    });
    CategoryService.getCategories().then((response) => {
      this.$store.commit("SET_CATEGORIES", response.data);
    });
  },
  
};
</script>
