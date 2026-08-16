# ⭐ ORM

- Prisma allow to describe database structure in convenient way.
- technology that allows allows to work with database by using OOP approach.
- make it easy to work bwtween application and database, automatically transforming objects into database style data and other way round.
- ORM is needed to improve development productivity and improving code reading and reducing number of errors during working with data.
- Prisma is used in Back-End (with Nsest.js or Node.js) with TypeScript

ORM it is alternative way how you can work with database, without using SQL queries to work with database. Using simple objects and method chain you can make query to database without SQL.

# ➕ Advantages of Prisma, why Prisma is a top ORM?

1. Prisma provide data types, Prisma automatically generate all TypeScript types based on your Database -> in Prisma/schema.prisma file
2. Understandable and simple
3. Automated Migration
4. PRISMA supports a lot of Databases:

- PostgreSQL
- MySQL
- SQLite
- mongoDB
- and others

- Prisma allows to make complicated queries to database, but like any ORM it is limited.
- Very rarely if query is very complicated you need to use SQL query to datbase.

# Why you need to try to stick with the plain ORM instead of using row SQL qeries in Prisma?

```JS
//example
//app.service.ts

sqlRequest(){
    return this.prisma.findUnique({
        where:{
            email:"test@test.com"
        }
    })

    //the same query but using row SQL query in Prisma
     return this.prisma.$queryRaw`SELECT * FROM user WHERE email = ${'test@test.com'}`

}
```

❌ Disadvantages of using - row SQL queries:

- it has no data types
- it is not optimised
- this approach is not scalable

✅ But sometimes you have to use it because it has more query options that ORM doesn't have

# 🌾 See how to use Prisma to seed your DB

- intall tsx

```bash
npm install -D tsx
```

- in prisma folder create - seed.ts file
- add this line -> seed: 'tsx prisma/seed.ts', in -> prisma.config.ts file
- run command

```JS
npx prisma db seed
```

# 👉 Prisma START

1. Install Nest (we will use -> Nest.js + Prisma)

```JS
//in terminal install NEST.JS

sudo npm install -g @nestjs/cli@latest
//or
🔥 npm i -g @nestjs/cli

//then ->
🔥 nest new my-app --skip-git  //this option with NO hidden .git folder
//✔ This avoids the problem completely.

//OR simply

🔥 npx @nestjs/cli new project-name --skip-git   //this option with NO hidden .git folder
//npx lets you run CLI tools without installing them globally.
//npx automatically downloads and runs the Nest CLI for this command only
//Always uses the latest version automatically

//nest.js CLI --> it is the way to help you to generate new projects and also has a couple other extra commands and create new files for you and it will make it easier
```

```JS
npm install pg //install postgreSQL DB
npm install -D @types/pg  //Install pg types if needed
```

```JS
npm install @nestjs/config //Instal to use process.env instead of using require('dotenv').config()
//therefore don't need to instal -> npm i dotenv

//add in app.module.ts
import { ConfigModule } from '@nestjs/config'; //npm install @nestjs/config

@Module({
  //use ConfigModule.forRoot() to load environment variables from .env file, instead of using require('dotenv').config()
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
    }),
  ],
  controllers: [AppController],
  providers: [AppService, PrismaService],
})
export class AppModule {}
```

2. Install Prisma & Initialize Prisma

```JS
//go to my-app folder

//# Install Prisma CLI
npm install prisma -D  //<-- should be development dependency
//or
//npm install prisma --save-dev

//# Install Prisma Client
npm install @prisma/client

npm install @prisma/adapter-pg //Install the Prisma PostgreSQL adapter
//PrismaClient driver adapter is required to connect to your database.

//If you use different DB you need to install different adapter
//npm install @prisma/adapter-mysql
//npm install @prisma/adapter-better-sqlite3
//and other databases adapters - etc.


npx prisma init //initialize Prisma
//This creates:
// prisma/
//   schema.prisma
// .env


//Your project will look something like this:
// my-project/
// │
// ├── prisma/
// │   └── schema.prisma
// │
// ├── src/
// │   ├── app.module.ts
// │   └── main.ts
// │
// ├── .env
// ├── package.json
// └── tsconfig.json
```

3. Configure the database

```JS
//In .env:
DATABASE_URL="postgresql://postgres:password@localhost:5432/mydb?schema=public" //for PSQL
//postgresql - which db you are using
//postgres - psql username
//mydb - your DB name

//DATABASE_URL="mysql://root:password@localhost:3306/mydb"   //for MySQL
//DATABASE_URL="file:./dev.db"  //for SQLite
```

4. Edit prisma/schema.prisma

