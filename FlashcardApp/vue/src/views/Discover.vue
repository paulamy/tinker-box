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
      <div id="trendingDecks" class="tile is-parent is-vertical">
          <div class="tile is-child box has-background-info-light">
            <h1 class="title is-3 has-text-grey-dark">Trending Decks</h1>
            <div v-for="deck in publicDecks" v-bind:key="deck.deckID">
              <deck-component
                class="mt-5 mb-5"
                v-bind:deckId="deck.deckID"
                v-bind:deckName="deck.name"
                flashcardType="studyFlashcard"
              ></deck-component>
            </div>
          </div>
        </div>
      
      
    </div>
  </div>
</template>

<script>
import User from "@/store/index"
import DeckComponent from "@/components/DeckComponent.vue";
import DeckService from "@/services/DeckService"
import CategoryService from "@/services/CategoryService";

export default {
  name: "discover",
  data() {
    return {
      selectedCategory: ""
    }
  },
  components: {DeckComponent},
  computed: {
    publicDecks: {
      get() {
        if(!this.selectedCategory) return this.$store.state.publicDecks
        else return this.$store.state.publicDecks.filter(deck => {
          return deck.categoryName == this.selectedCategory;
        })
      }
    },
    UserId : {
        get() {
          return User.state.user.userId
        }
      },
      categories: {
      get() {
        return this.$store.state.categories
      }
    }
  },
  created() {
    if(this.userId)
    {
    DeckService.getDecks().then((response) => {
      this.$store.commit("SET_PUBLIC_DECKS", response.data);
    });
    }
    else if (!this.userId)
    {
    DeckService.getDecks().then((response) => {
      this.$store.commit("SET_INITIAL_PUBLIC_DECKS", response.data);
    });
    CategoryService.getCategories().then((response) => {
      this.$store.commit("SET_CATEGORIES", response.data);
    });
    }
}
};
</script>
