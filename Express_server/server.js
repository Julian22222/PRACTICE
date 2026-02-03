const express = require("express");
const app = express();

app.get("/", (req, res) => {
  res.status(200).send("my Home Page .........."); // can write status code but not mendatory
});

app.get("/users", (req, res) => {
  res.send("All users");
});
//

app.listen(3000, (err) => {
  err ? console.log(err) : console.log("Server is listening on Port 3000");
});

//To run this file
// npm run dev
//node server.js
//load http://localhost:3000/ in a browser to see the output.
