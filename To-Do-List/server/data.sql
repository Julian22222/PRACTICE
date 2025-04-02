-- DROP DATABASE IF EXISTS todo_app;

-- CREATE DATABASE todo_app;

-- \c todo_app;

-- \c tyzqzdyo;

-- CREATE TABLE todos(
--     todo_id SERIAL PRIMARY KEY,
--     user_email VARCHAR(255),
--     title VARCHAR(30),
--     progress INT,
--     date VARCHAR(300)
-- );

-- CREATE TABLE users(
--     email SERIAL PRIMARY KEY,
--     password VARCHAR(255)
-- );

-- INSERT INTO todos
-- (user_email, title, progress, date)
-- VALUES('julian@test.com', 'First todo', 10, 'Thu March 2 2023 19:15:35 GMT+0400 (Gulf Standard Time)');

-- INSERT INTO users
-- (password)
-- VALUES(123);

SELECT * FROM todos;