const Pool = require("pg").Pool;
require("dotenv").config();

// this pool will help to comunicate with Database,
// to get all info from database by request
const pool = new Pool({
  // user and password from postgreSQL database
  user: process.env.USERNAME,
  password: process.env.PASSWORD,
  host: process.env.HOST,
  port: process.env.DBPORT,
  // database - name of database in postgreSQL database
  database: "my_bookshop",
});

module.exports = pool;
