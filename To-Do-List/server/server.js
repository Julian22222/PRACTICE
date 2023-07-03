const express = require("express");
const app = express();
const cors = require("cors");
const pool = require("./db");
// { v4: uuidv4 } - make a uniq id
// const { v4: uuidv4 } = require("uuid");

// const bcrypt = require("bcrypt");
// to hash the password

const jwt = require("jsonwebtoken");
// to get the authentication token to allow to sign up to the app

// if process.env.PORT doesn't exist we will use Port 9090
const PORT = process.env.PORT || 9090;

// is the same - if process.env.PORT doesn't exist we will use Port 9090
// const Port = process.env.PORT ?? 9090;

app.use(cors());
app.use(express.json());

// app.get("/", (req, res) => {
//   res.send("hello");
// });

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

  console.log(user_email, title, progress, date);
  try {
    const newToDo = await pool.query(
      `INSERT INTO todos(user_email,title,progress,date) VALUES($1,$2,$3,$4)`,
      [user_email, title, progress, date]
    );
    res.json(newToDo);
  } catch (err) {
    console.error(err);
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
    // res.json() - allow to send our data in json format
    res.json(deleteToDo);
  } catch (err) {
    console.error(err);
  }
});

//////loginExists
app.get("/users", async (req, res) => {
  const { email, password } = req.body;
  console.log(email, password);
  try {
    const exist = await pool.query(
      `INSERT INTO users(email,hashed_password) VALUES($1,$2)`,
      [email, password]
    );

    res.json(exist);
  } catch (err) {
    console.error(err);
  }
});

// // signup
// app.post("/signup", async (req, res) => {
//   const { email, password } = req.body;

//   try {
//     const signUp = await pool.query(
//       `INSERT INTO users (email,hashed_password) VALUES($1, $2)`,
//       [email, hashedPassword]
//     );

//     res.json({ email, token });
//   } catch (err) {
//     console.error(err);
//     if (err) {
//       res.json({ detail: err.detail });
//     }
//   }
// });

// //login
// app.post("/login", async (req, res) => {
//   const { email, password } = req.body;
//   try {
//     const users = await pool.query("SELECT * FROM users WHERE email =$1", [
//       email,
//     ]);

//     if (!users.rows.length) return res.json({ detail: "User does not exist!" });

//     const success = await bcrypt.compare(
//       password,
//       users.rows[0].hashedPassword
//     );
//     const token = jwt.sign({ email }, "secret", { expiresIn: "1hr" });
//     if (success) {
//       res.json({ email: users.rows[0].email, token });
//     } else {
//       res.json({ detail: "Login failed" });
//     }
//   } catch (err) {
//     console.error(err);
//   }
// });

app.listen(PORT, (err) => {
  err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`);
});
