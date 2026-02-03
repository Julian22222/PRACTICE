DROP DATABASE IF EXISTS asp_net_mvc_proj1;

CREATE DATABASE asp_net_mvc_proj1;

\c asp_net_mvc_proj1;

CREATE TABLE icon(
    icon_id SERIAL PRIMARY KEY,
    heading VARCHAR(255),
    pageTitle VARCHAR(100),
    content VARCHAR(255),
    shortDescription VARCHAR(300),
    featuredImageUrl VARCHAR(255),
    urlHandle VARCHAR(255),
    date_Time TIMESTAMP,
    author VARCHAR(255),
    visible BOOLEAN NOT NULL
);

CREATE TABLE tags(
    tags_id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    displayName VARCHAR(255)
);

INSERT INTO icon
(heading, pageTitle, content, shortDescription, featuredImageUrl, urlHandle, date_Time, author, visible)
VALUES('This is a heading1','page title1','Content1','shortDescription1', 'featuredImageUrl 1', 'urlHandle 1','2022-11-25 16:00:43.947','Julian','yes');

-- INSERT INTO users
-- ( hashed_password)
-- VALUES(123);

SELECT * FROM icon;