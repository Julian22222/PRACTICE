const db = require("./db");
require("dotenv").config();

const util = require("util"); //node utilities for mySQL, give access to different functions

//mySQL doesn't support async await, support callbacks only. Theredore we need -  util
//util.promisify <-- return Promise
const query = util.promisify(db.query).bind(db);
const seed = async () => {
  await query(`USE ${process.env.DB_DATABASE};`);
  const drop = await query(`DROP TABLE IF EXISTS cars;`);

  // console.log(drop);

  const create = await query(
    `CREATE TABLE IF NOT EXISTS cars (
          car_id INT AUTO_INCREMENT PRIMARY KEY,
          brand VARCHAR(100) NOT NULL,
          seats INT NOT NULL,
          year DATE,
          fuel VARCHAR(50) NOT NULL
          );`
  );

  // console.log(create);

  const insert = await query(
    `INSERT INTO cars (brand,seats,year,fuel)
                VALUES
                ('Bugatti',2,'2023-10-02','petrol'),
                ('Bentley',5,'2023-09-02','hybrid'),
                ('Skoda',5,'2023-08-10','electric'),
                ('Porsche',2,'2023-07-09','petrol'),
                ('Volkswagen',5,'2022-05-11','diesel'),
                ('Lamborghini',2,'2023-04-19','electric'),
                ('Volvo',6,'2023-10-05','hybrid');`
  );

  // console.log(insert);

  ////////////////////////////////////////////////////
  //Usual code when Await async is supported

  //await db.query(`DROP TABLE IF EXISTS cars;`);

  // const createTable = await db.query(
  //   `CREATE TABLE IF NOT EXISTS cars (
  //   car_id INT AUTO_INCREMENT PRIMARY KEY,
  //   brand VARCHAR(100) NOT NULL,
  //   seats INT NOT NULL,
  //   year DATE,
  //   fuel VARCHAR(50) NOT NULL
  //   );`
  // );

  // const insert = await db.query(
  //   `INSERT INTO cars (brand,seats,year,fuel)
  //   VALUES
  //   ('Bugatti',2,'2023-10-02','petrol'),
  //   ('Bentley',5,'2023-09-02','hybrid'),
  //   ('Skoda',5,'2023-08-10','electric'),
  //   ('Porsche',2,'2023-07-09','petrol'),
  //   ('Volkswagen',5,'2022-05-11','diesel'),
  //   ('Lamborghini',2,'2023-04-19','electric'),
  //   ('Volvo',6,'2023-10-05','hybrid');`
  // );
};

module.exports = seed;
