import axios from 'axios'
//import axis from 'axios'

export default {
    getSessions(userId){
        return axios.get(`sessions/user/${userId}`)
    },
    getSession(userId, sessionId){
        return axios.get(`sessions/user/${userId}/session/${sessionId}`)
    },

    createSession(userId, payload){
        return axios.post(`sessions/user/${userId}`, payload)
    }
}