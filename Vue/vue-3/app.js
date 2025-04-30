// data <-is method , which return an object
//data in this example is arrow function, -no need return
//   notes , title,placeholderString ... inside the data<- they are called models

// connects vue data method with our project in index.html - intrapolation
Vue.createApp({
  data: () => ({
    myHtml: "<h1>Vue 3 App</h1>",
    title: "Header",
    person: {
      firstName: "Peter",
      lastName: "Scott",
      age: 27,
    },
    items: [1, 2, 3, 4, 5, 6],
  }),
  computed: {
    evenItems() {
      return this.items.filter((i) => i % 2 === 0);
    },
  },
}).mount("#app");
