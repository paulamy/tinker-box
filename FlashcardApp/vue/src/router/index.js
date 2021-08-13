import Vue from 'vue'
import Router from 'vue-router'

import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import Study from '../views/Study.vue'
import Create from '../views/Create.vue'
import CreateCard from '../components/CreateCard.vue'
import Test from '../views/Test.vue'
import Discover from '../views/Discover.vue'
import FlashCard from '../components/FlashCard.vue'
import TestFlashCard from '../components/TestFlashCard.vue'
import Profile from '@/views/Profile.vue'
import Home from '@/views/Home.vue'
import SessionComponent from '@/components/SessionComponent.vue'

import store from '../store/index'


Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/study',
      name: 'study',
      component: Study,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/create',
      name: 'create',
      component: Create,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/create/card/deck/:deckId',
      name: 'createCard',
      component: CreateCard,
      meta : {
        requiresAuth: true
      }
    }, 
    {
      path: '/test',
      name: 'test',
      component: Test,
      meta : {
        requiresAuth: true
      }
    },
    {
      path: '/discover',
      name: 'discover',
      component: Discover,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/studyFlashcard/deck/:deckId',
      name: 'studyFlashcard',
      component: FlashCard,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/testFlashcard/deck/:deckId',
      name: 'testFlashcard',
      component: TestFlashCard,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/profile",
      name: "profile",
      component: Profile,
      meta:{
        requiresAuth: false
      }
    },
    {
      path: "/sessions/:sessionID",
      name: "session",
      component: SessionComponent,
      meta : {
        requiresAuth: true
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
  next();
});

export default router;
