# ORM

- technology that allows allows to work with database by using OOP approach.
- make it easy to work bwtween application and database, automatically transforming objects into database style data and other way round.
- ORM is needed to improve development productivity and improving code reading and reducing number of errors during working with data.
- Prisma is used in Back-End (with Nest.js or Node.js) with TypeScript

ORM it is alternative way how you can work with database, without using SQL queries to work with database. Using simple objects and method chain you can make query to database without SQL.

PRISMA supports a lot of Databases:

- PostgreSQL
- MySQL
- SQLite
- mongoDB
- and others

- Prisma allows to make complicated queries to database, but like any ORM it is limited.
- Very rarely if query is very complicated you need to use SQL query to datbase.

# 👉 Prisma START

1. Install Prisma

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

npm install pg
npm install -D @types/pg  //Install pg types if needed


npm install @nestjs/config //Instal to use process.env instead of using require('dotenv').config()

//and add in app.module.ts
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

2. Initialize Prisma

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
In .env:
DATABASE_URL="postgresql://postgres:password@localhost:5432/mydb?schema=public" //for PSQL

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

model User {
  id    Int    @id @default(autoincrement())
  email String @unique
  name  String?
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

```JS
//prisma.module.ts

import { Global, Module } from '@nestjs/common';
import { PrismaService } from './prisma.service';

@Global()
@Module({
  providers: [PrismaService],
  exports: [PrismaService],
})
export class PrismaModule {}

//Using @Global() means you only import the module once.
```

7. Import the module

```JS
//app.module.ts

import { Module } from '@nestjs/common';
import { PrismaModule } from './prisma/prisma.module';

@Module({
  imports: [PrismaModule],
})
export class AppModule {}
```

8. Inject Prisma anywhere

```JS
//Example service

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

# ✅ Useful Prisma commands

```JS
//# Generate Prisma Client
npx prisma generate
//Generates the Prisma Client after changing `schema.prisma`.

//# Create and apply a migration
npx prisma migrate dev --name add_users

//🔥 MIGRATIONS
npx prisma migrate dev --name init //<-- first migration
npx prisma migrate dev --name add_createdAt_to_post //add migration when we added createdAt field to Post table
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
npx prisma studio

//# Reset the database
npx prisma migrate reset
// - Deletes all data.
// - Drops and recreates the database (or schema).
// - Reapplies all migrations.
// - Regenerates the Prisma Client.


//is used only in Development environment.
//In Production environment use Migrations ONLY !!!
npx prisma db push //to add data from code into your Database
//used only in local development, when learning PRISMA
//with "db push" command you can't return back to previous data, you overwrite hardly all data in Database, it is not safe, because all data in database is overwriten, Prisma will warn you before applying destructive changes.
//The main reason it's not recommended for production is that it doesn't create migration files, so there's no versioned history of schema changes.

npx prisma db pull //to turn your DB schema into a Prisma schema
// Useful when:
// - connecting Prisma to an existing database
// - another tool changed the database schema
```

## Migrations

- Migrations allow to track code changes, fileds changes
- If you add,edit, delete any filed in Prisma schema/model it will record all changes

```JS
//Don't use this command in Production env
npx prisma migrate dev --name init
//migration command create a "migrations" folder with new file -> prisma/migrations/migrationFile -> this file has all changes in SQL that you made
//If you need to delete any migration file - delete it from prisma/migration folder and delete that migration from the Database table -> Table "Migrations" in your DB, all migrations are recorder there as well

//In Development environment - this command automatically will change data and will work!!!

// 🚀 In Production environmnet: -
//- push your new code to GitHub (using CI/CD for example)
// - to apply new migration you need to run command->
npx prisma migrate deploy
```

# Recommended project structure

```JS
src/
│
├── prisma/
│   ├── prisma.module.ts
│   └── prisma.service.ts
│
├── users/
│   ├── users.controller.ts
│   ├── users.service.ts
│   └── users.module.ts
│
├── app.module.ts
└── main.ts

prisma/
│
└── schema.prisma
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
