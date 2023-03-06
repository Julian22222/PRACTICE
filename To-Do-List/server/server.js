const express = require("express");
const app = express();
const cors = require("cors");
const pool = require("./db");
// { v4: uuidv4 } - make a uniq id
// const { v4: uuidv4 } = require("uuid");

// if process.env.PORT doesn't exist we will use Port 9090
const PORT = process.env.PORT || 9090;

// is the same - if process.env.PORT doesn't exist we will use Port 9090
// const Port = process.env.PORT ?? 9090;

app.use(cors());
app.use(express.json());

app.get("/todos/:userEmail", async (req, res) => {
  //   const userEmail = "julian@test.com";

  const { userEmail } = req.params;

  try {
    const todos = await pool.query(
      "SELECT * FROM todos WHERE user_email = $1",
      [userEmail]
    );
    res.json(todos.rows);
  } catch (err) {
    console.error(err);
  }
});

// create a new todo
app.post("/todos", async (req, res) => {
  const { user_email, title, progress, date } = req.body;
  //   uuidv4() make new uniq id,
  //   uuidv4();
  console.log(user_email, title, progress, date);
  try {
    const newToDo = await pool.query(
      `INSERT INTO todos(user_email,title,progress,date) VALUES($1,$2,$3,$4)`,
      [user_email, title, progress, date]
    );
    res.json(newToDo);
  } catch (err) {
    console.log(err);
  }
});

// edit a todo
app.put("/todos/:todo_id", async (req, res) => {
  const { todo_id } = req.params;
  console.log(req.params);
  const { user_email, title, progress, date } = req.body;
  try {
    const editToDo = await pool.query(
      `UPDATE todos SET user_email = $1, title = $2, progress = $3, date = $4 WHERE todo_id = $5;`,
      [user_email, title, progress, date, todo_id]
    );
    res.json(editToDo);
  } catch (err) {
    console.error(err);
  }
});

// delete a to do
app.delete("/todos/:todo_id", async (req, res) => {
  const { todo_id } = req.params;

  try {
    const deleteToDo = await pool.query(
      `DELETE FROM todos WHERE todo_id = $1;`,
      [todo_id]
    );
    res.json(deleteToDo);
  } catch (err) {
    console.error(err);
  }
});

app.listen(PORT, (err) => {
  err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`);
});
