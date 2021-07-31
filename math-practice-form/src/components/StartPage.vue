<template>
    <div>
        <h1>Math Practice</h1>
        <form v-on:submit.prevent><p>What type of practice would you like to do?</p>
            <div class="settings">
                <input type="checkbox" id="add" v-model="operatorChoices.add">
                <label for="add">Addition</label>
                <input type="checkbox" id="subtract" v-model="operatorChoices.subtract">
                <label for="subtract">Subtraction</label>
                <input type="checkbox" id="multiply" v-model="operatorChoices.multiply">
                <label for="multiply">Multiplication</label>
                <input type="checkbox" id="divide" v-model="operatorChoices.divide">
                <label for="divide">Division</label> <br><br>
                <label for="problemCount">Number of Problems: </label> 
                <input type="number" 
                id="problemCount" 
                required
                max="100" 
                min="5" 
                placeholder="5-100" 
                v-model="numberOfProblems">
                <span class="validity"></span> <br><br>
                <label for="negatives">Include negative numbers: </label>
                <input type="checkbox" id="negatives" v-model="negatives">
                <p>Orders of Magnitude:</p>
                <input type="radio" id="digits" value="10" v-model="orderOfMagnitude">
                <label for="digits">0-9</label><br>
                <input type="radio" id="tens" value="100" v-model="orderOfMagnitude">
                <label for="tens">0-99</label><br>
                <input type="radio" id="hundreds" value="1000" v-model="orderOfMagnitude">
                <label for="hundreds">0-999</label><br>
                <input type="radio" id="thousands" value="10000" v-model="orderOfMagnitude">
                <label for="thousands">0-9999</label><br>
                
            </div>
            <div class="actions">
                <button type="submit" v-on:click="problemSetInitialize">Generate Problem Set</button>
                <!-- <router-link v-bind:to="{name: 'Practice'}" tag="button" type="submit" v-on:click="problemSetInitialize"> 
                    Start Practicing
                </router-link> -->
            </div>
        </form>
    </div>
</template>

<script>

export default {
    name: "start-page",
    data() {
        return {
            orderOfMagnitude: 10,
            numberOfProblems: 10,
            negatives: false,
            operatorChoices: {
                add: false,
                subtract: false,
                multiply: false,
                divide: false
            },
            operators: [],
        }
    },
    computed: {
        maxInteger() {
            return parseInt(this.orderOfMagnitude);
        }
    },
    methods: {
        getRandomNumber(max) {
            return Math.trunc(Math.random() * Math.trunc(max))
        },
        shuffleArray(array) {
            return array.sort()
        },
        problemSetInitialize() {
            if(this.operatorChoices.add) {this.operators.push("+")}
            if(this.operatorChoices.subtract) {this.operators.push("-")}
            if(this.operatorChoices.multiply) {this.operators.push("*")}
            if(this.operatorChoices.divide) {this.operators.push("/")}
            else {
                let randomOperatorSet = ["+", "-", "*", "/"];
                this.operators.push(randomOperatorSet[this.getRandomNumber(3)]);
            }
            this.generateProblemSet(this.numberOfProblems, this.operators)
        },
        generateRandomProblem(operator) {
            let problem = {};
            let answerArray = [];
            let a;
            let b;
            if(this.negatives)
            {
                let temp1 = this.getRandomNumber(this.maxInteger);
                let temp2 = this.getRandomNumber(this.maxInteger);
                a = this.getRandomNumber(2) == 1 ? temp1 : temp1 - (2*temp1);
                b = this.getRandomNumber(2) == 1 ? temp2 : temp2 - (2*temp2);
            }
            else 
            {
                a = this.getRandomNumber(this.maxInteger);
                b = this.getRandomNumber(this.maxInteger);
            }
            let result;
            switch(operator) {
                case '+' : 
                    result = a + b;
                break;
                case '-' : 
                    result = a - b;
                break;
                case '*' : 
                    result = a * b;
                break;
                case '/' : 
                    result = Number((a / b).toFixed(3));
                break;
            }

            answerArray.push(result);

            for (let i = 1; i < 4; i++)
            {
                let c;
                let d;
                if(this.negatives)
                {
                    let temp1 = this.getRandomNumber(this.maxInteger);
                    let temp2 = this.getRandomNumber(this.maxInteger);
                    c = this.getRandomNumber(2) == 1 ? temp1 : temp1 - (2*temp1);
                    d = this.getRandomNumber(2) == 1 ? temp2 : temp2 - (2*temp2);
                }
                else 
                {
                    c = this.getRandomNumber(this.maxInteger);
                    d = this.getRandomNumber(this.maxInteger);
                }
                let randAnswer;
                switch(operator) {
                    case '+' : 
                        randAnswer = c + d;
                    break;
                    case '-' : 
                        randAnswer = c - d;
                    break;
                    case '*' : 
                        randAnswer = c * d;
                    break;
                    case '/' : 
                        randAnswer = Number((c / d).toFixed(3));
                    break;
                }
                answerArray.push(randAnswer);
            }

            let answers = this.shuffleArray(answerArray);
            let correct = result;
            
            problem = {
                expression: `${a} ${operator} ${b}`,
                answerChoices: answers,
                correctAnswer: correct,
                number: null
            }
            
            return problem;
                },
        generateProblemSet(size, operators) {
            for (let i = 0; i < size; i++)
            {
                let randOperator = operators[this.getRandomNumber(operators.length)];
                let randProblem = this.generateRandomProblem(randOperator);
                randProblem.number = i;
                this.$store.commit("ADD_PROBLEM", randProblem)
            }
            
        }
    }
}
</script>

<style scoped>
body {
  height: 100vh;
  font-family: Arial, Helvetica, sans-serif;
  width: 800px;
  margin: 0 auto;
  text-align: center;
}
h1 {
  font-family: 'Bangers', cursive;
  font-size: 5rem;
}
button {
  display: block;
  margin: 40px auto;
  padding: 8px 12px;
  background-color: #ABD587;
  text-transform: uppercase;
  border: 0px;
  border-radius: 4px;
  font-size: 20px;
}

input:invalid+span:after {
  content: '✖';
  padding-left: 5px;
}

input:valid+span:after {
  content: '✓';
  padding-left: 5px;
}
</style>