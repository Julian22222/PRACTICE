<template>
  <form @submit.prevent="onSubmit">
    <input
      type="text"
      class="input"
      placeholder="insert new todo"
      v-model="title"
    />
    <button type="submit" class="btn" @click="AddTodo">Create</button>
  </form>
</template>

<script>
export default {
  data() {
    return {
      title: "",
    };
  },
  methods: {
    onSubmit() {
      console.log("Submit", this.title);
      if (this.title.trim()) {
        const newTodo = {
          id: Date.now(),
          title: this.title,
          completed: false,
        };

        this.$emit("add-todo", newTodo);
        this.title = "";
      }
    },
  },
};
</script>

<style scoped>
form {
  display: flex;
}
</style>
