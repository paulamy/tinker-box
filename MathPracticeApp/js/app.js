//Known Issues:
//1. Answer of NaN for 0/0 doesn't register correct
//2. Duplicate problems in set
//3. Duplicate answer choices, unclear if duplicate correct answers work

//Global constants to define the problem sets and answer choices
const numberOfProblems = 10;
const numberOfAnswers = 4;
const maxNumberBound = 10;
const maxAnswerBound = 82;
const operators = []
//Global variables 
let answerButtons;
let expression;
let number;
let choices;
let correctAnswer;
let displayScore;
let displayProblem;
let problemNumber = 0;
let showHide;
let startOver;
//Holds relevant information about each practice session
class Session {
    constructor(problems, score, indexOfProblem) {
        this.problems= problems;
        this.answerCount = 0;
        this.score = score;
    }
}

//Wait till the DOM is fully loaded
document.addEventListener("DOMContentLoaded", (event) => {
    answerButtons = document.querySelectorAll("li");
    expression = document.getElementById("currentExpression");
    displayScore = document.querySelector(".currentScore");
    displayProblem = document.querySelector(".currentProblem")
    showHide = document.querySelectorAll(".show-hide");
    startOver = document.getElementById("btnStartOver");
    answerButtons.forEach((btn) => {
        btn.addEventListener("click", event => {
            practice.answerCount+=1;
            problemNumber+=1;
            if(event.target.innerText == correctAnswer)
            {
                practice.score+=1;
                updateDisplay(practice);
            }
            else 
            {
                updateDisplay(practice);
            }
            if (practice.answerCount<numberOfProblems)
            {
                getNextProblem(practice, problemNumber);
            }
            else 
            {
                showSummary();
            }
        })
    })
    initialize();
})

/**
 * Begin the program
 */
function initialize(){
    startScreen();
    startOver.addEventListener("click", event => {
        showHide.forEach(element => {
            element.classList.remove('hidden');
        })
        problemSetOperators();
        if(operators.length == 0) operators.push("*");
    document.querySelector('.directions').classList.add('hidden');
    document.getElementById('stats').classList.remove('hidden');
    startOver.addEventListener("click", (event) => reset());
    startOver.innerText = "Start Over";
    practice = new Session(generateProblemSet(numberOfProblems, operators), 0, 0); 
    getNextProblem(practice, problemNumber);    
    })  
}

/**
 * Display the initial screen letting users select operations to practice
 */
function startScreen()
{
    showHide.forEach(element => {
        element.classList.add("hidden");
    })
    document.querySelector('.directions').classList.remove('hidden');
    document.getElementById('stats').classList.add('hidden');
    startOver.innerText = "Start";
}

/**
 * Reset to the original screen
 */
function reset(){
    location.reload();
}

/**
 * Show the summary screen after the practice session
 */
function showSummary(){
    showHide.forEach(element => {
            element.classList.add("hidden");
    })
    
}

/**
 * Update the page to display a problem from the set
 * @param {Session} practice the session object that holds the problem set and tracks completed problems and score
 * @param {number} problemNumber the index of the current problem
 */
function getNextProblem(practice, problemNumber){
    //load up a problem from the set
    const currentProblem = {
        expression: practice.problems[problemNumber].expression,
        choices: practice.problems[problemNumber].answerChoices,
        answer: practice.problems[problemNumber].correctAnswer,
        number: practice.problems[problemNumber].number
    }
    //populate the information on the page
    expression.innerText = currentProblem.expression;
    number = currentProblem.number;
    correctAnswer = currentProblem.answer;
    choices = currentProblem.choices;
    for(let n = 0; n < choices.length; n++)
    {
        answerButtons[n].innerText = choices[n];
    }
}

/**
 * Update the stat display
 * @param {Session} practice the session object that holds the problem set and tracks completed problems and score
 */
function updateDisplay(practice) {
    if (practice.answerCount <= 10)
    {
        displayScore.innerText = practice.score;
    }
    if(practice.answerCount < 10)
    {
        displayProblem.innerText = practice.answerCount+1;
    }
}

// Utility functions for creating the random problem set

/**
 * Generate a random integer up to the specified maximum value
 * @param {number} max Exclusive upper boundary for random integer
 */
function getRandomNumber(max) {
    return Math.trunc(Math.random() * Math.trunc(max))
}

/**
 * Shuffle an input array
 * @param {[]} arr input array
 */
function shuffleArray(arr) {
    return arr.sort(function (a,b) { return Math.random() - 0.5})
}

/**
 * Track the selected operations for the practice session
 */
function problemSetOperators() {
    let operatorChoices = [];
    operatorChoices.push(document.querySelector("#add"));
    operatorChoices.push(document.querySelector("#subtract"));
    operatorChoices.push(document.querySelector("#multiply"));
    operatorChoices.push(document.querySelector("#divide"));
    operatorChoices.forEach(element => {
        if(element.checked && !operators.includes(element.id))
        {
            operators.push(element.id);
        }
    })
    for (let i = 0; i < operators.length; i++)
    {
        switch(operators[i]) {
            case 'add' : 
                operators[i] = "+";
            break;
            case 'subtract' : 
                operators[i] = "-";
            break;
            case 'multiply' : 
                operators[i] = "*";
            break;
            case 'divide' : 
                operators[i] = "/";
            break;
        }
    }
        return operators;
}

/**
 * Generates a random problem using the specified operation
 * @param {string} operator takes an input string of "*", "/", "+", or "-" 
 */
function generateRandomProblem(operator) {
    let problem = {};
    let answerArray = [];
    let a = getRandomNumber(maxNumberBound);
    let b = getRandomNumber(maxNumberBound);
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
            result = a / b;
        break;
    }

    answerArray.push(result);

    for (let i = 1; i < numberOfAnswers; i++)
    {
        let c = getRandomNumber(maxNumberBound);
        let d = getRandomNumber(maxNumberBound);
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
                randAnswer = c / d;
            break;
        }
        answerArray.push(randAnswer);
    }

    let answers = shuffleArray(answerArray);
    let correct = result;
    
    problem = {
        expression: `${a} ${operator} ${b}`,
        answerChoices: answers,
        correctAnswer: correct,
        number: null
    }
    
    return problem;
}

/**
 * Generates a random problem set of the specified size using the specified operations
 * @param {number} size How many problems to generate
 * @param {string} operators Which operations to use; takes an input string of "*", "/", "+", or "-" 
 */
function generateProblemSet(size, operators) {
    let problemSet = [];
    for (let i = 0; i < size; i++)
    {
        randOperator = operators[getRandomNumber(operators.length)];
        randProblem = generateRandomProblem(randOperator);
        randProblem.number = i;
        problemSet.push(randProblem);
    }
    return problemSet;
}