const express = require("express");
const cors = require("cors");

const app = express();

// import our database connection
const pool = require("./db");

app.use(cors());
app.use(express.json()); //to be able to send json body in POST and PUT methods to database

///////////////////////////////////////////////////////////////////////GET
app.get("/", async (req, res) => {
  try {
    const [rows] = await pool.query(
      `SELECT cars.*, phoneNumbers.phone
      FROM cars
      LEFT JOIN phoneNumbers ON phoneNumbers.ph_id = cars.car_id`
    );

    console.log(rows);

    if (!rows.length) {
      return res.status(404).json({ message: "No data found in database" });
    }

    // Convert serviceCheck to boolean
    const formattedRows = rows.map((row) => ({
      //converting database -serviceCheck property from 1/0 to true/false
      //MySQL, SQLite, SQL Server, Oracle stores boolean values as 1 (true) and 0 (false) under the hood.
      ...row,
      serviceCheck: Boolean(row.serviceCheck), // Convert 1/0 to true/false
    }));

    //is better use res.json(formattedRows) instead of res.send(formattedRows), can handle JSON objects better
    res.json(formattedRows);
    // res.send(formattedRows);
  } catch (err) {
    console.error(err);
    res.status(500).json({ message: "Server error" });
  }
});

////////////////////////////////////////////////////////////////////////////GET ID

app.get("/:carId", async (req, res) => {
  //get ID from request body, always comes as a string
  const { carId } = req.params;
  // find if the ID exists in the database

  //isNaN checks if the value is Not-a-Number, automatically convert the string to a number
  // If carId is NaN, then isNaN(carId) returns true
  if (isNaN(carId)) {
    return res
      .status(400)
      .json({ message: "Wrong card Id has been inserted." });
  }

  try {
    const [rows] = await pool.query(
      `SELECT cars.*, phoneNumbers.phone
       FROM cars
       LEFT JOIN phoneNumbers ON phoneNumbers.ph_id = cars.car_id
       WHERE cars.car_id = ?`, //// Use ?, ? placeholders for parameterized queries, In PostgreSQL we use $1, $2 — but that's not valid for MySQL
      [carId]
    );

    //if carId doesn't exist in database -> the result.length === 0
    if (!rows.length) {
      return res.status(404).json({ message: "Car Id Not Found" });
    }

    const car = {
      //converting database -serviceCheck property from 1/0 to true/false
      ...rows[0],
      serviceCheck: Boolean(rows[0].serviceCheck), // Convert 1/0 to true/false
    };

    res.json(car);
  } catch (err) {
    //err - if can't connect to our database
    console.log(err);
    res.status(500).json({ message: "Server error" });
  }
});

////////////////////////////////////////////////////////////////POST

app.post("/", async (req, res) => {
  // console.log(req.body);

  const { brand, seats, date, fuel, serviceCheck, involved, notes, phone } =
    req.body;

  // error handling, when posting but brand or /and seats field is empty
  if (!brand) {
    return res.status(400).send("Brand is required");
  } else if (!seats || isNaN(seats)) {
    return res.status(400).send("Seats must be a number");
  }

  try {
    // Insert into cars
    const [result] = await pool.query(
      //destructuring the result to get the insertId
      `INSERT INTO cars (brand, seats, date, fuel, serviceCheck, involved, notes) VALUES (?, ?, ?, ?, ?, ?, ?)`,
      [
        brand,
        seats,
        date || null,
        fuel || null,
        serviceCheck,
        involved || null,
        notes || null,
      ]
      //If date, fuel, involved, or notes are empty or undefined, they will be saved as null in the database.
    );

    const carId = result.insertId; // Get the inserted car_id from result, if the query was successful
    //The insertId (the ID of the new row if it's auto-incremented).

    if (phone) {
      // Insert phone
      await pool.query(
        `INSERT INTO phoneNumbers (ph_id, phone) VALUES (?, ?)`,
        [carId, phone]
      );
    }

    res.status(201).send(`Car created with ID: ${carId}`);
  } catch (err) {
    console.log(err);
    res.status(500).json({ message: "Server error" }); //if there is an error connecting to the database
  }
});

/////////////////////NOTES from PSQL
//use RETURNING *; (/ or RETURN column1, column2,... ); <--- for POST, PUT, PATCH, DELETE methods
//<--to get the inserted row back, The updated row(s) or deleted row(s) back

// const data = pool.query(
//       `INSERT INTO comments (author,body,article_id) VALUES($1, $2, $3)
//       RETURNING*;`,
//       [newComment.username, newComment.body, article_id]
//     )

////////////////////////////////////////////////////////////////////////UPDATE
app.put("/:carId", async (req, res) => {
  // Get ID from request body
  const { carId } = req.params;
  const { brand, seats, date, fuel, serviceCheck, involved, notes, phone } =
    req.body;

  const updatedData = req.body;

  if (isNaN(carId)) {
    return res.status(400).json({ message: "Invalid car ID" });
  }

  try {
    // Check if car exists
    const [existing] = await pool.query("SELECT * FROM cars WHERE car_id = ?", [
      carId,
    ]);

    if (!existing.length) {
      return res.status(404).json({ message: "Car ID not found" });
    }

    // Update cars table
    await pool.query(
      `UPDATE cars SET brand = ?, seats = ?, date = ?, fuel = ?, serviceCheck = ?, involved = ?, notes = ? WHERE car_id = ?`,
      [
        brand || existing[0].brand,
        seats || existing[0].seats,
        date || existing[0].date,
        fuel || existing[0].fuel,
        serviceCheck !== undefined ? serviceCheck : existing[0].serviceCheck,
        involved || existing[0].involved,
        notes || existing[0].notes,
        carId,
      ]
    );

    if (phone) {
      // Update or insert phone number for this car
      const [phoneExists] = await pool.query(
        "SELECT * FROM phoneNumbers WHERE ph_id = ?",
        [carId]
      );

      if (phoneExists.length) {
        await pool.query("UPDATE phoneNumbers SET phone = ? WHERE ph_id = ?", [
          phone,
          carId,
        ]);
      } else {
        await pool.query(
          "INSERT INTO phoneNumbers (ph_id, phone) VALUES (?, ?)",
          [carId, phone]
        );
      }
    }

    res.status(200).json({ message: "Car updated successfully", updatedData }); //send a message that the car has been updated successfully
    // res.status(200).send("Car updated successfully");
  } catch (err) {
    console.log(err);
    res.status(500).json({ message: "Server error" }); //if there is an error connecting to the database
  }
});

////////////////////////////////////////////////////////////////////////DELETE

app.delete("/:carId", async (req, res) => {
  const { carId } = req.params;

  if (isNaN(carId)) {
    return res.status(400).json({ message: "Invalid car ID" });
  }

  try {
    // Delete phone number first
    await pool.query("DELETE FROM phoneNumbers WHERE ph_id = ?", [carId]);

    // Delete car
    const [result] = await pool.query("DELETE FROM cars WHERE car_id = ?", [
      carId,
    ]);

    if (result.affectedRows === 0) {
      return res.status(404).json({ message: "Car ID not found" });
    }

    res.status(204).json({ message: "Car deleted Successfully", carId }); //204 No Content
  } catch (err) {
    console.error(err);

    res.status(500).json({ message: "Server error" }); //if there is an error connecting to the database
  }
});

module.exports = app;
