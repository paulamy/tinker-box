import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {

        currentProblem: 0,
        problemSet: []
    },
    mutations: {
        SET_CURRENT_PROBLEM(state, index) {
            state.currentProblem = index;
        },
        ADD_PROBLEM(state, problem){
            state.problemSet.push(problem);
        },
        CLEAR_PROBLEM_SET(state) {
            state.problemSet = [];
        }
    },
    actions: {},
    modules: {}
})