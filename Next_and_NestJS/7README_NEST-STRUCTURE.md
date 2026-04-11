### ✅ What is a Module in NestJS?

- A Module is a class annotated with @Module() decorator.
- It organizes your application into logical units.
- Think of a Module as a container that groups related code (like controllers, providers, services) together.

### ✅ Why do we need Modules?

- To keep your app organized.
- To encapsulate features — each module handles a specific domain or feature.
- To manage dependencies between different parts of your app.
- To make the app scalable and maintainable.

### ✅ The basics: What’s inside a Module?

A Module is defined with the @Module() decorator, which takes an object that can have:

1. providers
2. controllers
3. imports
4. exports

### ✅ Module Dependencies (in simple words)

When Module A needs something from Module B, Module A imports Module B.
Module B exports what it wants to share.
This creates a dependency relationship.
NestJS uses this info to inject the right instances where needed.

# ⭐ Modules

A module is a class that is annotated with the @Module() decorator. This decorator provides metadata that Nest uses to organize and manage the application structure efficiently.

Modules keep your app organized. To encapsulate features — each module handles a specific domain or feature. To manage dependencies between different parts of your app. To make the app scalable and maintainable.

Every Nest application has at least one module, the root module - app.module.ts, which serves as the starting point for Nest to build the application graph. This graph is an internal structure that Nest uses to resolve relationships and dependencies between modules and providers. While small applications might only have a root module, this is generally not the case. Modules are highly recommended as an effective way to organize your components. For most applications, you'll likely have multiple modules, each encapsulating a closely related set of capabilities.

✔️ The @Module() decorator takes a single object with properties that describe the module:

➡️ Providers-

the providers that will be instantiated by the Nest injector and that may be shared at least across this module, Here you put “Services” that will share data with this module after that we can make injection.  These are the services or classes that provide functionality. Usually, these are things like services, repositories, or helpers. Providers are injectable using NestJS’s Dependency Injection system. They handle business logic.

➡️ Controllers-

the set of controllers defined in this module which have to be instantiated. Current Module will have access to “Controllers” that you put here

➡️ imports-

the list of imported modules that export the providers which are required in this module, make relationship between modules.  This is where you bring in other modules that you need to use inside this module. By importing, you can use the exported providers from those modules.

➡️ exports-

the subset of providers that are provided by this module and should be available in other modules which import this module. You can use either the provider itself or just its token (provide value).  Modules can make some of their providers available to other modules by exporting them. This means other modules that import this module can use the exported providers.

#### Shared modules

In Nest, modules are singletons by default, and thus you can share the same instance of any provider between multiple modules effortlessly

