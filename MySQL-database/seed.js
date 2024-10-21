const db = require("./db");

const seed = async () => {
  await db.query(`DROP TABLE IF EXISTS cars;`);

  const carsTable = db.query(`
   CREATE TABLE cars(
    car_id SERIAL PRIMARY KEY,
    brand VARCHAR(100) NOT NULL,
    seats INT NOT NULL,
    year DATE,
    fuel VARCHAR(50) DEFAULT 0
    );`);

  await Promise.resolve(carsTable);

  const insertCarsValues = format(
    `INSERT INTO cars(brand,seats,year,fuel)
    VALUES('Bugatti',2,'2023-10-02','petrol'),
    ('Bentley',5,'2023-09-02','hybrid'),
    ('Skoda',5,'2023-08-10','electric'),
    ('Porsche',2,'2023-07-09','petrol'),
    ('Volkswagen',5,'2022-05-11','disel'),
    ('Lamborghini',2,'2023-04-19','electric');`
  );

  const carsPromise = db.query(insertCarsValues).then((result) => result.rows);

  await Promise.resolve(carsPromise);

  return db.query(insertCarsValues).then((result) => result.rows);
};

module.exports = seed;
