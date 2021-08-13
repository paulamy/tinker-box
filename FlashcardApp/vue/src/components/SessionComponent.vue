<template>
  <div class="columns is-centered mt-6 pt-6">
    <div class="tile is-ancestor is-8">
      <div class="tile is-parent">
        <div id="sessionComponent" class="is-child box has-background-warning">
        <div class="level-item has-text-grey-dark is-size-2 pb-2">
          Session Report
        </div>
        <!-- SessionDescription, Time Created, Time Completed, Score -->
        <div class="level-item">
          <table class="table">
              <tr>
                <th>
                  Description
                </th>
                <th>
                  Time Elapsed
                <th>
                  Score
                </th>
              </tr>
              <tr>
                <td>
                  {{currentSession.sessionDescription}}
                </td>
                <td>
                  {{currentSession.timeCompleted}}
                </td>
                <td>
                  {{currentSession.score}}
                </td>
              </tr>
          </table>
        </div>
        <!-- <div class="level-item pt-5 has-text-grey-dark is-size-4">
          <h1 class="tile">
            Reviewed Cards: {{reviewedCards}}
          </h1>
          <h1 class="tile">
            Mastered Cards : {{masteredCards}}
          </h1>
        </div> -->
    </div>
  </div>
      </div>
    </div>
    
</template>

<script>
import SessionService from "../services/SessionService"
import User from "../store/index"

export default {
  name: 'sessionComponent',
  props: {
  },
  data() {
      return {
          currentSession: null,
      }
  },

  computed : {
    masteredCards : {
      get() {
        return this.currentSession.masteredCards.length
      }
    },
    reviewedCards : {
      get() {
        return this.currentSession.reviewCards.length
      }
    }
  },
  created(){
    SessionService.getSession(User.state.user.userId, this.$route.params.sessionID).then((response) => {
      this.currentSession = response.data;
    })
  },
}
</script>

<style>

</style>