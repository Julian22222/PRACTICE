<template>
  <div>
    <h1>Hello</h1>
    <input type="text" placeholder="search your movie" v-model="search" />
    <div>
      <ul>
        <li v-for="(person, index) in usersList" :key="person">
          ({{ index + 1 }}) .{{ person.name }}
        </li>
      </ul>
    </div>
  </div>
  {{ search }}
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      data: [
        { name: "John", age: 25, text: "aaaaa" },
        { name: "Michael", age: 32, text: "bbbbbb" },
        { name: "Sam", age: 27, text: "cccc" },
      ],
      search: null,
    };
  },
  components: {},
  computed: {
    usersList() {
      if (this.search) {
        return this.data.filter((user) => {
          return this.search
            .toLowerCase()
            .split(" ")
            .every((v) => user.name.toLowerCase().includes(v));
        });
      } else {
        return this.data;
      }

      // // return this.data;
      // if (this.search.trim().length > 0) {
      //   // return [];
      //   return this.data.filter((user) => {
      //     user.name.includes(this.search.trim());
      //   });
      // }
      // return this.data;
    },
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
