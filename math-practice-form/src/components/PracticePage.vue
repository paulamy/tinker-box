<template>
    <div>
        <section id="problem">
        <div class="expression"><p id="currentExpression"></p></div>
        <p id="stats">
            Problem: <span class="currentProblem">1</span>/10 | Score: <span class="currentScore">0</span>
        </p>
        </section>
        <section id="answers" class="show-hide">
        <ul>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
        </section>
        <button id="btnStartOver">Start Over</button>
    </div>
</template>

<script>
export default {
    props: ["operators"],
    name: "practice-page"
}
</script>

<style scoped>
#problem .expression {
  font-size: 48px;
  border: 1px solid #eeeeee;
  padding: 10px;
  margin: 0 40px;
}

#answers ul {
  margin: 0;
  padding: 0;
  margin: 40px auto;
  display:flex;
  list-style-type: none;
  justify-content: space-around;
}
#answers li {
  margin: 0 20px;
  padding: 20px 30px;
  cursor: pointer;
  background-color: #eeeeee;
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
</style>