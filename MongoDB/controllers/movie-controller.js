const Movie = require("../models/movie");

const handleError = (res, error) => {
  // to make our code DRY, res.status(500).json({ error: "Something went wrong ..." })
  // is the same in every route
  res.status(500).json({ error });
};

const getMovies = (req, res) => {
  console.log("Hello");
  // Movie is a model -imported from movie
  Movie.find()
    .sort({ title: 1 })
    .then((movies) => {
      res.status(200).json(movies);
      //   db.collection("movies").find() method doesn't return all movies, it returns -cursor object(it is a data set from Database)
      //   cursor has some methods( hasNext,next,forEach)
    })
    .catch((err) => {
      res.status(500).json({ error: "Something went wrong ..." });
    });
};

const getMovie = (req, res) => {
  Movie.findById(req.params.id)
    .then((movie) => {
      res.status(200).json(movie);
      //   db.collection("movies").find() method doesn't return all movies, it returns -cursor object(it is a data set from Database)
      //   cursor has some methods( hasNext,next,forEach)
    })
    .catch(() => {
      res.status(500).json({ error: "Something went wrong ..." });
      //   this errorhandling doesn't show correct and exact error to the user, need to use err -handleError(res, err);
    });
};

const deleteMovie = (req, res) => {
  Movie.findByIdAndDelete(req.params.id)
    .then((result) => {
      res.status(200).json(result);
      //   db.collection("movies").find() method doesn't return all movies, it returns -cursor object(it is a data set from Database)
      //   cursor has some methods( hasNext,next,forEach)
    })
    .catch((err) => {
      // we can replace all catch methods with error 500 on this function below
      // to make code DRY
      handleError(res, err);
      //   as a response will show an err
    });
};

const addMovie = (req, res) => {
  const movie = new Movie(req.body);

  movie
    .save()
    .then((result) => {
      res.status(201).json(result);
    })
    .catch((err) => {
      console.log(err);
      handleError(res, err);
    });
};

const updateMovie = (req, res) => {
  // console.log(req.body);
  Movie.findByIdAndUpdate(req.params.id, req.body)
    .then((result) => {
      res.status(200).json(result);
    })
    .catch((err) => {
      handleError(res, err);
    });
};

module.exports = { getMovies, getMovie, deleteMovie, addMovie, updateMovie };
