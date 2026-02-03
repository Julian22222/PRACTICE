const App = {
  // data <-is method , which return an object
  //   notes , title,placeholderString ... inside the data<- they are called models
  data() {
    return {
      title: "To do list",
      placeholderString: "insert your note",
      inputValue: "",
      notes: ["Note 1", "Note 2"],
    };
  },
  //   methods is an object where you list functions and methods for our app
  methods: {
    // inputChangeHandler(event) {
    //   //   two side binding , binding input with our inputValue variable
    //   this.inputValue = event.target.value;
    // },
    addNewNote() {
      // add new note only when inputValue is not an empty string
      if (this.inputValue !== "") {
        // we pushing our input to the existing array
        this.notes.push(this.inputValue);
        //   make empty input after clicking the button Add
        this.inputValue = "";
      }
    },
    // inputKeyPress(event) {
    //   //   console.log(event.key); <--shows what key on your keyboard you press
    //   if (event.key === "Enter") {
    //     this.addNewNote();
    //   }
    // },
    toUpperCase(item) {
      return item.toUpperCase();
    },
    removeNote(index) {
      //   console.log(index);
      this.notes.splice(index, 1);
    },
  },
  //   computed is an object ,that behaves as getters and setters,-->
  // -->therefore we don't invoke computed functions, when we need to invoke this function --> we put -doubleCounted , without brackets -->(doubleCounted())
  //   computed must be dependant from variables (use variables) that we have created in app.js (notes -for instance)
  computed: {
    // doubleCounted(){
    //     return this.notes.length *2
    // }
  },
  //   watch tracks all variables that we need
  watch: {
    inputValue(value) {
      console.log("inputValue", value);
      //   if (value.length > 10) {
      //     this.inputValue = "";
      //   }
    },
  },
};

// connects vue data method with our project in index.html - intrapolation
Vue.createApp(App).mount("#app");
