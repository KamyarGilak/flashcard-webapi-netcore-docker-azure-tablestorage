<template>
  <div id="app">

    <img alt="Vue logo" src="./assets/apps_logo.svg">
    <CardInputForm msg="Flash Card App"/>

    <div class="card-input-form">
      <label for="front">Term
        <input v-model.trim="newFront" type="text" id="front">
      </label>
      <label for="back">Definition
        <input v-on:keypress.enter="addNew" v-model.trim="newBack" type="text" id="back">
      </label>
      <button v-on:click="addNew">Add a New Card</button>
      <span class="error" v-show="error">Oops! Flashcards need a front and a back.</span>
    </div>

    <ul class="flashcard-list">
      <li v-for="(card, index) in cards" v-on:click="toggleCard(card)">
        <transition name="flip">
          <p class="card" v-if="!card.flipped" key="front">
            {{card.front}}
            <span class="delete-card" v-on:click="cards.splice(index, 1)">X</span>
          </p>
          <p class="card" v-else key="back">
            {{card.back}}
            <span class="delete-card" v-on:click="cards.splice(index, 1)">X</span>
          </p>
        </transition>
      </li>
    </ul>



  </div>
</template>

<script>
import CardInputForm from './components/CardInputForm'

const cards = [
  {
    front: 'The "First Computer Programmer"',
    back: 'Ada Lovelace',
    flipped: false,
  },
  {
    front: 'Invented the "Clarke Calculator"',
    back: 'Edith Clarke',
    flipped: false,

  },
  {
    front: 'Famous World War II Enigma code breaker',
    back: 'Alan Turing',
    flipped: false,
  },
  {
    front: 'Created satellite orbit analyzation software for NASA',
    back: 'Dr. Evelyn Boyd Granville',
    flipped: false,
  },
];

export default {
  name: 'app',

  data () {
    return {
      cards: cards,
      newFront: '',
      newBack: '',
      error: false
    }
  },

  methods: {
    toggleCard: function(card){
      card.flipped = !card.flipped;
    },
    addNew: function(){
      if(!this.newFront.length || !this.newBack.length){
        this.error = true;
      } else {
        this.cards.push({
          front: this.newFront,
          back: this.newBack,
          flipped: false
        });
        this.newFront = '';
        this.newBack = '';
        this.error = false;
      }
    }
  },

  components: {
    CardInputForm
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

body {
  font-family: 'Montserrat', sans-serif;
  text-align: center;
}

ul {
  padding-left: 0;
  display: flex;
  flex-flow: row wrap;
}

li {
  list-style-type: none;
  padding: 10px 10px;
  transition: all 0.3s ease;
}

.container {
  max-width: 100%;
  padding: 2em;
}

.card {
  display: block;
  width: 150px;
  height: 175px;
  padding: 80px 50px;
  background-color: #51aae5;
  border-radius: 7px;
  margin: 5px;
  text-align: center;
  line-height: 27px;
  cursor: pointer;
  position: relative;
  color: #fff;
  font-weight: 600;
  font-size: 20px;
  -webkit-box-shadow: 9px 10px 22px -8px rgba(209,193,209,.5);
  -moz-box-shadow: 9px 10px 22px -8px rgba(209,193,209,.5);
  box-shadow: 9px 10px 22px -8px rgba(209,193,209,.5);
  will-change: transform;
}

li:hover{
  transform: scale(1.1);
}

li:nth-child(-n+3) .card{
  background-color: #e65f51;
}

li:nth-child(2n+1) .card{
  background-color: #a17de9;
}

li:nth-child(4n) .card{
  background-color: #feca34;
}

li:nth-child(5n-2) .card{
  background-color: #51aae5;
}

li:nth-child(4n+4) .card{
  background-color: #feca34;
}

li:nth-child(-7n+7) .card{
  background-color: #e46055;
}

.delete-card {
  position: absolute;
  right: 0;
  top: 0;
  padding: 10px 15px;
  opacity: .4;
  transition: all 0.5s ease;
}

.delete-card:hover, .error {
  opacity: 1;
  transform: rotate(360deg);
}

.flip-enter-active {
  transition: all 0.4s ease;
}

.flip-leave-active {
  display: none;
}

.flip-enter, .flip-leave {
  transform: rotateY(180deg);
  opacity: 0;

}

/* Form */
.card-input-form{
  position: relative;
}


label{
  font-weight: 400;
  color: #333;
  margin-right: 10px;
}

input{
  border-radius: 5px;
  border: 2px solid #eaeaea;
  padding: 10px;
  outline: none;
}

button{
  border-radius: 5px;
  border: 1px solid #87cb84;
  background-color: #87cb84;
  padding: 8px 15px;
  outline: none;
  font-size: 14px;
  font-weight: 700;
  color: #fff;
  cursor: pointer;
  transition: all 0.3s ease;
}

button:hover{
  background-color: #70a66f;
}

.error{
  margin-top: 10px;
  display: block;
  color: #e44e42;
  font-weight: 600;
}

</style>
