<template>
  <div>
    <ul>
      <!-- v-bind:todo="todo" passing todo to TodoItem component as props -->
      <TodoItem
        v-for="(todo, i) in todos"
        v-bind:todo="todo"
        v-bind:index="i"
        v-on:remove-todo="removeTodo"
      />
    </ul>
  </div>
</template>

<script>
import App from "@/App.vue";
import TodoItem from "@/components/TodoItem.vue";
export default {
  // props: ["todos"] - unpacking props from App.vue to use them in this component
  props: ["todos"],
  components: {
    TodoItem,
  },
  methods: {
    removeTodo(id) {
      console.log(id);
      this.$emit("remove-todo", id);
    },
  },
};
</script>

<!-- scoped atribute will allow these styles to be visible only inside this component -->
<style scoped>
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}
</style>
