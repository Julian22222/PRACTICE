const express = require("express");

const app = express();
const cors = require("cors");

// import our database connection
const pool = require("./db");

app.use(cors());
app.use(express.json());

const PORT = 9080;

///////////////////////////////////////////////////////////////////////GET

app.get("/", async (req, res, next) => {
  await pool.query((err) => {
    pool.query("SELECT * FROM cars", (err, result, fields) => {
      if (err) {
        res.send(err);
      }
      if (result) {
        res.send(result);
      }
    });
  });
});

////////////////////////////////////////////////////////////////////////////GET ID

app.get("/:carId", async (req, res, next) => {
  try {
    //get ID from request body
    const { carId } = req.params;

    // find if the ID exists in the database
    const item = await pool.query((err) => {
      pool.query(
        `SELECT * FROM cars WHERE car_id ='` + carId + "'",
        // "SELECT * FROM cars WHERE car_id = $1",
        // [carId],
        (err, result, fields) => {
          if (err) {
            res.send(err);
          }

          if (result.length === 0) {
            res.send("Wrong card Id has been inserted.");
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

//////////////////////////////////////////////////////////////////////////////////////////

app.listen(PORT, (err) => {
  err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`);
});
