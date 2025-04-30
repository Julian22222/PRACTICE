const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const movieSchema = new Schema({
  title: {
    type: String,
    required: true,
  },
  director: {
    type: String,
    required: true,
  },
  year: {
    type: Number,
    required: true,
  },
  genres: [String],
  rating: Number,
  duration: {
    hours: Number,
    minutes: Number,
  },
  reviews: [
    {
      name: String,
      text: String,
    },
  ],
  votes: {
    type: Number,
    required: false,
  },
});

const Movie = mongoose.model("Movie", movieSchema);
// const Movie is a model which represents a class, trefore it is Capital
// ("Movie")- name of the model
// movieSchema - name of the Schema (must be a movie coz mongoose will search-
// -in database collection with plural name from movie) - collection name in mongoDB -is movies

// document manipulation from database is happening because of model
module.exports = Movie;
