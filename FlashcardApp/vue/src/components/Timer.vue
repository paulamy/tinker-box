<template>
  <div>
    <div class="buttons columns is-centered mt-5" id="app">
      <!-- <button class="button is-success" @click="start">Start</button> -->
      <button class="button box is-danger" @click="stop">End Test</button>
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
</template>

<script>
import SessionService from "../services/SessionService"
import User from "../store/index"

export default {
  name: "myTimer",
  props: {
    // sessionDescription: String,
    correctAnswers: String,
    sessionDescription: String,
  },
  data() {
    return {
      elapsedTime: 0,
      timer: undefined,
      currentSession = this.$store.currentSession
    };
  },
  computed: {
    formattedElapsedTime() {
      const date = new Date(null);
      date.setSeconds(this.elapsedTime / 1000);
      const utc = date.toUTCString();
      return utc.substr(utc.indexOf(":") +1, 6);
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
        // SessionDescription: "TestDes",
        SessionDescription: this.props.sessionDescription,
        SessionTypeID: 2,
        TimeCompleted: this.elapsedTime,
        Score: this.props.correctAnswers,
        // deckId: 1
      }
      //need to save formattedElapsedTime in store
      SessionService.createSession(User.state.user.userId, payload).then((response) => {

        this.$router.push(`/sessions/${response.data.sessionID}`);
      }).catch((error) => {
        console.log(error);
      })

    },
    reset() {
      this.elapsedTime = 0;
    }
  },
  created() {
    this.start();
  }
};
</script>

<style scoped>
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
