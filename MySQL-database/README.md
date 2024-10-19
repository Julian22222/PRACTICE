# Connection using ElephantSQL + DBeaver

- ElephantSQL provides all data for DB connection, such as:

  - Server
  - User & DataBase name
  - password
  - URL link

- DBeaver needs all this Data from ElephantSQL to connect our DB
- Insert Data from ElephantSQL to DBeaver-->

  1. Server(from ElephantSQL) === Host(from Dbeaver)
  2. User & Default database (from ElephantSQL) === Database and Username (from Dbeaver)
  3. Password (from ElephantSQL) === Password (from Dbeaver)
  4. PORT (in DBeaver always stays 5432)

- URL link (from ElephantSQL) we use in Render.com -->in Environment variables (to host our DB to web)
