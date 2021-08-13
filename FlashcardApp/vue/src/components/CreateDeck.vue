<template>
  <div class="columns is-centered pt-6 mt-6">
    <div class="tile is-ancestor is-6">
      <div class="tile is-parent">
        <div class="tile is-child box has-background-info-light">
          <form>
            <div class="field">
              <Strong class="is-size-4">Create Deck</Strong>
            </div>
            <div class="field">
              <p class="control has-icons-left is-half">
                <input
                  type="text"
                  id="deck-name"
                  class="input"
                  placeholder="Name"
                  required
                  autofocus
                  v-model="name"
                />
                <span class="icon is-small is-left">
                  <i class="fab fa-buffer"></i>
                </span>
              </p>
            </div>
            <div class="field">
              <p class="control has-icons-left is-half">
                <input
                  type="text"
                  id="deck-description"
                  class="input"
                  placeholder="Description"
                  required
                  autofocus
                  v-model="description"
                />
                <span class="icon is-small is-left">
                  <i class="far fa-edit"></i>
                </span>
              </p>
            </div>
            <div class="tile select is-4 mb-3">
              <select v-model="categoryID" style="width: 200px">
                <option v-bind:value="null">Select Category</option>
                <option
                  v-for="category in categoryTypes"
                  v-bind:key="category.categoryID"
                  v-bind:value="category.categoryID"
                  >{{ category.categoryName }}</option
                >
              </select>
            </div>
            <div class="tile select is-4 mb-3">
              <select v-model="isPublic" style="width: 200px">
                <option v-bind:value="true">Public</option>
                <option v-bind:value="false">Private</option>
              </select>
            </div>
            <div class="field">
              <p class="control">
                  <button class="button is-success" v-on:click.prevent="saveDeck()">Save Deck</button>
              </p>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import User from "../store/index";
import CategoryService from "../services/CategoryService";
import DeckService from "../services/DeckService";

export default {
  name: "createDeck",
  data() {
    return {
      name: "",
      description: "",
      categoryID: null,
      isPublic: true,
      categoryTypes: [],
      userID: User.state.user.userId,
    };
  },
  methods: {
    saveDeck() {
      
      const payload = {
        name: this.name,
        description: this.description,
        categoryID: this.categoryID,
        public: this.isPublic,
        userID: this.userID,
      };
      DeckService.createDeck(this.userID, payload)
        .then((response) => {
          this.name = "";
          this.description = "";
          this.categoryID = null;
          this.isPublic = true;
          this.$router.push(`/create/card/deck/${response.data.deckID}`)
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  created() {
    console.log(this.userID)
    CategoryService.getCategories().then((response) => {
      this.categoryTypes = response.data;
    });
  },
};
</script>
