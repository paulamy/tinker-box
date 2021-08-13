<template>
    <div class="columns is-centered mt-6">
        <div class="tile is-ancestor is-8">
            <div class="tile is-parent">
                <div class="tile is-child box has-background-info-light">
                    <user-profile></user-profile>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import User from '@/store/index'
import UserProfile from '@/components/UserProfile.vue'
import DeckService from '@/services/DeckService'
import SessionService from '@/services/SessionService'

export default {
  name: "profile",
  components: { UserProfile },
  computed : {
      UserId : {
        get() {
          return User.state.user.userId
        }
      }
    },
  created() {
      SessionService.getSessions(this.UserId).then((response) => {
      this.$store.commit("SET_MY_SESSIONS", response.data);
    });
      DeckService.getDecksByUserId(this.UserId).then((response) => {
      this.$store.commit("SET_MY_DECKS", response.data);
    });
    console.log("Created");
    DeckService.getDecks().then((response) => {
      this.$store.commit("SET_PUBLIC_DECKS", response.data);
    });
}
};
</script>