const express = require("express");

const app = express();
const cors = require("cors");

// import our database connection
const pool = require("./db");

app.use(cors());
app.use(express.json()); //to be able to send json body in POST and PUT methods to database

// const PORT = 9080;

///////////////////////////////////////////////////////////////////////GET

app.get("/", async (req, res, next) => {
  try {
    await pool.query((err) => {
      pool.query("SELECT * FROM cars", (err, result, fields) => {
        // result -is our query (data from database), err - any error while connecting to our database
        if (err) {
          res.send(err);
        }
        if (result) {
          res.send(result);
        }
      });
    });
  } catch (err) {
    next(err);
  }
});

////////////////////////////////////////////////////////////////////////////GET ID

app.get("/:carId", async (req, res, next) => {
  try {
    //get ID from request body
    const { carId } = req.params;

    if (!carId) {
      return Promise.reject({
        status: 400,
        message: "Bad request, invalid car id",
      });
    }

    // find if the ID exists in the database
    const item = await pool.query((err) => {
      pool.query(
        `SELECT * FROM cars WHERE car_id ='` + carId + "'",
        // "SELECT * FROM cars WHERE car_id = $1",
        // [carId],
        (err, result, fields) => {
          // result -is our query, err - if can't connect to our database
          if (err) {
            res.status(400).send(err);
          }

          //   if carId doesn't exist in database -> the result.length === 0
          if (result.length === 0) {
            res.status(404).send("Wrong card Id has been inserted.");
            // return Promise.reject({ status: 404, msg: "Id not found" });
          }

          if (result.length > 0) {
            res.send(result);
          }
        }
      );
    });
  } catch (error) {
    next(error);
  }
});
////////////////////////////////////////////////////////////////POST
app.post("/", async (req, res, next) => {
  // error handling, when posting but brand or /and seats field is empty
  if (req.body.brand === undefined) {
    res.send("Wrong Brand Input");
  } else if (req.body.seats === undefined) {
    res.send("Wrong Seats Input");
  } else {
    // if all field correctly ,send a req to database
    await pool.query((err) => {
      pool.query(
        `INSERT INTO cars(brand,seats, year, fuel) VALUES
        ('${req.body.brand}','${req.body.seats}','${req.body.year}','${req.body.fuel}')
        `,
        (err, result, fields) => {
          if (err) {
            res.send(err);
          }

          if (result) {
            res.send("Data Inserted Successfully");
          }

          if (fields) {
            console.log(fields);
          }
        }
      );
    });
  }
});
////////////////////////////////////////////////////////////////////////UPDATE
app.put("/:carId", async (req, res, next) => {
  try {
    // Get ID from request body
    const { carId } = req.params;

    //  Find if the ID exists in the database
    const item = await pool.query((err) => {
      pool.query(
        `SELECT * FROM cars WHERE car_id ='` + carId + "'",
        // if carId doesn't exist -< err
        (err, result, fields) => {
          if (err) {
            res.send(err);
          }

          //   if cardID exists we can make an update
          if (result && result.length) {
            pool.query(
              `UPDATE cars SET brand = '${req.body.brand}', seats = '${req.body.seats}', year = '${req.body.year}', fuel = '${req.body.fuel}' WHERE car_id ='` +
                carId +
                "'",
              (err1, result1, fields1) => {
                if (err1) {
                  res.send(err1);
                }

                if (result1) {
                  res.send("Data Updated Successfully");
                }

                if (fields1) {
                  console.log(fields1);
                }
              }
            );
          } else {
            res.send("Record not found.");
          }
        }
      );
    });
  } catch (error) {
    next(error);
  }
});

////////////////////////////////////////////////////////////////////////DELETE

app.delete("/:carId", async (req, res, next) => {
  try {
    // Get ID from request body
    const { carId } = req.params;

    //  Find if the ID exists in the database or not
    const item = await pool.query((err) => {
      pool.query(
        `SELECT * FROM cars WHERE car_id = '` + carId + "'",
        (err, result, fields) => {
          if (err) {
            res.send(err);
          }

          //   if ID exists in database
          if (result && result.length) {
            pool.query(
              `DELETE FROM cars WHERE car_id = '${carId}'`,
              (err1, result1, fields1) => {
                if (err1) {
                  res.send(err1);
                }

                if (result1.length === 0) {
                  res.send("Wrong card Id has been inserted.");
                }

                if (result1.length > 0) {
                  res.send("Data Deleted Successfully");
                }

                if (fields1) {
                  console.log(fields1);
                }
              }
            );
          } else {
            res.send("Record not found.");
          }
        }
      );
    });
  } catch (error) {
    next(error);
  }
});

// Error handler
app.use((err, req, res, next) => {
  console.log(err.stack);
  console.log(err.name);
  console.log(err.code);

  res.status(500).json({
    message: "Something went wrong",
  });
});

module.exports = app;