```JS
//Example
generator client {
  provider = "prisma-client-js"
}

datasource db {
  provider = "postgresql"
//   url      = env("DATABASE_URL")
}

//add your models here, (tables with its fields)
//Prisma models
//Prisma has a special language that allows to describe your models
//Model it is a table in your database
model User {
  id    Int    @id @default(autoincrement())
  email String @unique
  name  String?
  posts    Post[] //<--one user can have many posts

  @@map("users") //<--@map allow to rename this table in Database from User to users
}

model Post {
  id       String     @id  @default(cuid())
  title    String
  content  String?
  isPublished   Boolean   @default(false) @map("is_published")  //<--@map allow to rename this filed in Database to -> is_published
  author      User    @relation (fields: [authorId],  references: [id])  //link to another table with connection by authorId and refernce - id, each post has one User
  authorId   String   @map("author_id")  //<--@map allow to rename this filed in Database to -> author_id
  createdAt   DateTime  @default(now())  @map("created_at")

  @@map("posts")
}
```

5. Create the database

```JS
//After configuring your database URL in .env, you can:

//# Create your first migration
npx prisma migrate dev --name init
//Prisma will:
//create the database tables from prisma/schema.prisma
//generate the Prisma Client


//If you only changed the schema later:
//# Generate Prisma Client
//create databese from your code, from objects
npx prisma generate

```

6. Create PrismaService

```JS
src/prisma/
    prisma.module.ts
    prisma.service.ts
```

```JS
//prisma.service.ts

import { Injectable } from '@nestjs/common';
import { PrismaPg } from '@prisma/adapter-pg';
import { PrismaClient } from '../generated/prisma/client.js';

@Injectable()
export class PrismaService extends PrismaClient {
  constructor() {
    const adapter = new PrismaPg({
      connectionString: process.env.DATABASE_URL!,
    });

    super({ adapter });
    console.log('DATABASE_URL =', process.env.DATABASE_URL);
  }
}
```

7. Import Prisma into the module - app.module.ts

```JS
//app.module.ts


@Module({
  //use ConfigModule.forRoot() to load environment variables from .env file, instead of using require('dotenv').config()
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
    }),
  ],
  controllers: [AppController],
  providers: [AppService, PrismaService],
})
export class AppModule {}
```

8. Inject Prisma anywhere

```JS
//Example service - app.service.ts

import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';

@Injectable()
export class UsersService {
  constructor(private prisma: PrismaService) {}

  async findAll() {
    return this.prisma.user.findMany();
  }

  async create(email: string, name: string) {
    return this.prisma.user.create({
      data: {
        email,
        name,
      },
    });
  }
}
```

# ⛓️‍💥 Transactions - ACID

- provide all data validation if you make many database operations. For example: you create user + you create posts for this user at the same method. if there is an erro of creating a user it will not create posts in databse. It will roll back

see app.service.ts file

# 👀 Complicated queries in Prisma

Use SQL queries in Prisma

- if you need to use Join, LeftJoin, etc. or some calculation operations as SQL request, you can't do it with plain ORM

```JS
//app.service.ts

  sqlRequest() {
    //this is an example of how to use raw SQL queries in Prisma, you can use raw SQL queries to execute any SQL query, for example if you want to select all users from the database, you can use this.prisma.$queryRaw`SELECT * FROM users`
    return this.prisma.$queryRaw`SELECT * FROM users WHERE email = ${'test@test.com'} `; //in quotes you can use any SQL query, for example if you want to select all users from the database, you can use this.prisma.$queryRaw`SELECT * FROM users`
  }
```

# ⚟ Prisma allows to separate your schema into many small schemas

you always start with one schema -> prisma/schema.prisma

If you have a large project that has many lines of code- many models, it is convenient to separate your schema into smaller schema files, for better reading and orentation in your code.

- name your new schemas using -> short names(For example: user.prisma).
- create new foleder -> schema, and put all your schemas there (schema.prisma, user.prisma,post.prisma , etc.)
- if you separate schemas -> in schema.prisma add previewFeatures field

```JS
generator client {
  provider = "prisma-client"
  output   = "../generated/prisma"
  previewFeatures = ["prismaSchemaFolder"]
}
```

```JS
//Prisma structure
//Recommended project structure

myapp/
│
├── generated/   //Generated Prisma Client / TypeScript code
│
├── prisma/
│   ├── migrations/  //Database migration history
│   ├── 202608..._init/
│   │   └── ...
│   │
│   ├── schema/
│   │     ├── schema.prisma //Main Prisma schema(file) / configuration
│   │     ├── user.prisma  //User model(s), separattion into smaller schemas (splitting your Prisma schema into multiple files)
│   │     └── post.prisma  //separattion into smaller schemas (splitting your Prisma schema into multiple files)
│   │
│   └──  seed.ts  //Database seeding script
│
├── src/
│   ├── main.ts  //NestJS application entry point
│   ├── app.module.ts
│   ├── prisma/
│   │   ├── prisma.service.ts
│   │   └── prisma.module.ts
│   └── ...                    // Other NestJS modules/services/controllers
│
├── test/
│
├── package-lock.json
├── package.json
├── prisma.config.ts  // Prisma CLI/configuration
├── tsconfig.json  //TypeScript configuration
```

