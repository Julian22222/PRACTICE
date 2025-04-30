const express = require("express");

const router = express.Router();

const {
  getMovies,
  getMovie,
  deleteMovie,
  addMovie,
  updateMovie,
} = require("../controllers/movie-controller");

const {
  getUsers,
  getSingleUser,
  addUser,
  updateUser,
  deleteUser,
} = require("../controllers/user-controller");

router.get("/", (req, res) => {
  // console.log("Yoo");
  res.send({
    getAllMovies: "https://movies-ypff.onrender.com/movies",
    getMovieById: "https://movies-ypff.onrender.com/movies/:id",
    deleteMovieById: "https://movies-ypff.onrender.com/movies/:id",
    postMovie: "https://movies-ypff.onrender.com/movies",
    updateMovieById: "https://movies-ypff.onrender.com//movies/:id",
    ////////
    getAllUsers: "https://movies-ypff.onrender.com/users",
    getUserById: "https://movies-ypff.onrender.com/users/:id",
    postUser: "https://movies-ypff.onrender.com/users",
    updateUserById: "https://movies-ypff.onrender.com//users/:id",
    deleteUserById: "https://movies-ypff.onrender.com/users/:id",
  });
});

router.get("/movies", getMovies);
// db - hold the data from Database in successesful connection to Database
router.get("/movies/:id", getMovie);
router.delete("/movies/:id", deleteMovie);
router.post("/movies", addMovie);
router.patch("/movies/:id", updateMovie);

router.get("/users", getUsers);
router.get("/users/:id", getSingleUser);
router.post("/users", addUser);
router.patch("/users/:id", updateUser);
router.delete("/users/:id", deleteUser);

module.exports = router;
