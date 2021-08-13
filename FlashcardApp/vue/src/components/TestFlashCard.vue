<template>
  <div>
    <!-- <flash-card></flash-card> -->
    <div id="flashcard">
    <div id="card-subject" class="columns is-centered is-6" style="height: 75px">
      <div class="column is-4">
        <h1 class="content is-size-4 has-text-grey-dark has-text-centered">{{ deckTitle }}</h1>
      </div>
    </div>
    <div id="card-display" class="columns is-centered is-vcentered mt-4">
      <div
        id="frontCard"
        class="column box is-half align-text flashcard-box "
        style="height: auto"
      >
        <div v-if="!isImage"><p  class="is-size-4 pt-4 mt-2">{{ isFlipped ? back : front }}</p></div>
        <div v-else><img :src="front" class="is-size-4 pt-4 mt-2"><p>{{ isFlipped ? back : front }}</p></div>
      </div>
    </div>
    <div class="field">
      <div
      class="columns control is-centered mt-6"
      id="text-input-answer"
      >
      <input
        class="column is-6 input"
        type="text"
        placeholder="Type Answer Here"
        v-model="userInput"
      />
      </div>
      <div class="columns is-centered mt-4">
        <div class="column control is-10 has-text-centered">
          <button v-on:click.prevent="flipCard()" class="button is-link mr-1">
            <p v-on:click.prevent="checkAnswer()">Submit</p>
          </button>
          <!-- <button
          v-on:click.prevent="toggleComplete()" 
          class="button ml-1" 
          v-bind:class="{ 'is-success': isMarkedForReview, 'is-danger' : !isMarkedForReview }"
          >
          {{ isMarkedForReview ? 'Mark Complete' : 'Mark For Review'}}
        </button> -->
        <button class="button ml-1 is-warning has-text-white" v-on:click.prevent="nextCard()">
          Next
        </button> 
        </div>
      </div>
    </div>
    
    
    <div class="columns mt-4 is-centered">
      <Strong>
        Currently Viewing Card {{ cardIndex + 1 }} of {{ numberOfCards }}
      </Strong>
    </div>
  </div>
    <!--<div
      id="mult-choice-answer"
      v-show="typeId === 2"
      class="columns is-centered is-6 mb-6"
      style="height: 150px"
    >
      <div class="column is-6">
        <h2 class="is-size-5 has-text-grey-dark is-centered mb-3">
          Please select the correct answer:
        </h2>
        <div class="control mb-2">
          <label class="radio">
            <input type="radio" name="answer" />
            Answer option 1 goes here
          </label>
        </div>
        <div class="control mb-2">
          <label class="radio">
            <input type="radio" name="answer" />
            Answer option 2 goes here
          </label>
        </div>
        <div class="control mb-2">
          <label class="radio">
            <input type="radio" name="answer" />
            Answer option 3 goes here
          </label>
        </div>
        <div class="control mb-2">
          <label class="radio">
            <input type="radio" name="answer" />
            Answer option 4 goes here
          </label>
        </div>
      </div>
    </div> -->
    <div class="columns mt-4 is-centered">
      <Strong>Correct Answers: {{correctAnswers}} / {{numberOfCards}}</Strong>
    </div>
    
    <!-- Timer -->
    <div>
    <div class="buttons columns is-centered mt-5" id="app">
      <!-- <button class="button is-success" @click="start">Start</button> -->
      <button class="button box is-danger" @click="stop()">End Test</button>
      <!-- <button class="button is-link" @click="reset">Reset</button> -->
    </div>
    <div class="columns is-centered has-text-centered">
      <p
        class="column box is-2 has-text-white is-size-3 notification is-primary"
      >
        {{ formattedElapsedTime }}
      </p>
    </div>
  </div>

  </div>
</template>

<script>
//import FlashCard from "../components/FlashCard.vue";
// import Timer from "../components/Timer.vue";
import User from "../store/index"
import SessionService from "../services/SessionService"

export default {
  components: { },
  name: "testFlashCard",
  data() {
    return {
      deckId: this.$route.params.deckId,
      currentDeck: null,
      cardIndex: 0,
      isCorrect: false,
      isFlipped: false,
      userInput: null,
      correctAnswers: 0,
      sessionDescription: "",
      elapsedTime: 0,
      timer: undefined,
      
    };
  },
  computed: {
    formattedElapsedTime: {
      get() {
        const date = new Date(null);
      date.setSeconds(this.elapsedTime / 1000);
      const utc = date.toUTCString();
      return utc.substr(utc.indexOf(":") +1, 6);
      }
    },
    isImage: {
      get() {
        if(this.currentCard.typeID == 4) return true;
        else return false;
      }
    },
    typeId: {
      get() {
        return this.currentDeck.deckID;
      },
    },
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
      }
    },
    isMarkedForReview: {
        get() {
          return this.currentDeck.cards[this.cardIndex].isMarkedForReview
          // return true;
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
    start() {
      this.timer = setInterval(() => {
        this.elapsedTime += 1000;
      }, 1000);
    },
    stop() {
      clearInterval(this.timer);
      const payload = {
        userID: User.state.user.userId,
        SessionDescription: this.deckTitle,
        SessionTypeID: 2,
        // TimeCompleted: "1:15",
        TimeCompleted: this.formattedElapsedTime,
        Score: this.correctAnswers,
        deckId: 1
      }
      console.log(this.elapsedTime);
      //need to save formattedElapsedTime in store
      SessionService.createSession(User.state.user.userId, payload).then((response) => {

        this.$router.push(`/sessions/${response.data.sessionID}`);
      }).catch((error) => {
        console.log(error);
      })

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
      this.userInput = "";
    },
    checkAnswer() {
      if(this.userInput.trim().toLowerCase().replace(/\s/g, "") === this.back.trim().toLowerCase().replace(/\s/g, ""))
      {
        return this.correctAnswers++;
      }
    },
    
  },
  created() {
    this.start();
    this.currentDeck = this.combineDecks().filter(
      (deck) => deck.deckID === this.deckId
    )[0];
  }
};
</script>

<style>

#start {
  background-color: rgb(23, 201, 100);
}
#stop {
  background-color: rgb(238, 110, 110);
}
#reset {
  background-color: yellow;
}
#time {
  padding-left: 3em;
}
#stopwatch-header {
  font-size: larger;
  padding-bottom: 5pt;
}
</style>