[ modules ](https://docs.nestjs.com/modules)

Now any module that imports the CatsModule has access to the CatsService and will share the same instance with all other modules that import it as well.

If we were to directly register the CatsService in every module that requires it, it would indeed work, but it would result in each module getting its own separate instance of the CatsService. This can lead to increased memory usage since multiple instances of the same service are created, and it could also cause unexpected behavior, such as state inconsistency if the service maintains any internal state.

## Here is small working example of a NestJS module with a provider and a controller

Example: Cats Module

This module will:

- Have a CatsService provider that returns a list of cats.
- Have a CatsController that defines a GET route to return cats.

```JS
🔥 //Step 0: Setup a new NestJS project (if you don’t have one)

npm i -g @nestjs/cli    //in terminal write, to install Nest JS CLI globally or locally into your project
//or -> sudo npm install -g @nestjs/cli@latest

🔥 //Run this command in your terminal to create a new NestJS project:
nest new my-app --skip-git  //this option with NO hidden .git folder. <--with NEST CLI build in command helps to create your project,
//This creates a new folder called "my-app" with all the basic files

//NestJS CLI --> it is the way to help you to generate new projects and also has a couple other extra commands and create new files for you and it will make it easier. Nest CLI contains many build in commands that helps building Back-end in Nest.js

----------------------------------------------

// There is also a CLI command that allows you to generate an entire resource along with controllers and providers all in one command

🔥 //from src folder in terminal -> if you run this command
nest g resource ninjas   //<-- will create all needed files and folders AUTOMATICALLY for ninjas -> ninjas folder, and ninjas.module.ts, ninjas.controller.ts and ninjas.service.ts files in correct locations. Name of resource = ninjas,
nest generate resource ninjas  //<-- the same command



/////////////////////////
// 👀 Separate module creation - using Nest CLI


📍 //we create new module, with the name ninjas
nest g module ninjas
//or
nest generate module ninjas  //<-- the same command
//This comand will create new file in this direction (will create ninjas folder automatically)--> src/ninjas/ninjas.module.ts
//Also, will update file automatically--> src/app.module.ts our new module will be added to --> imports. starting to build that dependency tree.
//When you are creating new modules make sure it is being added to -> app.module.ts file - to imports array of another module. This is how things are registred in NEST.JS.

📍//<--//we create new service, with name ninjas,
nest g service ninjas
// this command will create -> src/ninjas/ninjas.service.spec.ts,
// //will create -> src/ninjas/ninjas.service.ts,
// //will update -> src/ninjas/ninjas.module.ts (this will add [NinjasService] to providers), to register that service

📍 //<-- we create new controller, with name ninjas,
nest g controller ninjas
// //this command will create -> src/ninjas/ninjas.controller.spec.ts,
// //wil create -> src/ninjas/ninjas.controller.ts
//will update -> src/ninjas/ninjas.module.ts --> (this will add [NinjasController] to controllers), to register that controller
```

```JS
//Step 1: Create cats.service.ts inside src/cats/: (🔥 OR USE NEST CLI - IT WILL CREATE all needed files and folders AUTOMATICALLY)

// src/cats/cats.service.ts
import { Injectable } from '@nestjs/common';

@Injectable()  //@Injectable() means this class can be injected as a provider.
export class CatsService {
  private cats = ['Tom', 'Garfield', 'Felix'];

  findAll(): string[] {  //method findAll returning an array of cat names
    return this.cats;
  }
}
```

```JS
//Step 2: Create cats.controller.ts inside src/cats/: (🔥 OR USE NEST CLI - IT WILL CREATE all needed files and folders AUTOMATICALLY)

// src/cats/cats.controller.ts
import { Controller, Get } from '@nestjs/common';
import { CatsService } from './cats.service';

//@Controller('cats') means this controller handles routes starting with /cats.
@Controller('cats')  // Base route: /cats
export class CatsController {
  constructor(private readonly catsService: CatsService) {}

  @Get()  // GET /cats
  findAll(): string[] {   //findAll method handles GET requests to /cats.
    return this.catsService.findAll();  //It uses CatsService to get the list.
  }
}
```

```JS
//Step 3: Create cats.module.ts inside src/cats/: (🔥 OR USE NEST CLI - IT WILL CREATE all needed files and folders AUTOMATICALLY)

// src/cats/cats.module.ts
import { Module } from '@nestjs/common';
import { CatsService } from './cats.service';
import { CatsController } from './cats.controller';

@Module({   //The @Module() decorator defines this class as a NestJS module.
  providers: [CatsService],   //Declares CatsService as a provider.
  controllers: [CatsController],  //Declares CatsController as a controller.
})

export class CatsModule {}
```

```JS
//Step 4: Use CatsModule in the root module (app.module.ts)

// src/app.module.ts
import { Module } from '@nestjs/common';
import { CatsModule } from './cats/cats.module';

@Module({
  imports: [CatsModule],
})

export class AppModule {}

//The root module AppModule imports CatsModule.This tells NestJS to include everything inside CatsModule when the app runs.
```

➡️ What happens when you run this?

When you start the NestJS app, the CatsModule is loaded.
If you make a GET request to /cats, the CatsController calls CatsService.findAll().
You get a response: ["Tom", "Garfield", "Felix"].

# 🔥 Important how to import and export services - if you want to use different services in other Modules

For example if you want to use -> TransactionsService (from Transactions Module) and AccountsService (from Accounts Module) in UsersService (check Bank/bank-api/src/users/users.service.ts)

```JS
//Bank/bank-api/src/users/users.service.ts)

constructor(
  private readonly accountsService: AccountsService,
  private readonly transactionsService: TransactionsService,
) {}

//line 121
newAcount = this.accountsService.create(accObj); // we use accountsService in user.services.ts file. Invoking accountsService method to interact with the Database

//line 144
const newTrx = this.transactionsService.create(transObj);// we use transactionsService in user.services.ts file. Invoking transactionsService method to interact with the Database

-----------------------------------------

//Then in Bank/bank-api/src/users/users.module.ts file - you need to import these modules (AccountsModule and Transactions Module in our case) - modules that you are going to use in this users module

@Module({
  imports: [AccountsModule, TransactionsModule],  ///<-- here we import these modules
  controllers: [UsersController],
  providers: [UsersService],
})
export class UsersModule {}

----------------------------------------

//Then in Bank/bank-api/src/accounts/accounts.module.ts file - you need to export this accounts module to be able to import it in others modules (import in users Module in our case)
//accounts.module.ts
@Module({
  controllers: [AccountsController],
  providers: [AccountsService],
  exports: [AccountsService],  //<-- here we exporting this module
})
export class AccountsModule {}


//Then in Bank/bank-api/src/transactions/transactions.module.ts file - you need to export this transactions module to be able to import it in others modules (import in users Module in our case)
//transactions.module.ts
@Module({
  controllers: [TransactionsController],
  providers: [TransactionsService],
  exports: [TransactionsService], //<-- here we exporting this module
})
export class TransactionsModule {}
```

# Work with Database in Nest.JS

❌ in Nest.js you don't need to use usual approach such as: (this approach is working but it is not good practice)

```JS
✅ Pros of this approach
- Simple
- Quick to set up
- Fine for small scripts or prototypes

❌ Cons (important)
- ❌ Not testable (hard to mock)
- ❌ Tight coupling to implementation
- ❌ No lifecycle management (connect/disconnect)
- ❌ Hard to scale (multiple DBs, configs, environments)
- ❌ Breaks NestJS dependency injection system

👉 This is basically “Node.js style”, not NestJS style.
```

```JS
  //dbconnection.ts file

const { Pool } = require('pg');
require('dotenv').config();

const pool = new Pool({
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  host: process.env.DB_HOST,
  port: process.env.DB_PORT,
  database: process.env.DB_DATABASE,
});

module.exports = pool;

---------------------------------
//then in services you use ->

const pool = require('../../data/dbconnection');

const await pool.query('QUERY to YOUR DATABSE...');
```

#### 🔥 Good Practice how to interact with DB in Nest.JS

Create DatabaseModule and Inject pool via dependency injection

If you're using NestJS → always use Dependency Injection

🔥 architecture:
Controller → Service → DatabaseService → PostgreSQL

❌ Instead of:
Controller → Service → pool.query(...)

```JS
✅ Pros of this approach
- ✅ Fully aligned with NestJS architecture
- ✅ Easy to mock in unit tests
- ✅ Centralized DB configuration
- ✅ Reusable across modules
- ✅ Supports transactions, multiple DBs, etc.
- ✅ Clean separation of concerns
- ✅ Centralized error handling (try/catch everywhere)
- ✅ Easy to switch DB (future-proof) - for example from PostgreSQL (pg) to Prisma / TypeORM
- ✅ Easy logging (this.logger.log(query);)
- ✅ Easy transactions


❌ Cons
- Slightly more setup (but worth it)

✅ Why creating DatabaseModule is better then previous approach without creating DatabaseModule
- Central place for:
   -logging
   -transactions
   -error handling
- Easier to refactor later (e.g., switch to Prisma)
- Cleaner services
```

```JS
//you create your Database module

//structure
// src/
//   database/
//     database.module.ts
//     database.service.ts   👈 important
//     database.constants.ts (optional)


//database.module.ts

import { Module } from '@nestjs/common';
import { Pool } from 'pg';

export const PG_POOL = 'PG_POOL';

const pool = new Pool({
  user: process.env.DB_USER,
  host: process.env.DB_HOST,
  database: process.env.DB_NAME,
  password: process.env.DB_PASSWORD,
  port: Number(process.env.DB_PORT),
});

@Module({
  providers: [
    {
      provide: PG_POOL,
      useValue: pool,
    },
  ],
  exports: [PG_POOL],
})
export class DatabaseModule {}

---------------------------------------------------------------

//database.service.ts
import { Inject, Injectable, Logger } from '@nestjs/common';
import { Pool } from 'pg';
import { PG_POOL } from './database.module';

@Injectable()
export class DatabaseService {
  private readonly logger = new Logger(DatabaseService.name);

  constructor(@Inject(PG_POOL) private readonly pool: Pool) {}

  async query(query: string, params?: any[]) {
    try {
      this.logger.log(`Executing query: ${query}`);
      return await this.pool.query(query, params);
    } catch (error) {
      this.logger.error('Database error', error.stack);
      throw error;
    }
  }
}

---------------------------------------------------------------

//then import DatabaseModule in AppModule and other modules where you are going to connect to Database

//app.module.ts
import { Module } from '@nestjs/common';
import { DatabaseModule } from './database/database.module';
import { UsersModule } from './users/users.module';

@Module({
  imports: [DatabaseModule, UsersModule], //<--DatabaseModule imported here
})
export class AppModule {}

-------------------------------------

//users.module.ts
@Module({
  imports: [AccountsModule, TransactionsModule, DatabaseModule],  //<--DatabaseModule imported here
  controllers: [UsersController],
  providers: [UsersService],
})
export class UsersModule {}

-------------------------------------

//accounts.module.ts
@Module({
  imports: [DatabaseModule],  //<--DatabaseModule imported here
  controllers: [AccountsController],
  providers: [AccountsService],
  exports: [AccountsService],
})
export class AccountsModule {}

-------------------------------------

//transactions.module.ts
@Module({
  imports: [DatabaseModule],  //<--DatabaseModule imported here
  controllers: [TransactionsController],
  providers: [TransactionsService],
  exports: [TransactionsService],
})
export class TransactionsModule {}

------------------------------------------------------------------

//Then in services
//For example: users.service.ts
import { Inject, Injectable } from '@nestjs/common';
import { Pool } from 'pg';
import { PG_POOL } from '../database/database.module';

//use constructor to inject you DB connection to this class
@Injectable()
export class UsersService {
  constructor(
    @Inject(PG_POOL) private readonly pool: Pool,  //Injection
  ) {}

---------------------------------
//Now replace all -  pool.query calls with:
//then in your methods use:
await this.pool.query(`QUERY to YOUR DATABSE...`)
```

# Summary:

- Service (CatsService): contains logic (list of cats).
- Controller (CatsController): defines routes and handles requests.
- Module (CatsModule): groups service + controller.
- Root module (AppModule): imports the CatsModule to load it.
