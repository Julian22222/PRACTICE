```JS
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
//bad example i use async await together with callbacks
app.get("/", async (req, res) => {  //<--async
  try {
    pool.query(
      "SELECT * FROM cars INNER JOIN phoneNumbers ON ph_id = car_id",
      (err, result, fields) => {  //<-- callbacks
        // result -is our query (data from database), err - any error while connecting to our database
        if (err) {
          res.send(err);
        }
        if (!result || result.length === 0) {
          res.send("No data found in database");
        }

        if (result) {
          //converting database -serviceCheck property from 1/0 to true/false
          //MySQL, SQLite, SQL Server, Oracle stores boolean values as 1 (true) and 0 (false) under the hood.
          const formattedResult = result.map((row) => ({
            ...row,
            serviceCheck: Boolean(row.serviceCheck), // Convert 1/0 to true/false
          }));

          res.send(formattedResult);
          // res.json(formattedResult);
        }
      }
    );
  } catch (err) {
    console.log("error:", err);
  }
});

////////////////////////////////////////////////////////////////////////////GET ID

app.get("/:carId", async (req, res) => {
  try {
    //get ID from request body, always comes as a string
    const { carId } = req.params;

    console.log(carId);
    console.log(typeof carId);
    //isNaN checks if the value is Not-a-Number, automatically convert the string to a number
    // If carId is NaN, then isNaN(carId) returns true
    if (isNaN(carId)) {
      return res.status(400).send("Wrong card Id has been inserted.");
    }

    // find if the ID exists in the database
    pool.query(
      `SELECT cars.car_id,cars.brand,cars.seats,cars.date,cars.fuel,
      cars.created_at,cars.serviceCheck,cars.involved,cars.notes,phoneNumbers.phone
      FROM cars INNER JOIN phoneNumbers ON ph_id = car_id WHERE car_id ='` +
        carId +
        "'",
      // "SELECT * FROM cars WHERE car_id = $1",[carId],
      (err, result) => {
        // result -is our query, err - if can't connect to our database
        if (err) {
          res.status(500).send("error", err.message);
        }

        // console.log(result);

        //if carId doesn't exist in database -> the result.length === 0
        if (result.length === 0) {
          res.status(404).send("Car Id Not Found");
        }

        if (result.length > 0) {
          // res.send(result);

          //converting database -serviceCheck property from 1/0 to true/false
          const formattedResult = result.map((row) => ({
            ...row,
            serviceCheck: Boolean(row.serviceCheck), // Convert 1/0 to true/false
          }));

          // console.log(formattedResult[0]);

          res.json(formattedResult[0]); // send only one object, not an array
        }
      }
    );
    // });
  } catch (err) {
    console.log("error:", err);
  }
});

////////////////////////////////////////////////////////////////POST

app.post("/", async (req, res) => {
  // console.log(req.body);

  try {
    const { brand, seats, date, fuel, serviceCheck, involved, notes, phone } =
      req.body;

    // error handling, when posting but brand or /and seats field is empty
    if (req.body.brand === undefined) {
      res.status(400).send("Wrong Brand Input");
    } else if (req.body.seats === undefined) {
      res.send("Wrong Seats Input");
    }

    pool.query(
      `INSERT INTO cars(brand,seats, date, fuel, serviceCheck, involved, notes) VALUES
        (?, ?, ?, ?, ?, ?, ?)`, /// Use ?, ? placeholders for parameterized queries, In PostgreSQL we use $1, $2 — but that's not valid for MySQL
      [brand, seats, date, fuel, serviceCheck, involved, notes],
      (err1, result1) => {
        console.log("result1", result1);
        if (err1) {
          return res.status(500).send(err1);
        }

        const car_id = result1.insertId; // Get the inserted car_id from result1, if the query was successful

        pool.query(
          `INSERT INTO phoneNumbers (ph_id, phone) VALUES
            (?, ?)`, // Use ?, ? placeholders for parameterized queries, In PostgreSQL we use $1, $2 — but that's not valid for MySQL
          [car_id, phone],
          (err2, result2) => {
            if (err2) {
              return res.status(500).send(err2);
            }

            res.status(201).send("Data inserted into both tables successfully");
          }
        );
      }
    );
  } catch (err) {
    console.log("error:", err);
  }
});
////////////////////////////////////////////////////////////////////////UPDATE
app.put("/:carId", async (req, res) => {
  try {
    // Get ID from request body
    const { carId } = req.params;
    const { brand, seats, year, fuel } = req.body;

    //  Find if the ID exists in the database
    const item = await pool.query((err) => {
      pool.query(
        `SELECT * FROM cars WHERE car_id ='` + carId + "'",
        // if carId doesn't exist -< err
        (err, result) => {
          if (err) {
            res.status(500).json("error", err.message);
          }

          //   if cardID exists we can make an update
          if (result && result.length) {
            pool.query(
              `UPDATE cars SET brand = '${brand}', seats = '${seats}', year = '${year}', fuel = '${fuel}' WHERE car_id ='` +
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
    console.log("error:", err);
  }
});

////////////////////////////////////////////////////////////////////////DELETE

app.delete("/:carId", async (req, res) => {
  const { carId } = req.params;
  console.log(carId);

  pool.query(
    `DELETE FROM phoneNumbers WHERE ph_id = ${carId}`,
    (err1, result1) => {
      // console.log("result1", result1);
      if (err1) return res.status(500).json({ error: err1.message });
      if (result1.affectedRows > 0) {
        console.log("Phone number deleted successfully");
      } else {
        console.log("No phone number found for this car ID");
      }
    }
  );

  pool.query(`DELETE FROM cars WHERE car_id = ${carId}`, (err2, result2) => {
    if (err2) return res.status(500).json("error:", err2.message);
    if (result2.affectedRows > 0) {
      console.log("Car deleted successfully");
      return res.status(204).send("Data Deleted Successfully from both tables");
    }
    if (!result2 || result2.length === 0) {
      return res.status(404).send("Car Id not found");
    }
  });

  //////////////////////////////////////////////////////////////////////////////////////////
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
```

```JS
Error handler
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
```
