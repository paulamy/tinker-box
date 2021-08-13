<template>
   <div class="has-background-info-light">
        <div>
            <h1 class="is-size-2 has-text-grey align-text">{{userName}}'s Profile</h1>
            <h2 class="is-size-3 has-text-grey">My Stats</h2>
            <p>Total Sessions: <span>{{sessionCount}}</span> 
            | Study Sessions: <span>{{studyCount}}</span> 
            | Test Sessions: <span>{{testCount}}</span></p> 
        </div><br>
        <div>
            <h2 class="is-size-3 has-text-grey">My Decks</h2>
                <div v-for="deck in myDecks" v-bind:key="deck.deckID">
                    <deck-component
                        class="mt-5 mb-5"
                        v-bind:deckName="deck.name"
                        v-bind:deckId="deck.deckID"
                        flashcardType="studyFlashcard"></deck-component>                  
                </div>
        </div>
        <!--div>
            <h2 class="is-size-3 has-text-grey">My Account</h2>
            <button v-on:click="$router.push()">Change Password</button>
        </div>-->             
   </div>            
</template>

<script>
import User from '@/store/index'
import DeckComponent from "@/components/DeckComponent.vue";
export default {
  name: "userProfile",
  components: { DeckComponent },
  data() {
      return {
          
      }
  },
  computed: {
    userName: {
        get() {
            return User.state.user.username
        }
    },
    myDecks: {
        get() {
        return this.$store.state.myDecks;
      }
    },
    mySessions: {
        get() {
            return this.$store.state.mySessions;
        }
    },
    sessionCount: {
        get() {
            return this.mySessions.length;
        }
    },
    studyCount: {
        get() {
            let studySessions = this.mySessions.filter(session => {
                return session.sessionTypeID === 1;
            });
            return studySessions.length;
        }
    },
    testCount: {
        get() {
            let testSessions = this.mySessions.filter(session => {
                return session.sessionTypeID === 2;
            });
            return testSessions.length;
        }
    }
},
}
</script>
