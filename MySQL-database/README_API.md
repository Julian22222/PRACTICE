# Use parameterized queries everywhere, never concatenate strings for SQL queries.

- Don't use concatenating strings for SQL queries. Here, you directly append the carId parameter (which comes from the user via URL params) into the SQL query string.

- Is vulnerable to SQL Injection attacks — a malicious user could send something like carId=1; DROP TABLE cars; and the query could execute unintended commands that damage or expose your data.

- May cause errors if the value contains characters like quotes ', which break the SQL syntax.

- Is harder to maintain and less readable than using parameterized queries

```JS
//Bad Example 1

pool.query(
    `SELECT cars.car_id,cars.brand,... FROM cars INNER JOIN phoneNumbers ON ph_id = car_id
    WHERE car_id ='` + carId + "'");  //<--  car_id ='` + carId + "'" Is oncatenating string, not safe to use
```

Use parameterized queries

- database driver automatically escapes and safely inserts the user-supplied values for you.
- The MySQL library safely escapes carId before inserting it into the query.
- This prevents SQL injection and avoids syntax errors.
- Better Readability: Cleaner, easier-to-read code.
- Maintainability: Easy to change queries without string concatenation chaos.

```JS
//Good example 2

pool.query(
  "SELECT cars.*, phoneNumbers.phone FROM cars LEFT JOIN phoneNumbers ON ph_id = car_id WHERE car_id = ?", [carId]);
```

# Best Practice to use async/await with Promises instead of using callbacks for async flow.

- async/await with Promises is better than using callbacks
- try/catch it is working with async/await!!!
- Promises that lets you write asynchronous code like synchronous code, making it easier to read and maintain.
- If you use try/catch it won't catch the .then() or .catch() errors because .then() returns a Promise. For try/catch to work, you need async/await.
- Don't use try/catch and Promise (.then()) together

Use async/await over callbacks:

    - Cleaner code — Avoid “callback hell” (nested callbacks, hard to read).
    - Better error handling — Use try/catch blocks instead of checking errors in every callback.
    - Easier to reason about — Your code flows top-to-bottom rather than nested and fragmented.
    - More maintainable — Easier to add logic before/after async calls.

Example below works but it is not clean in therms of code syntax

- try/catch doesn’t catch errors inside pool.query because it’s a callback.
- Handling error and success are done inside the callback, fragmenting the flow.
- You can’t await pool.query because it uses callbacks.

```JS
//Bad example 1

app.get("/", async (req, res) => {             //<--async/await
  try {
    pool.query("SELECT * FROM cars INNER JOIN phoneNumbers ON ph_id = car_id", (err, result, fields) => {     //<-- call backs
      if (err) {
        res.send(err);
      }
      //some code..
    }
  }
});
```

```JS
//Example 2 - using callback

app.get("/", (req, res) => {
  pool.query(
    "SELECT * FROM cars INNER JOIN phoneNumbers ON ph_id = car_id",
    (err, results) => {
      if (err) {
        console.error(err);
        return res.status(500).send("Database error");
      }
      res.json(results);
    }
  );
});
```

```JS
//Example 3 - using Promises,
//this only works if your pool is set up to support promises.

app.get("/", (req, res) => {
  pool
    .query("SELECT * FROM cars INNER JOIN phoneNumbers ON ph_id = car_id")
    .then(([rows]) => {
      res.json(rows);
    })
    .catch((err) => {
      console.error(err);
      res.status(500).send("Database error");
    });
});


//.then() Only Works if:
//You initialized your pool like this:
const mysql = require("mysql2/promise");

const pool = mysql.createPool({
  host: "localhost",
  user: "root",
  password: "",
  database: "your_db",
});

```

Use this option instead

- pool.query returns a Promise which you await.
- Your code looks synchronous and linear.
- You use a single try/catch block to catch any error — no fragmented error handling.
- No nested callbacks — more readable and maintainable.
- You can return early if no data found, reducing nested ifs

```JS
//Good example 4

app.get("/", async (req, res) => {  //<-- use async
  try {
    const [result] = await pool.query("SELECT * FROM cars LEFT JOIN phoneNumbers ON ph_id = ?",[ car_id]);  //<-- use await and no callbacks

    if (!result || result.length === 0) {
      return res.status(404).send("No data found in database");
    }

    const formattedResult = result.map(row => ({
      ...row,
      serviceCheck: Boolean(row.serviceCheck),
    }));
    res.json(formattedResult);
  }
```

# Always return proper HTTP status codes in your REST API

res.status(404).send("Car Id Not Found");
res.status(201).send("Data inserted into both tables successfully");
res.status(204).send("Data Updated Successfully");
return res.status(400).send("Wrong card Id has been inserted.");
res.status(500).send("Server error");

200 OK for successful GET
201 Created for POST success
204 No Content for successful update/delete with no body
400 Bad Request for validation errors
404 Not Found when the resource doesn't exist
500 Internal Server Error for unexpected errors

# ✅ try-catch with async await and block then()

try-catch:

- during running some programms errors can occur, therefore we use -try catch.
- catch block - allow us to process, or handle error and show
- required message to the user or developer, without shouting down the running programm.
- in catch block we write down the error that we are trying to catch , and then we
- show a message that we want to show to the user, when this error occur.

```JS
try{
  /some code to do
}
catch(err){
  //show an error to developer
  //Anything after a throw is unreachable, so console.log must be before throw:
  console.log(err)
  throw error(err);
  //or
  throw new Error(`Something went wrong: ${err.message}`);
}

//to handle other errors-->  can use may catch blocks to catch different errors
//Example from C#

try{.....
}catch(error){ ..... when find this error , do this...
}catch(FormatException){ ..... when find this error , do this...
}catch(DivideByZeroException){..... when find this error , do this...}4

also you can add -- finally block - which will anyway run after try blcok or catch block
try{....
}catch(){....
}finally{ .....}
```

When you get a data from Database, by --> fetch(“http://......”).
It always returns a Promise

Then you need to handle the response asynchronously using .then() or async/await.

⭐ Try-Catch blocks can be used with: async await, but not with then() callback-->

- Because -try...catch works with synchronous code or with async/await (which is syntactic sugar over Promises),
- but .then() uses callbacks that are asynchronous, so exceptions inside those callbacks don’t propagate to the outer try...catch.
- try/catch only catches synchronous errors.

❌ If we will use try catch block with then() - we won’t catch the error inside .then() because the error happens asynchronously after the try block has already finished running.

Example below:

```JS
//❌ This is BAD example, not working
try {
  fetch(url)
    .then(response => {
      // If this throws an error...

      // .then() callbacks run later, in the microtask queue → outside the try block → so the catch never triggers.
      throw new Error('Oops');
    });
} catch (error) {
  // This won't catch the error thrown inside .then()
  console.error('Caught:', error);
}
```

```JS
//🔥 Good Example with Try-Catch block + async/await

async function fetchData() {

  try {
    const response = await fetch(url);
    if (!response.ok) throw new Error('Network response was not ok');
    const data = await response.json();
    console.log(data);
  } catch (error) {
    console.error('Fetch error:', error);
  }
}
```

```JS
//🔥 Good Example with promises (callbacks)

fetch(url)
  .then(response => {
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
  })
  .then(data => {
    console.log(data);
  })
  .catch(error => {
    console.error('Fetch error:', error);
  });
```
