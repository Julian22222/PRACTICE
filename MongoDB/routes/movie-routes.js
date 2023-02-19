const express = require("express");

const router = express.Router();

const {
  getMovies,
  getMovie,
  deleteMovie,
  addMovie,
  updateMovie,
} = require("../controllers/movie-controller");

router.get("/", (req, res) => {
  console.log("Yoo");
});

router.get("/movies", getMovies);
// db - hold the data from Database in successesful connection to Database
router.get("/movies/:id", getMovie);
router.delete("/movies/:id", deleteMovie);
router.post("/movies", addMovie);
router.patch("/movies/:id", updateMovie);

module.exports = router;
