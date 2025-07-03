CREATE TABLE cars(
car_id SERIAL PRIMARY KEY,
  brand VARCHAR(100) NOT NULL,
  seats INT NOT NULL,
  date DATE,
  fuel VARCHAR(50) DEFAULT "petrol",
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  serviceCheck BOOLEAN DEFAULT FALSE,
  involved VARCHAR(300) DEFAULT NULL,
  notes VARCHAR(500) DEFAULT NULL
);

CREATE TABLE phoneNumbers(
phone_id SERIAL PRIMARY KEY,
phone VARCHAR(15) NOT NULL,
ph_id BIGINT UNSIGNED NOT NULL,
FOREIGN KEY (ph_id) REFERENCES cars(car_id) 
);

INSERT INTO cars(brand,seats,date,fuel,created_at,serviceCheck,involved,notes)
VALUES
('Bugatti',2,'2023-10-02','petrol','2025-02-23 14:30:00', FALSE, 'Chris Brown, Mike Johnson', 'No issues'),
('Bentley',5,'2023-09-15','petrol','2025-03-23 14:30:00', FALSE,'Chris Brown', 'Minor scratches'),
('Porsche',2,'2023-08-20','petrol','2025-04-18 14:30:00', FALSE,'John Doe', NULL),
('Toyota',5,'2023-07-25','hybrid','2025-05-15 14:30:00', TRUE,'Tom Smith, John Doe', 'Battery replaced'),
('Honda',5,'2023-06-10','petrol','2025-06-10 14:30:00', FALSE, 'Mike Johnson','Regular maintenance'),
('BMW',2,'2023-05-05','diesel','2025-07-05 14:30:00', TRUE, 'Tom Smith', NULL),
('Audi',5,'2023-04-01','hybrid','2025-08-01 14:30:00', TRUE, 'Mike Johnson', NULL),
('Volvo',5,'2023-04-01','hybrid','2025-08-01 14:30:00', TRUE, 'Tom Smith, John Doe', NULL);

INSERT INTO phoneNumbers(phone, ph_id)
VALUES
('1234567890', 1),
('0987654321', 2),
('1122334455', 3),
('2233445566', 4),
('3344556677', 5),
('4455667788', 6),
('5566778899', 7),
('5566778899', 8);


-------------------- JOIN 2 tables
SELECT cars.brand, cars.seats, cars.seats, cars.notes, phoneNumbers.phone
FROM cars
INNER JOIN phoneNumbers ON ph_id = car_id;

-- Corrected JOIN query to select all columns from both tables
SELECT *
FROM cars
INNER JOIN phoneNumbers ON ph_id = car_id;