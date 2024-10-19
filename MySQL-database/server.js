const app = require("./app"); //Import app.js file here
const PORT = 9070;

app.listen(PORT, (err) => {
  err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`);
});
