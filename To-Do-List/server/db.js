const Pool = require("pg").Pool;
require("dotenv").config();

// this pool will help to comunicate with Database,
// to get all info from database by request
const pool = new Pool({
  // ///////////// user and password from local postgreSQL database
  // user: process.env.USERNAME,
  // password: process.env.PASSWORD,
  // host: process.env.HOST,
  // port: process.env.DBPORT,
  // // database - name of database in local postgreSQL database
  // database: process.env.PGDATABASE,

  //////////////// connection to hosted database (node pg pool connection string)
  connectionString: process.env.HOST,
});

module.exports = pool;
