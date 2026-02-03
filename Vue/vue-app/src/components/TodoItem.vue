<template>
  <!-- <li>{{ todo.title }}</li> -->
  <li>
    <span v-bind:class="{ done: todo.completed }">
      <input type="checkbox" v-on:change="todo.completed = !todo.completed" />
      <strong>
        {{ index + 1 }}
      </strong>
      {{ todo.title }}
    </span>
    <!-- &times; - is a close button, cross  -->
    <button class="rm" v-on:click="$emit('remove-todo', todo.id)">
      &times;
    </button>
  </li>
</template>

<script>
export default {
  // props: {todo} - unpacking props from TodoList.vue to use them in this component
  props: {
    todo: {
      type: Object,
      required: true,
    },
    index: Number,
  },
};
</script>

<!-- scoped atribute will allow these styles to be visible only inside this component -->
<style scoped>
li {
  border: 1px solid #ccc;
  display: flex;
  justify-content: space-between;
  padding: 0.5rem 2rem;
  margin-bottom: 1rem;
}

.rm {
  background: red;
  color: #fff;
  width: 20px;
  border-radius: 50%;
  font-weight: bold;
}

input {
  margin-right: 1rem;
}
.done {
  text-decoration: line-through;
}
</style>
