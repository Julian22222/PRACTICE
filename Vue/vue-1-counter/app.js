const App = {
  // data <-is method , which return an object
  //   counter , title ... inside the data<- they are called models
  data() {
    return {
      counter: 0,
      title: "To do list",
    };
  },
};

// connects vue data method with our project in index.html - intrapolation
Vue.createApp(App).mount("#app");
