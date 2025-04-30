DROP DATABASE IF EXISTS my_bookshop;

CREATE DATABASE my_bookshop;

\c my_bookshop;

CREATE TABLE authors_data(
    author_id SERIAL PRIMARY KEY,
    author VARCHAR(50),
    author_fun_fact VARCHAR(300)
);

\c my_bookshop

INSERT INTO authors_data
  (author, author_fun_fact)
VALUES
('Dan Brown', 'Favourite colour is not brown'),
('Antoine de Saint-Exupéry', 'He was a successful commercial pilot before World War II, working airmail routes in Europe, Africa, and South America.'),
('Douglas Adams', 'He made two appearances in Monty Python`s Flying Circus'),
('Stephen Hawking', 'Doctors told him he wouldn`t live past his early 20s'),
('Eric Carle', 'When he was a young boy, Carle had a dream that he would build a bridge from Germany to America.'),
('J. D. Salinger', 'The Catcher in the Rye was the only novel that J.D. Salinger published during his lifetime, not bad for a first try!'),
('Beatrix Potter', 'Between 1881 and 1897 Potter kept a journal in which she jotted down her private thoughts in a secret code . This code was so fiendishly difficult it was not cracked and translated until 1958.'),
('C. S. Lewis', 'Lewis set up a charitable trust to give away whatever money he received from his books.'),
('Roald Dahl', 'During World War II he passed intelligence to MI6 from Washington.'),
('Frank Herbert', 'While conversing with fungi expert Paul Stamets, Herbert revealed that the world of Dune was influenced by the lifecycle of mushrooms, with his imagination being helped along by a more "magic" variety.'),
('Louis de Bernières', 'De Bernières is an avid musician who plays flute, mandolin, clarinet and guitar.'),
('H. G. Wells', 'In 1914 H.G. Wells published a novel titled The World Set Free. In this book he described a weapon that was eerily similar to the first atomic bomb unleashed on the Japanese cities of Hiroshima and Nagasaki in 1945.'),
('George Orwell', 'Orwell intentionally got himself arrested for being "drunk and incapable."'),
('Jane Austen', 'The author of her first novel, Sense and Sensibility was simply "A Lady," and her later works like Pride and Prejudice were credited to "the Author of Sense and Sensibility." She wasn`t named as the author of her novels until after her death!'),
('Margaret Atwood', 'Atwood was the first author to contribute to The Future Library Project, which will take one writer`s contribution each year for one hundred years to be printed in the year 2114.');

-- SELECT * FROM authors_data;