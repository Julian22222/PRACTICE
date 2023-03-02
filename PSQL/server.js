const express = require("express");
const app = express();
const pool = require("./db");

// if process.env.PORT doesn't exist we will use Port 9090
const PORT = process.env.PORT || 9090;

// is the same - if process.env.PORT doesn't exist we will use Port 9090
// const Port = process.env.PORT ?? 9090;

// app.get("/", (req, res) => {
//   res.send("Hello Julian");
// });

app.get("/books", async (req, res) => {
  try {
    const books = await pool.query("SELECT * FROM books_info");
    res.json(books.rows);
  } catch (err) {
    console.error(err);
  }
});

app.get("/auth", async (req, res) => {
  try {
    const auth = await pool.query("SELECT * FROM authors_data");
    res.json(auth.rows);
  } catch (err) {
    console.error(err);
  }
});

app.listen(PORT, (err) => {
  err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`);
});
