const express = require("express");

const app = express();
const cors = require("cors");

// import our database connection
const pool = require("./db");
const { Console } = require("console");

app.use(cors());
app.use(express.json()); //to be able to send json body in POST and PUT methods to database

// const PORT = 9080;

///////////////////////////////////////////////////////////////////////GET

app.get("/", async (req, res, next) => {
  try {
    // await pool.query((err) => {
    await pool.query(
      "SELECT * FROM cars INNER JOIN phoneNumbers ON ph_id = car_id",
      (err, result, fields) => {
        // result -is our query (data from database), err - any error while connecting to our database
        if (err) {
          res.send(err);
        }
        if (result) {
          res.send(result);
        }
      }
    );
    // });
  } catch (err) {
    next(err);
  }
});

////////////////////////////////////////////////////////////////////////////GET ID

app.get("/:carId", async (req, res, next) => {
  try {
    //get ID from request body
    const { carId } = req.params;

    // console.log(carId);
    // console.log(typeof carId);

    // console.log(!isNaN(carId));

    if (!isNaN(carId) === false) {
      return res.status(400).send("Wrong card Id has been inserted.");
    }

    // if (Number(carId) === NaN) {
    //   // return Promise.reject({
    //   //   status: 400,
    //   //   message: "Wrong card Id has been inserted.",
    //   // });
    //   res.status(400).send("Wrong card Id has been inserted.");
    // }

    // find if the ID exists in the database
    // const item = await pool.query((err) => {
    await pool.query(
      `SELECT cars.car_id,cars.brand,cars.seats,cars.date,cars.fuel,
      cars.created_at,cars.serviceCheck,cars.involved,cars.notes,phoneNumbers.phone 
      FROM cars INNER JOIN phoneNumbers ON ph_id = car_id WHERE car_id ='` +
        carId +
        "'",
      // "SELECT * FROM cars WHERE car_id = $1",
      // [carId],
      (err, result, fields) => {
        // result -is our query, err - if can't connect to our database
        if (err) {
          res.status(400).send(err);
        }

        // console.log(result);

        //   if carId doesn't exist in database -> the result.length === 0
        if (result.length === 0) {
          res.status(404).send("Car Id Not Found");
          // return Promise.reject({ status: 404, msg: "Id not found" });
        }

        if (result.length > 0) {
          res.send(result);
        }
      }
    );
    // });
  } catch (err) {
    next(err);
  }
});
////////////////////////////////////////////////////////////////POST
app.post("/", async (req, res, next) => {
  // console.log(req.body);

  try {
    // error handling, when posting but brand or /and seats field is empty
    if (req.body.brand === undefined) {
      res.status(400).send("Wrong Brand Input");
    } else if (req.body.seats === undefined) {
      res.send("Wrong Seats Input");
    } else {
      // if all field correctly ,send a req to database
      // await pool.query((err) => {
      //   if (err) {
      //     res.send(err);
      //   }

      pool.query(
        `INSERT INTO cars(brand,seats, date, fuel, serviceCheck, involved, notes) VALUES
        ('${req.body.brand}','${req.body.seats}','${req.body.date}','${req.body.fuel}, ${req.body.serviceCheck}, ${req.body.involved}, '${req.body.notes}')
        `,
        (err, result, fields) => {
          if (err) {
            res.send(err);
          }

          if (result) {
            res.status(201).send("Data Inserted Successfully");
          }

          if (fields) {
            console.log(fields);
          }
        }
      );
      // });
    }
  } catch (err) {
    next(err);
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
                  res.status(204).send("Data Updated Successfully");
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
  // try {
  // Get ID from request body
  const { carId } = req.params;

  //  Find if the ID exists in the database or not
  // const item = await pool.query((err) => {
  // pool.query(
  //   `SELECT * FROM cars WHERE car_id = '` + carId + "'",
  //   (err, result, fields) => {
  //     if (err) {
  //       res.send(err);
  //     }

  //   if ID exists in database
  // if (result && result.length) {

  /////////////////////////////////////////////////////////////////////////
  // pool.query(
  //   `DELETE FROM cars WHERE car_id = '${carId}'`,
  //   (err1, result1, fields1) => {
  //     if (err1) {
  //       res.send(err1);
  //     }

  //     if (result1.length === 0) {
  //       res.status(400).send("Wrong card Id has been inserted.");
  //     }

  //     if (result1.length > 0) {
  //       res.status(204).send("Data Deleted Successfully");
  //     }

  //     if (fields1) {
  //       console.log(fields1);
  //     }
  //   }
  // );

  console.log(carId);

  await pool.query(
    `DELETE FROM cars WHERE car_id = '${carId}'`,
    (err, result, fields) => {
      // console.log("Console from app.js", result.affectedRows);

      if (result.affectedRows > 0) {
        return res.status(204).send();
      }

      return res.status(404).send("Car Id not found");
    }
  );

  //////////////////////////////////////////////////////////////////////////////////////////

  // }

  // else {
  //   res.send("Record not found.");
  // }
  // }
  // );
  // });
  // } catch (error) {
  //   next(error);
  // }
});

// Error handler
app.use((err, req, res, next) => {
  if (err.status && err.msg) {
    res.status(err.status).send({ message: err.msg });
  } else {
    next(err);
  }

  console.log(err.stack);
  console.log(err.name);
  console.log(err.code);
});

app.use((err, req, res, next) => {
  res.status(500).json({
    message: "Something went wrong",
  });
});

module.exports = app;
