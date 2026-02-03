const express = require("express");
const mongoose = require("mongoose");
const cors = require("cors");
const Movie = require("./models/movie");
const movieRoutes = require("./routes/movie-routes");
require("dotenv").config();

const PORT = process.env.PORT;
// this URL we use without hosting
// localhost :27017 - from mongodb compass / moviebox -our Database
// const URL = "mongodb://localhost:27017/moviebox";

const URL = `mongodb+srv://${process.env.DB_USERNAME}:${process.env.PASSWORD}@cluster0.rmfxbzb.mongodb.net/${process.env.DATABASE}?retryWrites=true&w=majority`;

const app = express();

app.use(cors());

app.use(express.json());
app.use(movieRoutes);

mongoose
  .connect(URL, { useNewUrlParser: true, useUnifiedTopology: true })
  .then((res) => {
    console.log("Connected to MongoDB");
  })
  .catch((err) => {
    console.log(`DB connection error: ${err}`);
  });

app.listen(PORT, (err) => {
  err ? console.log(err) : console.log(`Server is listening on port ${PORT}`);
});

module.exports = app;
