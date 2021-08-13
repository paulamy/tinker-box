<template>
  <div>
      <create-deck></create-deck>
  </div>
    <!-- <div class="columns is-vcentered">
      <div class="column is-5">
        <div class="tile is-ancestor">
          <div class="tile is-parent is-vertical mt-6 ml-6 mr-3 pt-6" style="height: 150px">
            <div class="tile is-child box has-background-primary is-size-4 has-text-white">Create Deck</div>
          </div>
        </div>  
      </div>
      <div class="column is-5">
        <div class="tile is-ancestor is-vertical">
          <div class="tile-is-parent box has-background-warning" style="height: 100px">Create Card</div>
        </div>
      </div>
    </div>
    <div class="column is-half is-offset-2 is-vcentered pt-6 mt-6">
      <div class="tile is-ancestor">
        <div class="tile is-parent">
          <div class="tile is-child box">
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
              <div class="field select">
                <select v-model="categoryID">
                  <option v-bind:value="null">Select Category</option>
                  <option
                    v-for="category in categoryTypes"
                    v-bind:value="category.categoryID"
                    v-bind:key="category.categoryID"
                    >{{ category.categoryName }}</option
                  >
                </select>
              </div>
              <div class="field select">
                <select v-model="isPublic">
                  <option v-bind:value="true">Public</option>
                  <option v-bind:value="false">Private</option>
                </select>
              </div>
              <div class="field">
                <p class="control">
                  <button 
                    class="button is-success" 
                    v-on:click.prevent="saveDeck()"
                  >
                    Save Deck
                  </button>
                </p>
              </div>
            </form>
          </div>
          <div class="tile is-ancestor">
            <div class="tile is-parent is-vertical mt-6 ml-3 mr-6 pt-6" style="height: 150px"></div>
            <div class="tile is-child box has-background-primary is-size-4 has-text-white">Create Card</div>
          </div>
        </div>
      </div>    
    </div>
  </div> -->
</template>

<script>

import CreateDeck from "../components/CreateDeck"

export default {
  name: "create",
  components: {
    CreateDeck
  }
};
</script>
