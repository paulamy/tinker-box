<template>
  <div class="column is-half is-offset-2 is-vcentered pt-6 mt-6">
    <div class="tile is-ancestor">
      <div class="tile is-parent">
        <div class="tile is-child box">
          <form class="form-register" @submit.prevent="register">
            <div class="alert alert-danger" role="alert" v-if="registrationErrors">
              {{ registrationErrorMsg }}
            </div>
            <div class="field">
              <Strong class="is-size-4">Create Account</Strong>
            </div>
            <div class="field">
              <p class="control has-icons-left has-icons-right is-half">
                <input
                  type="text"
                  id="username"
                  class="input"
                  placeholder="Username"
                  v-model="user.username"
                  required
                  autofocus
                />
                  <span class="icon is-small is-left">
                    <i class="fas fa-user"></i>
                  </span>
              </p>
            </div>
            <div class="field">
              <p class="control has-icons-left">
                <input
                  type="password"
                  id="password"
                  class="input"
                  placeholder="Password"
                  v-model="user.password"
                  required
                />
                  <span class="icon is-small is-left">
                    <i class="fas fa-lock"></i>
                  </span>
              </p>
            </div>
            <div class="field">
              <p class="control has-icons-left">
                <input
                  type="password"
                  id="confirmPassword"
                  class="input"
                  placeholder="Confirm Password"
                  v-model="user.confirmPassword"
                  required
                />
                  <span class="icon is-small is-left">
                    <i class="fas fa-lock"></i>
                  </span>
              </p>
            </div>
            <div class="field">
              <p class="control">
                <button class="button is-success" type="submit">Create Account</button>
              </p>
            </div>
            <router-link :to="{ name: 'login' }">Have an account?</router-link>
          </form>  
        </div>
      </div>
    </div>
  </div> 
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style></style>
