const mysql = require("mysql");
require("dotenv").config();

///////////////////////////////////////is the same -
// const pool = mysql.createConnection({
// connectionLimit:10,
//   //   host: process.env.HOST,
//   //   user: process.env.USER,
//   //   password: process.env.PASSWORD,
//   //   database: process.env.DB,
// })

const pool = mysql.createPool({
  host: process.env.DB_HOST,
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_DATABASE,

  //   how many connections do you want by default , max number of connections it will allow you to create
  //   max: 20
});

// connection to the pool database
// pool.connect((err,connction)=>{})  <-the same

pool.getConnection((err, connection) => {
  if (err) {
    if (err.code === "PROTOCOL_CONNECTION_LOST") {
      console.error("Database connection was closed.");
    }

    if (err.code === "ER_CON_COUNT_ERROR") {
      console.error("Database has too many connections.");
    }

    if (err.code === "ECONNREFUSED") {
      console.error("Database connection was refused.");
    }
  }

  if (connection) {
    console.log("Pool Connected!");
    connection.release();
    return;
  }
});

// if pool connection was successful we export pool
module.exports = pool;
