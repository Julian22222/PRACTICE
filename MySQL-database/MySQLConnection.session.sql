CREATE TABLE cars(
car_id SERIAL PRIMARY KEY,
  brand VARCHAR(100) NOT NULL,
  seats INT NOT NULL,
  year DATE,
  fuel VARCHAR(50) DEFAULT 0
);

INSERT INTO cars(brand,seats,year,fuel)
VALUES('Bugatti',2,'2023-10-02','petrol'),
('Bentley',5,'2023-09-02','hybrid'),
('Skoda',5,'2023-08-10','electric'),
('Porsche',2,'2023-07-09','petrol'),
('Volkswagen',5,'2022-05-11','disel'),
('Lamborghini',2,'2023-04-19','electric');