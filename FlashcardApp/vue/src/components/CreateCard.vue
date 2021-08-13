<template>
  <div class="columns is-centered pt-6 mt-6">
    <div class="tile is-ancestor is-6">
      <div class="tile is-parent">
        <div class="tile is-child box has-background-info-light">
          <form>
            <div class="field">
              <Strong class="is-size-4">Create Card</Strong>
            </div>
            <div class="field">
              <p class="control has-icons-left is-half">
                <textarea
                  type="text"
                  id="question"
                  class="input"
                  placeholder="Question"
                  required
                  autofocus
                  v-model="front"
                />
                <span class="icon is-small is-left">
                  <i class="fas fa-question"></i>
                </span>
              </p>
            </div>
            <div class="field">
              <p class="control has-icons-left is-half">
                <textarea
                  type="text"
                  id="answer"
                  class="input"
                  placeholder="Answer"
                  required
                  autofocus
                  v-model="back"
                />
                <span class="icon is-small is-left">
                  <i class="fas fa-check"></i>
                </span>
              </p>
            </div>
            <div class="field">
              <p class="control has-icons-left is-half">
                <input
                  type="text"
                  id="tag"
                  class="input"
                  placeholder="Subject Tag"
                  required
                  autofocus
                  v-model="tag"
                />
                <span class="icon is-small is-left">
                  <i class="far fa-edit"></i>
                </span>
              </p>
            </div>
            <div class="tile select is-4 mb-3">
              <select v-model="cardType" style="width: 180px">
                <!-- <option>Multiple Choice</option> -->
                <option v-bind:value="null">Select Type</option>
                <option v-bind:value="2">Question</option>
                <option v-bind:value="3">Definition</option>
                <option v-bind:value="4">Image</option>
                <!-- <option>Image</option> -->
              </select>
            </div>
            <div class="tile select is-4 mb-3">
              <select v-model="difficulty" style="width: 180px">
                <option v-bind:value="null">Select Difficulty</option>
                <option v-bind:value="1">Easy</option>
                <option v-bind:value="3">Intermediate</option>
                <option v-bind:value="5">Challenging</option>
              </select>
            </div>
            <div class="field">
              <p class="control">
                <button
                  class="button is-success"
                  v-on:click.prevent="saveCard()"
                >
                  Save Card
                </button>
              </p>
            </div>
            <div class="field">
              <!-- <p class="control">
                <button class="button is-danger">Complete Deck</button>
              </p> -->
            </div>
          </form>
          <p class="control">
            <button class="button is-danger" v-on:click="completeDeck()">Complete Deck</button>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CardService from "../services/CardService";
import User from "../store/index";

export default {
  name: "create",
  data() {
    return {
      front: "",
      back: "",
      tag: [],
      cardType: null,
      difficulty: null,
      deckId: this.$route.params.deckId,
      userId: User.state.user.userId,
    };
  },
  methods: {
    saveCard() {
      const payload = {
        front: this.front,
        back: this.back,
        tags: [this.tag],
        difficulty: this.difficulty,
        typeID: this.cardType,
      };
      CardService.createCard(this.userId, this.deckId, payload)
        .then(() => {
          // console.log(response.data)
          this.front = "";
          this.back = "";
          this.tag = "";
          this.cardType = null;
          this.difficulty = null;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    completeDeck() {
      this.$router.push(`/study`);
    },
  },
};
</script>