```JS
//prisma/schema/user.prisma

    model User {  //you can use -> User in your project with TypeScript as a data type
    id       String     @id @default(cuid())
    email    String     @unique
    name     String?
    posts    Post[] //<--one user can have many posts

    @@map("users") //<--@map allow to rename this table in Database from User to users
    }


//prisma/schema/post.prisma
    model Post {
    id       String     @id  @default(cuid())
    title    String
    content  String?
    isPublished   Boolean   @default(false) @map("is_published")  //<--@map allow to rename this filed in Database to -> is_published
    author      User    @relation (fields: [authorId],  references: [id])  //link to another table with connection by authorId and refernce - id, each post has one User
    authorId   String   @map("author_id")  //<--@map allow to rename this filed in Database to -> author_id
    createdAt   DateTime  @default(now())  @map("created_at")

    @@map("posts")
    }
```

```JS
//Flow

NestJS
│
├── Controller
│
├── Service
│       │
│       ↓
│   PrismaService
│       │
│       ↓
│   Prisma Client
│       │
│       ↓
└──── Database
```

# ✅ Useful Prisma commands

make migrations -> then generate (after changes)

```JS
//do this command first
//npx prisma generate → reads the Prisma schema and generates the Prisma Client and related TypeScript types into the configured generated/ directory.
//npx prisma generate → converts the Prisma schema into generated TypeScript code (Prisma Client and types).

//# Generate Prisma Client
npx prisma generate
//Generates the Prisma Client after changing `schema.prisma`.
//this command add new data to your DB

//# Create and apply a migration
npx prisma migrate dev --name add_users

//🔥 MIGRATIONS
//this command only in Development env.
npx prisma migrate dev --name init //<-- first migration
npx prisma migrate dev --name add_createdAt_to_post //add migration when we added createdAt field to Post table, name migration depending from changes that you did
// - Creates a new migration.
// - Applies it to your development database.
// - Updates the Prisma Client.


//# Check migration status
npx prisma migrate status
// Shows:
// - applied migrations
// - pending migrations
// - migration history


//# Open Prisma Studio (GUI)
//Opens a web GUI for viewing and editing your database.
npx prisma studio  //Prisma Studio is a visual web interface for viewing and editing the data in your database.
//after this command -> Prisma starts a local web application where you can see your database tables and records.

//# Reset the database
npx prisma migrate reset
// - Deletes all data.
// - Drops and recreates the database (or schema).
// - Reapplies all migrations.
// - Regenerates the Prisma Client.


//✅ is used only in Development environment. //used only in local development, when learning PRISMA
//✅ In Production environment use Migrations ONLY !!!
npx prisma db push //to add data from code into your Database
//❌ with "db push" command you can't return back to previous data, you overwrite hardly all data in Database, it is not safe, because all data in database is overwriten, Prisma will warn you before applying destructive changes.
//The main reason it's not recommended this command for production is that it doesn't create migration files, so there's no versioned history of schema changes.

npx prisma db pull //to turn your DB schema into a Prisma schema, (if you have already data in your DB and you want to convert data into -> Prisma schema)
// Useful when:
// - connecting Prisma to an existing database
// - another tool changed the database schema
```

## 📊 Migrations

- Migrations allow to track code changes and fields changes
- If you add, edit, delete any filed in Prisma schema/model it will record all changes, -> Migrations has DB changes history
- Migrations allow to return back to previous migration version

```JS
//Don't use this command in Production env
npx prisma migrate dev --name init
//migration command create a "migrations" folder with new file -> prisma/migrations/migrationFile -> this file has all changes in SQL that you made

//If you need to delete any migration file - delete it from prisma/migration folder and delete that migration from the Database table -> Table "Migrations" in your DB, all migrations are recorder there as well

//In Development environment - this command automatically will change data and will work!!!

// 🚀 In Production environmnet: - if you want to apply new migration (apply new changes for your DB)
//- push your new code to GitHub (using CI/CD for example)
// - to apply new migration you need to run command->
npx prisma migrate deploy
```

## Prisma connection to DB:

- in prisma.config.ts file in the root of your project

## You can find - Prisma schemas/models

- in prisma/schema.prisma file

# CRUD operations with PRISMA

- create file -> src/prisma.service.ts

```JS
import { Injectable } from '@nestjs/common';
import { PrismaPg } from '@prisma/adapter-pg';
import { PrismaClient } from '../generated/prisma/client.js';

@Injectable()
export class PrismaService extends PrismaClient {
  constructor() {
    const adapter = new PrismaPg({
      connectionString: process.env.DATABASE_URL!,
    });

    super({ adapter });
    console.log('DATABASE_URL =', process.env.DATABASE_URL);
  }
}

```

- import PrismaService into app.module
- add methods in app.service using Prisma, that we want to do
