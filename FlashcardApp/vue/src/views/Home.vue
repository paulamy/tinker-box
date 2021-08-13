<template>
    <div class="columns is-centered mt-6">
      <div class="tile is-ancestor is-8">
        <div class="tile is-parent">
          <div class="tile is-child box has-background-info-light">
            <div>
            <p class="title is-3 has-text-grey">Welcome to Quiz-liTE!</p>
            </div>
            <div>
            <p class="is-size-5">Qiuz-liTE is your new favorite study resource! From here you can study any subject, test your knowledge, and even create resources of your own!</p>
            </div>
            <br>
            <p class="is-size-5">Explore our library of public flashcard decks or create an account and start building and sharing decks with the Quiz-liTE community.</p>
            <br>
            <div v-on:click="$router.push(isLoggedInStudy)" class="box has-background-warning has-text-white">
              <p class="is-size-5">Study</p>
              <p class="is-size-6">A great resource for learning new subjects!</p>
            </div>
            <div v-on:click="$router.push(isLoggedInCreate)" class="box has-background-primary has-text-white">
              <p class="is-size-5">Create</p>
              <p class="is-size-6">Build your own flashcard decks based on any subject! Keep them for yourself or share them with the world.</p>
            </div>
            <div v-on:click="$router.push(isLoggedInTest)" class="box has-background-danger has-text-white">
              <p class="is-size-5">Test</p>
              <p class="is-size-6">Test your knowledge on flashcard decks of varying difficulty.</p>
            </div>
            <div v-on:click="$router.push('/discover')" class="box has-background-info has-text-white">
              <p class="is-size-5">Discover</p>
              <p class="is-size-6">Explore all of the public decks in our library!</p>
            </div>
            
          </div>
        </div>
      </div>
      </div>
</template>

<script>
import User from '@/store/index'
import DeckService from '@/services/DeckService.js'

export default {
    name: 'home',
    return: {
      userID: User.state.user.userId
    },
    computed : {
      UserId : {
        get() {
          return User.state.user.userId
        }
      },
      isLoggedInStudy: {
        get() {
          return this.UserId ? '/study' : '/login';
        }
        
      },
      isLoggedInCreate: {
          get() {
          return this.UserId ? '/create' : '/login';
        }
      },
      isLoggedInTest: {
          get() {
          return this.UserId ? '/test' : '/login';
        }
      }
    },
    created() {
    if(!this.UserId)
    {
      DeckService.getDecks().then((response) => {
      this.$store.commit("SET_INITIAL_PUBLIC_DECKS", response.data);
    });
    }
 }
}
</script>