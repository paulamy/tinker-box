<template>
  <div id="app" style="height: 1000px">
    <section>
      <div id="nav">
        <nav
          class="navbar p-4 has-background-info"
          role="navigation"
          aria-label="main navigation"
        >
          <div class="navbar-brand" id="logo-home-button">
            <div class="nabar-item icon-text">
              <router-link v-bind:to="{ name: 'home' }">
                <span>
                  <Strong
                    ><i
                      class="fas fa-graduation-cap is-size-1 has-text-white"
                    ></i
                  ></Strong>
                </span>
                <span>
                  <Strong class="is-vcentered is-size-2 has-text-white"
                    >quiz-liTE</Strong
                  >
                </span>
              </router-link>
            </div>
          </div>
          <div class="navbar-menu is-active has-background-info is-shadowless" id="user-options">
            <div class="navbar-end">
              <div class="navbar-item">
                <div class="buttons">
                  <a class="button is-primary" v-show="!isUserLoggedIn">
                    <router-link v-bind:to="{ name: 'register' }">
                      Sign up
                    </router-link>
                  </a>
                  <a class="button is-light" v-show="!isUserLoggedIn">
                    <router-link v-bind:to="{ name: 'login' }">
                      Log In
                    </router-link>
                  </a>
                  <a class="button is-light" v-show="isUserLoggedIn">
                    <router-link
                      v-bind:to="{ name: 'logout' }"
                      v-if="$store.state.token != ''"
                      >Logout</router-link
                    >
                  </a>
                  <a class="button is-primary" v-show="isUserLoggedIn">
                    <router-link
                      v-bind:to="{ name: 'profile' }"
                      v-if="$store.state.token != ''"
                      >User Profile</router-link
                    >
                  </a>
                </div>
              </div>
            </div>
          </div>
        </nav>
      </div>
    </section>
    <section style="height: 1000px">
      <div class="columns pt-3">
        <div
          id="sidbar"
          class="column is-2 has-background-info-light"
          style="height: 1000px"
        >
          <aside class="menu mt-6 pt-6">
            <ul class="menu-list">
              <li>
                <h2 class="title is-3">
                  <router-link v-bind:to="{ name: 'study' }">Study</router-link>
                </h2>
              </li>
              <li>
                <h2 class="title is-3">
                  <router-link v-bind:to="{ name: 'create' }"
                    >Create</router-link
                  >
                </h2>
              </li>
              <li>
                <h2 class="title is-3">
                  <router-link v-bind:to="{ name: 'test' }">Test</router-link>
                </h2>
              </li>
              <li>
                <h2 class="title is-3">
                  <router-link v-bind:to="{ name: 'discover' }"
                    >Discover</router-link
                  >
                </h2>
              </li>
            </ul>
          </aside>
        </div>
        <div
          id="content"
          class="column is-10 is-vcentered"
          style="height: 1000px"
        >
          <router-view />
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import User from "./store/index"
export default {
    name: "app",
    computed : {
      isUserLoggedIn : {
        get() {
          return User.state.user.userId
        }
      }
    } 
}
</script>
