const app = require("./app"); //Import app.js file here

const PORT = 9080;

app.listen(PORT, "0.0.0.0", (err) => {
  err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`);
});
