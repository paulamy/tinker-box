<template>
<router-link v-bind:to="{name: flashcardType, params: {deckId : deckId}}">
  <div id="deckComponent" class="box has-background-warning box has-text-centered">
    <button class="button is-warning">
      <p class="is-size-5">{{deckName}}</p>
    </button>
  </div><br>
</router-link>
</template>

<script>
export default {
  name: 'deckComponent',
  props: {
      deckName: String,
      deckId: Number,
      flashcardType: String,
  },
  data() {
      return {
          message: "Deck Component"
      }
  }
}
</script>

<style>

</style>