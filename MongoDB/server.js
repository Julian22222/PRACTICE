const express = require("express");
const mongoose = require("mongoose");
const Movie = require("./models/movie");
const movieRoutes = require("./routes/movie-routes");

const PORT = 3000;
// this URL we use without hosting
const URL = "mongodb://localhost:27017/moviebox";
// const URL =
//   "mongodb+srv://julikgolovenj:Julik123@cluster0.rmfxbzb.mongodb.net/moviebox?retryWrites=true&w=majority";
// localhost :27017from mongodb compass / moviebox -our Database
//
const app = express();

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
