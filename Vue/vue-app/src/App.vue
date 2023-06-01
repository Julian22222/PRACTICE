<template>
  <div class="app">
    <div class="post">
      <h1>Todo application</h1>
      <hr />

      <!-- <div>{{ post.body }}</div> -->
    </div>
    <br />
    <AddTodo @add-todo="addTodo" />
    <br />
    <br />
    <!-- v-bind:todos="todos" passing todo to TodoList component as props -->
    <TodoList v-bind:todos="todos" v-on:remove-todo="removeTodo" />

    <!-- <form>
      <input
        v-bind:value="title"
        @input="title = $event.target.value"
        class="input"
        type="text"
        placeholder="insert your nickname"
      />
      <input
        v-bind:value="body"
        class="input"
        type="text"
        placeholder="insert your comment"
      />
      <button class="btn" @click="createComment">Search</button>
    </form> -->
    <!-- <div> -->
    <!--or  <button v-on:click="addLike"></button> -->
    <!-- <button @click="addLike">Like</button>
      <button @click="addDislike">Dislike</button>
    </div>
    <div>
      Likes: <strong>{{ likes }}</strong>
    </div>
    <div>
      Dislikes: <strong>{{ dislikes }}</strong>
    </div> -->
  </div>
</template>

<script>
import TodoList from "@/components/TodoList.vue";
import AddTodo from "@/components/AddTodo.vue";
// export default {
//   data() {
//     return {
//       likes: 0,
//       dislikes: 0,
//     };
//   },
//   methods: {
//     addLike() {
//       this.likes += 1;
//     },
//     addDislike() {
//       this.dislikes += 1;
//     },
//   },
// };
export default {
  components: {
    TodoList,
    AddTodo,
  },

  data() {
    return {
      posts: [
        { id: 1, title: "JavaScript", body: "description of the post" },
        { id: 2, title: "JavaScript 2", body: "description of the post 2" },
        { id: 3, title: "JavaScript 3", body: "description of the post 3" },
      ],
      title: "",
      body: "",
      todos: [
        { id: 1, title: "buy groceries", completed: false },
        { id: 2, title: "studing", completed: false },
        { id: 3, title: "wash car", completed: false },
      ],
    };
  },
  methods: {
    // searchMovie() {},
    // createComment() {},
    // inputTitle(event) {
    //   console.log(event);
    // },
    removeTodo(id) {
      this.todos = this.todos.filter((e) => e.id !== id);
    },
    addTodo(todo) {
      this.todos.push(todo);
    },
  },
};
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

.app {
  padding: 20px;
}

.post {
  padding: 15px;
  border: 2px solid teal;
}

form {
  display: flex;
  flex-direction: column;
}

.input {
  width: 100%;
  border: 1px solid teal;
  padding: 10px 15px;
  margin-top: 15px;
}

.btn {
  margin-top: 15px;
  align-self: flex-end;
  padding: 10px 15px;
  background: none;
  color: teal;
  border: 1px solid teal;
}
</style>

//Single file component
