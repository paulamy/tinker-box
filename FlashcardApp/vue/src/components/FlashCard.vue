<template>
  <div id="flashcard">
    <div id="card-subject" class="columns is-centered is-6" style="height: 55px">
      <div class="column is-4">
        <h1 class="content is-size-4 has-text-centered has-text-grey-dark">{{ deckTitle }}</h1>
      </div>
    </div>
    <div class="columns is-centered mt-4">
      <div 
        class="column tag is-2"
        v-bind:class="{'is-success' : !isReviewArr[cardIndex], 'is-danger' : isReviewArr[cardIndex]}"
      >
        {{isReviewArr[cardIndex] ? 'Needs Review' : 'Completed'}}
      </div>
    </div>
    <div class="columns is-flex is-vcentered is-centered mt-5">
      <div id="backArrow" class="column is-2">
        <div class="columns">
          <div class="column"></div>
          <div class="column">
            <a v-on:click.prevent="previousCard()"
              ><i class="fas fa-angle-left is-size-1" style="height: 50%"></i
            ></a>
          </div>
          <div class="column"></div>
        </div>
      </div>
      <div
        id="frontCard"
        class="column box is-half align-text flashcard-box "
        style="height: auto"
      >
        <div v-if="!isImage"><p  class="is-size-4 pt-4 mt-2">{{ isFlipped ? back : front }}</p></div>
        <figure v-else><img :src="front" class="is-size-4 pt-4 mt-2 justify-content" ><p>{{ isFlipped ? back : front }}</p></figure>
      </div>
      <div id="forwardArrow" class="column is-2">
        <div class="columns">
          <div class="column"></div>
          <div class="column ">
            <a v-on:click.prevent="nextCard()"
              ><i
                class="fas fa-angle-right is-size-1 is-pulled-right"
                style="height: 50%"
              ></i
            ></a>
          </div>
          <div class="column"></div>
        </div>
      </div>
    </div>
    <div class="columns">
      <div class="column is-centered has-text-centered">
        <button v-on:click.prevent="flipCard()" class="button is-link mr-5">
          Flip Card
        </button>
        <button
          v-on:click.prevent="flipReview()"
          class="button"
          v-bind:class="{ 'is-success': isReviewArr[cardIndex], 'is-danger': !isReviewArr[cardIndex] }"
        >
          {{ isReviewArr[cardIndex] ? "Mark Completed" : "Mark for Review" }}
        </button>
        <div></div>
      </div>
    </div>
    <div class="columns mt-4 is-centered">
      <Strong>
        Currently Viewing Card {{ cardIndex + 1 }} of {{ numberOfCards }}
      </Strong>
    </div>
  </div>
</template>

<script>
export default {
  name: "flashcardComponent",
  data() {
    return {
      isFlipped: false,
      cardIndex: 0,
      deckId: this.$route.params.deckId,
      currentDeck: null,
      isReview: false,
      isReviewArr: [],
    };
  },
  computed: {
    currentCard: {
      get() {
        return this.currentDeck.cards[this.cardIndex];
      },
    },
    numberOfCards: {
      get() {
        return this.currentDeck.cards.length;
      },
    },
    front: {
      get() {
        return this.currentCard.front;
      },
    },
    back: {
      get() {
        return this.currentCard.back;
      },
    },
    deckTitle: {
      get() {
        return this.currentDeck.name;
      },
    },
    isMarkedForReview: {
        get() {
          return this.isReviewArr[this.cardIndex]
        }
      },
    
    isImage: {
      get() {
        if(this.currentCard.typeID == 4) return true;
        else return false;
      }
    }
  },
  methods: {
    flipReview() {
      const index = this.cardIndex;
      const newValue = !this.isReviewArr[index];
      this.isReviewArr.splice(index,1, newValue);
    },
    combineDecks() {
      return this.$store.state.myDecks.concat(this.$store.state.publicDecks);
    },
    flipCard() {
      this.isFlipped = !this.isFlipped;
    },
    nextCard() {
      if (this.cardIndex + 1 < this.numberOfCards) {
        this.cardIndex++;
      } else {
        this.cardIndex = 0;
      }
      this.isFlipped = false;
    },
    previousCard() {
      if (this.cardIndex === 0) {
        this.cardIndex = this.numberOfCards - 1;
      } else {
        this.cardIndex--;
      }
      this.isFlipped = false;
    },
  },
  created() {
    this.currentDeck = this.combineDecks().filter(
      (deck) => deck.deckID === this.deckId
    )[0];
    
    for(let i = 0; i < this.numberOfCards; i++){
      this.isReviewArr.push(false);
    }
  },
};
</script>

<style>
.flashcard-box {
  /* border-radius: 5px; */
  background-color: rgb(245, 245, 245);
}
.align-text {
  text-align: center;
}
</style>
