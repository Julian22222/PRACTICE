# NEST JS Framework

- One of the best framework for Node JS
- Node.js Framework for building efficient, reliable and scalable server-side applications
- Nest.JS is cover/abstruction over express.js. NEST JS takes architecture and uses express under the hood.

Nest.js is a progressive Node.js framework for building server-side applications. It’s built with TypeScript (but you can also use JavaScript) and is heavily inspired by Angular (the frontend framework).

# Why use Nest.js?

- It makes building backend applications organized and scalable.
- It uses modern JavaScript and TypeScript features.
- It provides a structured way to build apps using modules, controllers, and services.
- It supports dependency injection, which helps manage your code better.
- It works great with REST APIs, GraphQL, WebSockets, and more.

# Main concepts in Nest.js

1. Modules

- Think of modules as containers or groups that organize your app’s components. Every Nest.js app has at least one root module called AppModule. Modules help keep related code together.

2. Controllers

- Controllers handle incoming requests (like HTTP requests) and send responses back. They are like the “traffic cops” directing the requests to the right place.

3. Services

- Services contain business logic and are used by controllers. They do the actual work like fetching data from a database or processing information. They can be injected into controllers using dependency injection.

4. Providers

- Providers are any class that can be injected via dependency injection. Services are the most common providers.

5. Dependency Injection (DI)

- DI is a design pattern that makes it easier to manage dependencies between classes. Instead of creating instances manually, Nest injects them for you, making testing and maintenance easier.

6. Decorators

- Nest.js uses decorators (special annotations) like @Module(), @Controller(), @Injectable(), and @Get() to define how classes and methods behave.

# How does Nest.js work? (Basic flow)

1. You define a module to organize your app.
2. Inside the module, you create controllers to handle HTTP requests.
3. Controllers use services to perform the logic.
4. Requests come in → Controller routes them → Service does the work → Controller sends back response.

# Summary

- Nest.js = A framework to build backend apps in Node.js using TypeScript.
- Uses modules, controllers, services to organize code.
- Uses decorators to declare routes and behaviors.
- Supports dependency injection for cleaner, testable code.
- Helps build scalable and maintainable server applications.

# 👀 Example of Nest JS

```JS
//Example 1:
//1. Define a Task interface

export interface Task {
  id: number;
  title: string;
  completed: boolean;
}

//2. Create a Service to handle tasks logic
//Services contain the business logic — like storing tasks, adding new tasks, etc., interaction with DB

import { Injectable } from '@nestjs/common';
import { Task } from './task.interface';

@Injectable()  // @Injectable() means this class can be injected elsewhere (like in controllers).
export class TasksService {
  private tasks: Task[] = [];
  private idCounter = 1;

  findAll(): Task[] {  //returns all tasks.
    return this.tasks;
  }

  create(title: string): Task {  //adds a new task with a unique ID and returns it.
    const newTask: Task = {
      id: this.idCounter++,
      title,
      completed: false,
    };

    this.tasks.push(newTask);

    return newTask;
  }
}

//3. Create a Controller to handle HTTP requests
//Controllers route incoming requests and send responses.

import { Controller, Get, Post, Body } from '@nestjs/common';
import { TasksService } from './tasks.service';
import { Task } from './task.interface';

@Controller('tasks')   //@Controller('tasks') means this controller handles routes starting with /tasks. @Controller('tasks') means the base route is /tasks
export class TasksController {

  constructor(private readonly tasksService: TasksService) {}  //Constructor injects the TasksService

  //@Get() method returns all tasks. @Get() means this method responds to HTTP GET requests at /tasks.  If you want to change the route  to tasks/getall <-- we put @Get(“getall”)
  //If you want the URL to be /tasks/getAllTasks, you can specify the path inside the @Get() decorator:  @Get('getAllTasks')
  @Get()  //@Get() without any parameters means this method handles GET requests on exactly /tasks.
  getAllTasks(): Task[] {
    return this.tasksService.findAll();
  }

  @Post()  //@Post() method creates a new task using the title sent in the request body.
  createTask(@Body('title') title: string): Task {
    return this.tasksService.create(title);
  }
}

////////////////////////////////////////////////////////////////////////

❌ //@Get() can have only one method, with the same path, can’t have more than 1 method -> can't have two or more @Get() in the same controller

//If we will have more than 1 @Get() this will through an error -->
@Controller('tasks')
export class TasksController {
  @Get()
  getAllTasks(): Task[] {
    return this.tasksService.findAll();
  }

  @Get()
  getSomethingElse(): string {
    return 'Something else';
  }
}

❌ //What will happen?

//Nest.js will throw an error at runtime.
//Why? Because both methods are trying to handle the exact same route: GET /tasks — this causes a conflict.

//Nest can’t decide which method to call for that URL.

// You must give each @Get() decorator a unique path if you have multiple GET handlers in the same controller: @Get() and @Get('somethingElse')

❌// Summary:
// You cannot have two identical routes with the same HTTP method in the same controller.
// You can have multiple handlers with different paths.

// Summary:
// @Get() → responds to /tasks
// @Get('getAllTasks') → responds to /tasks/getAllTasks


// The route path and HTTP method are controlled by the decorators, not by the function name. The method name , for example- getAllTasks(): doesn’affect anything . It is used for Readability and maintainability, Code organization, Code organization .  getAllTasks ß can have any name, doesn’t affect anything


// So, why name the method getAllTasks()?
// Readability and maintainability
// Method names help humans understand what the code does. getAllTasks() clearly says this method fetches all tasks.
// Code organization
// If your controller has multiple GET endpoints, naming methods clearly helps you and others quickly know their purpose.
// Function reference
// You need a method to handle the request. The method name is just a regular TypeScript function name.


//4. Define the Module
//Modules tie everything together.

import { Module } from '@nestjs/common';
import { TasksController } from './tasks.controller';
import { TasksService } from './tasks.service';

@Module({
  controllers: [TasksController],
  providers: [TasksService],
})

export class TasksModule {}


// How it works when running:
// If you send GET /tasks, Nest calls getAllTasks() → returns list of tasks.
// If you send POST /tasks with JSON { "title": "Learn Nest.js" }, Nest calls createTask() → adds a new task → returns the new task.


// Summary of this example:
// Service holds and manipulates data.
// Controller handles HTTP requests and calls service methods.
// Module organizes controller and service.
```

# 👀 Nest JS + PSQL example

Here we have an example without using an ORM, using a plain PostgreSQL client library and writing SQL queries directly.

1. Install pg package

```JS
npm install pg
```

2. Create a PostgreSQL database connection provider

-Create a dedicated database provider to handle the connection pool.
-Create a file src/database/database.providers.ts:

```JS
import { Pool } from 'pg';

export const databaseProviders = [
  {
    provide: 'PG_CONNECTION',
    useFactory: async () => {
      const pool = new Pool({
        host: 'localhost',
        port: 5432,
        user: 'your_db_username',
        password: 'your_db_password',
        database: 'your_db_name',
      });

      await pool.connect(); // test connection

      return pool;
    },
  },
];
```

3. Create a DatabaseModule to export the connection

-Create src/database/database.module.ts:

```JS
import { Module } from '@nestjs/common';
import { databaseProviders } from './database.providers';

@Module({
  providers: [...databaseProviders],
  exports: [...databaseProviders],
})

export class DatabaseModule {}
```

4. Create UsersService to run SQL queries

-Create src/users/users.service.ts:

```JS
import { Inject, Injectable } from '@nestjs/common';
import { Pool } from 'pg';

export interface User {
  id: number;
  name: string;
  email: string;
}

@Injectable()
export class UsersService {
  constructor(@Inject('PG_CONNECTION') private pool: Pool) {}

  async findAll(): Promise<User[]> {
    const { rows } = await this.pool.query('SELECT * FROM users');

    //if users not found, throw an error, handling error in controller
    if(!rows){
      // return { message: `Users not found` };
        throw new Error(`Users not found`); //will return to controller - this message, "error": "Not Found","statusCode": 404
    }

    //if found, return the
      return rows;
  }

  async findOne(id: number): Promise<User | null> {
    const { rows } = await this.pool.query('SELECT * FROM users WHERE id = $1', [id]);
    return rows[0] || null;
  }

  async create(user: Partial<User>): Promise<User> {
    const { name, email } = user;
    const { rows } = await this.pool.query(
      'INSERT INTO users(name, email) VALUES ($1, $2) RETURNING *',
      [name, email],
    );
    return rows[0];
  }

  async remove(id: number): Promise<void> {
    await this.pool.query('DELETE FROM users WHERE id = $1', [id]);
  }
}
```

5. UsersController remains mostly the same

-Create src/users/users.controller.ts

```JS
import { Controller, Get, Post, Delete, Param, Body } from '@nestjs/common';
import { UsersService, User } from './users.service';

@Controller('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Get()
  findAll(): Promise<User[]> {

// try-catch block to handle potential errors when getting all users from Database
    try{
      return this.usersService.findAll();
    }catch(error){
        //error is received from the users.services method when users are not found
      //findAll method in users.service.ts file throws an error if the users not found

      // throw new Error(error.message);
      throw new NotFoundException(err.message);
    }

  }

  @Get(':id')
  findOne(@Param('id') id: string): Promise<User | null> {
    return this.usersService.findOne(+id); //+id <-- converts string to number
  }

  @Post()
  create(@Body() userData: Partial<User>): Promise<User> {
    return this.usersService.create(userData);
  }

  @Delete(':id')
  remove(@Param('id') id: string): Promise<void> {
    return this.usersService.remove(+id);
  }
}
```

6. UsersModule

-Create src/users/users.module.ts:

```JS
import { Module } from '@nestjs/common';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { DatabaseModule } from '../database/database.module';

@Module({
  imports: [DatabaseModule],
  controllers: [UsersController],
  providers: [UsersService],
})

export class UsersModule {}
```

7. Import UsersModule in AppModule

-Edit src/app.module.ts

```JS
import { Module } from '@nestjs/common';
import { UsersModule } from './users/users.module';

@Module({
  imports: [UsersModule],
})

export class AppModule {}
```

8. Create users table in PostgreSQL

-Run this SQL manually in your database

```JS
CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE
);
```

Summary:

- You use pg client directly to run raw SQL queries.
- Database connection pool is injected via Nest.js provider system.
- Service runs queries with parameters to avoid SQL injection.
- Controller exposes API endpoints as before.

# 👉 NestJS START

- you need Node JS and npm package installed

- then we need to install NEST.JS CLI

```JS
//in terminal write
npm install -g @nestjs/cli@latest
//-g → installs globally so you can use the nest command anywhere
//@latest → ensures you get the most recent version.

//or
sudo npm install -g @nestjs/cli@latest
//or
npm i -g @nestjs/cli

//OR

🔥 npx @nestjs/cli new project-name
//npx lets you run CLI tools without installing them globally.
//npx automatically downloads and runs the Nest CLI for this command only

//nest.js CLI --> it is the way to help you to generate new projects and also has a couple other extra commands and create new files for you and it will make it easier
```

To create new application with the NEST CLI you just have to use

```JS
nest new nestProjectName  //with NEST CLI
//then use npm package manager

🔥 npx @nestjs/cli new myappName   //without NEST CLI
```

Here you can see benefits of the NEST CLI - it starts to inform you of what is a recommended folder structure, what is a recommended file naming convention. You can of course do all these manually - creating folder - ninjas and write correct file names if you wanted. But is saves you a lot of time in terms of generating files and hooking things up.

```JS
//use NEST CLI commands:

nest g resource users  //<--for example, if you want to give a the name of resource -users,
// //if you run this command -> it will create entire resource -users (folder with models,controllers,services and DTOs) with one command

nest generate module ninjas //will create module with ninjas name
nest g module ninjas //<-- //the same but short version, we create new module, with name ninjas,
////we put - nest generate module ->  and then give the name of the resource that you are trying to create
//This comand will create new file in this direction (will create ninjas folder automatically)--> src/ninjas/ninjas.module.ts
//Also, will update file automatically--> src/app.module.ts our new module will be added to --> imports. starting to build that dependency tree.
//When you are creating new modules make sure it is being added to -> app.module.ts file - to imports array of another module. This is how things are registred in NEST.JS.

nest g service ninjas  //<--//we create new service, with name ninjas,
// this command will create -> src/ninjas/ninjas.service.spec.ts,
// //will create -> src/ninjas/ninjas.service.ts,
// //will update -> src/ninjas/ninjas.module.ts (this will add [NinjasService] to providers), to register that service

nest g controller ninjas  //<-- we create new controller, with name ninjas,
// //this command will create -> src/ninjas/ninjas.controller.spec.ts,
// //wil create -> src/ninjas/ninjas.controller.ts
//will update -> src/ninjas/ninjas.module.ts --> (this will add [NinjasController] to controllers), to register that controller


```

# Avoid error before pushing Nest.JS and/or Next.JS

- after creating new NEST.JS --> ninja-api folder using NEST CLI - it creates hiden .git folder, it causes .git which can cause error when you push the code to GitHub. Your main folder Next_AND_NEST folder has a .git folder. After command - "nest new ninja-api" ,NEST CLI automatically creates its own .git folder inside ninja-api folder. Now you have a .git folder inside .git folder, this is called nested Git repo, and Git doesn't like it. VS Code and GitHub are confused because ninja-api is not part of the main repository remote tracking, ninja-api has no remote link.To solve this error ->
  - open termimal and navigate to your NEST JS project folder
  - type command: ls -a (list all files, including hidden files)
  - rm -rf .git (delete .git folder inside Nest.JS)

If you don't delete .git inside your Nest.Js project and push the code to GitHub it will create Git folder with arrow on GitHub --> "Git submodule", it is not a regular folder, you can't open this folder

Next.js also has its own .git hidden file

```JS
//Remove the submodule reference from your main repo:

git rm --cached bankapp  //bankapp  <-- folder to remove Git submodule
rm -rf .git/modules/bankapp

git commit -m "Remove bankapp submodule"
git add bankapp
git commit -m "Add bankapp as regular folder"
git push origin main

```

# ✅ To run NEST.JS

```JS
npm run start:dev  //Start the server
```

# 🗃️ Main locations of different files (Structure and components of the NEST.JS)

```JS
/src/main.ts  //enty point for your application, have connection PORT, root module

```

When you make a URL request with some method, it goes to Controller first then that it forwards to our service and it will returns back to controller to give a response back.

```JS
HTTP GET/ --> Controller --> Service
                                |
response  <-- Controller <-------
```

- it has high level architecture that NEST JS is telling you to use.
- Controllers -> defining the Routes. Routes represented by methods and those methods ulimately call our Services.
- Services - have your business logic and any sort of reusable logic that you want to use across other services or other controllers that you have

# 🔥 3 Main components in NEST - Modules, Controllers and Services

# 🔴 Modules in NEST

see --> /src/app.module.ts

- module has your own controllers and providers.
- You can think of this app.module.ts as the root of our application and we can add here additional modules to our application. These modules can be grouped and encapsulated. Most projects your modules will probably be for each feature and each of these features might have their own Routes, their own business logic. For example we can make - Users modules where we can add, delete and edit users etc.
- Everything starts at the root module - app.module. Also, modules can depend on other modules. It will make up with some kind of tree structure that always has to have at least one root module.

To create new module and add it to our application we need to use Nest CLI that we installed

```JS
//command to create new module
//we put - nest generate module ->  and then give the name of the resource that you are trying to create
nest generate module ninjas //will create module with ninjas name

//or
nest g module ninjas  //the same but short version
```

- This comand will create new file in this direction (will create ninjas folder automatically)--> src/ninjas/ninjas.module.ts
- Also, will update file automatically--> src/app.module.ts
  our new module will be added to --> imports. starting to build that dependency tree.
- When you are creating new modules make sure it is being added to -> app.module.ts file - to imports array of another module. This is how things are registred in NEST.JS.
- You don’t need to manually register NinjaController, UsersController, NinjaService, or UsersService in your main AppModule if they’re already declared in their own feature modules (NinjasModule, UsersModule).

```JS
//Then, in your AppModule, you just import those modules:

@Module({
  imports: [NinjasModule, UsersModule],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}

//By importing NinjasModule and UsersModule, all their controllers and providers automatically become part of the application’s dependency graph.


/////////////////////////

@Module({
  controllers: [NinjasController],
  providers: [NinjasService],
  exports: [NinjasService], // optional: only if you want to use this service in other modules
})

export class NinjasModule {}
```

- You would only add controllers or providers to AppModule if they:
  Don’t belong to a feature module, or
  Need to be globally available (e.g. global interceptors, guards, or filters).
- After creating new Module we need to add new controller and new service for this.

# 🔴 Controllers

```JS
//using NEST CLI command - we create new controller, with name ninjas
nest g controller ninjas

//this command will create -> src/ninjas/ninjas.controller.spec.ts
//wil create -> src/ninjas/ninjas.controller.ts
//will update -> src/ninjas/ninjas.module.ts --> (this will add [NinjasController] to controllers), to register that controller
```

- Controllers define the routes and allow to parse values from the requests and queries from URL, from request body and then we can pass these data down to services or providers, where we have all the logic
- at the high level controllers are in charge of defining the path, what are the HTTP methods for each of these path.
- @Controller('ninjas') - tells prefix of your route for all methods within this controller
- then we need to export a class --> export class NinjasController.
- inside your controller you need to define your Routes
- @Get() - inside get method we can provide optional path --> @Get('black'). This method will have "ninjas/black" path

1. URL path main structure

```JS
//see --> ninjas.controller.ts

@Controller('ninjas') //this decorator tells that everything withing this controller will have URL path - /ninjas
export class NinjasController {....}


//if you want to make a GET request to /ninjas, you would define a method in this controller with the @Get() decorator, like so:
// GET  /ninjas  -->  use this decorator -> @Get() and we will get all ninjas as an array []
// GET  /ninjas/:id  --> use this decorator -> @Get(':id') , it is taking in a id aparameter. and we will get a single ninja by id as an object {...}
// POST /ninjas  --> use this decorator -> @Post() and we will create a new ninja and return the created ninja as an object {...}
// PUT  /ninjas/:id  --> use this decorator -> @Put(':id') and we will update an existing ninja by id and return the updated ninja as an object {...}
// DELETE /ninjas/:id  --> use this decorator -> @Delete(':id') and we will delete an existing ninja by id and return a success message or the deleted ninja as an object {...}
```

```JS
//see --> ninjas.controller.ts

//example 1
@Controller('ninjas') // This decorator defines a controller that will handle requests to the 'ninjas' path.
export class NinjasController {
  //
  //GET /ninjas --> []
  //This provides an idea for NEST.JS that "/ninjas" path with "GET" method returns all ninjas
  @Get() // This decorator defines a GET method request to the 'ninjas' path.
  getNinjas() {  //method for a current controller, method name can have any name, but it is better use more logical names for you current method. GetNinjas method should return all ninjas array
    return [];
  }
}
```

```JS
//example 2
////We have different decorators for different HTTP methods - CRUD operations
//can't have the same method with the same Route within one controller- can't have @GET()  and another @GET(), can have @GET('additinalPathHere')

import { Controller, Delete, Get, Post, Put } from '@nestjs/common';

@Controller('ninjas')
export class NinjasController {

    @Get()
  getNinjas() {
      return ['ninja1', 'ninja2', 'ninja3'];
  }


  //GET params from URL
  @Get(':id') //parameter "id" inside Get decorator
  //to parse out this id from request to make some logic in our method below
  //@Param('id')  --> //Param decorator helps to get id
  //id: string  --> this needs for TypeScript to give id a data type
  getNinjaById(@Param('id') id: string) {  //Param decorator helps to get id from the URL
    return {id};
  }


  @Post()
  createNinja() {
    return {};
  }


  @Put(':id')
  updateNinja(@Param('id') id: string, @Body() body: any) {
    return {id,...body};
  }


  @Delete(':id')
  deleteNinja(@Param('id') id: string) {
    return {};
  }
}
```

2. URL can also include a query. For example we can filter ninjas by the typy of ninja that you are getting back from URL query

```JS
//you URL might look something like this-->
//GET  /ninjas?type=fast --> []  //filter and find all fast ninjas by type
```

- Now we need to parse that out of the URL
- it is very similar approach to @Param, but istead of @Param -> we use @Query

```JS
//see --> ninjas.controller.ts

  //@Query('type')  --> //Query decorator helps to get a value from "type" query
  //type: string  --> this needs for TypeScript to give type a data type
 @Get()
  getNinjas(@Query('type') type: string) {  //type is a query name
    return {type};
  }
```

3. Post method, Here we need to parse @Body request, to get body passed object

- We use this method with Dto, we create a dto folder inside ninjas folder where we will keep all our DTO for ninjas

```JS
//see --> ninjas.controller.ts

//POST /ninjas --> {...}
  @Post() // This decorator defines a method that will handle POST requests to the 'ninjas' path.
  createNinja(@Body() body: any) {   //@Body decorator allows you to parse out request body
    //method for a current controller
    return {
        body.name
    };
  }
```

4. Update the data, we need to get @Body and @Param

```JS
//see --> ninjas.controller.ts

  //PUT  /ninjas/:id --> {...}
  @Put(':id')
  updateNinjaById(@Param('id') id: string,  @Body() updateNinjaDto: UpdateNinjaDto) {
    return { id: id, name: 'ninja' + id };
  }
```

# 🔴 Services (or Providers)

```JS
//using NEST CLI command - we create new service, with name ninjas
nest g service ninjas

//this command will create -> src/ninjas/ninjas.service.spec.ts
//will create -> src/ninjas/ninjas.service.ts
//will update -> src/ninjas/ninjas.module.ts (this will add [NinjasService] to providers), to register that service
```

- all API logic live in Providers or Services.
- Providers is just a class just like anything else in NEST but they specifically have an @Injectable decorator, this provider is something that can be injected into any class that depends on it.

# What is Dependency Injection and how it works?

🔥 To understand this concept better lets make an example:

- We mentioned that our API should manage our collection of ninjas. The Ninjas collection in real application will be stored in DB but in your own project it can be stored locally in "Services".
- Then In Services we can make some methods where will be all the logic.
- From controller we call this "Services" methods.

```JS
//see --> ninjas.service.ts

@Injectable() //Injectable decorator
export class NinjasService {

//This is our local data storage for ninjas
  private ninjas = [
    { id: 1, name: 'Naruto Uzumaki', rank: 'Genin', weapon: 'stars' },
    { id: 2, name: 'Sasuke Uchiha', rank: 'Genin', weapon: 'nunchucks' },
    { id: 3, name: 'Sakura Haruno', rank: 'Chunin', weapon: 'sward' },
  ];

 //method the return all ninjas
  getNinjas() {
    return this.ninjas;
  }

}
```

- to get acces to "Service" methods in controller we need to instantiate an instance of "Service" class (create an object from "Service" class)

```JS
//example of ninjas.controller.ts (NOT GOOD PRACTICE) ❌
//We can create instantiate an instance of "Service" class in each method - GET,POST,PUT,DELETE,
//Example without injection of ninja service class

@Controller('ninjas')
export class NinjasController {

    @Get()
    getNinjas(@Query('weapon') weapon: 'stars' | 'nunchucks' | 'sward') { //method getNinjas,in a wapon variable it can receive - 'stars' | 'nunchucks' | 'sward'

    const service = new NinjasService();  //we create an object from NinjasService to use its method (from ninjas.service.ts)

    //use NinjasService method --> getNinjas, and passing optional "weapon" filter
    return service.getNinjas(weapon);
  }


@Post(){
    ...
    const service = new NinjasService();
}


@Put(){
    ...
    const service = new NinjasService();
}


}
```

This example above is not a great option, it takes a lot of time to instanciate an instance of "Service" class in all Routes/ all methods in controller. It will be nice if that was just the instance that was created for us and injected into out Ninjas conntroller and that is actually what NEST.JS can do for you -->

```JS
//ninjas.controller.ts
//GOOD PRACTICE ✅

@Controller('ninjas')
export class NinjasController {

    //we add a new constructor, and inside we put ninjasService1 - it is a parametr with data type = NinjasService (=== the name of our service that we are injecting)
    constuctor (private readonly ninjasService1: NinjasService){}

    @Get()
    getNinjas(@Query('weapon') weapon: 'stars' | 'nunchucks' | 'sward') {
        //we don't need to create new object from Service class to use its methods anymore
    return this.ninjasService1.getNinjas(weapon);
  }

}
```

- What is happening here? we can inject our "service" class because we have @Injectable decorator in our service class. So we are telling NEST.JS this is a class that will be instanciating/injecting this class and then you can automatically inject it to anything that depends on it.

```JS
//ninjas.service.ts

@Injectable() //Injectable decorator
export class NinjasService {
    //some code
}
```

We never instanciate the Ninjas controller class - NEST.JS is doing that for us. So behind the scenes NEST.JS is doing -->

```JS
const controller = new NinjasController()
//and
const service = new NinjasService()
```

- This way - you can inject a provider into a service, you can use multiple services that inject other service, controllers can inject multiple services it depends what you are trying to build

⭐ This is what Dependency Injection is

# Create entire resource(folder with models,controllers,services and DTOs) with one command

There is also a CLI command that allows you to generate an entire resource along with controllers and providers all in one command

```JS
//for example, if you want to give a the name of resource -users
//if you run this command -> it will create entire resource -users (folder with models,controllers,services and DTOs) with one command
nest g resource users

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//Also, we can specify the dry-run command if you want. This command will show how much space each file will take without creating any file
nest g resource users --dry-run
//--dry-run → A flag that simulates the command without actually creating any files.
//When you include --dry-run (or the short version -d), NestJS will:
// -Will show those files that can be created or modified, without actually creating any files to your disk.
//This is useful when you want to preview the effects of the command before running it for real.

//When you run:
nest g resource users --dry-run
//You might see something like this:

//CREATE src/users/users.controller.ts (88 bytes)
//CREATE src/users/users.service.ts (108 bytes)
//CREATE src/users/users.module.ts (252 bytes)
//CREATE src/users/dto/create-user.dto.ts (34 bytes)
//CREATE src/users/dto/update-user.dto.ts (36 bytes)
//CREATE src/users/entities/user.entity.ts (36 bytes)

//INFO  Dry run mode -- no files were created
```

- then you will see that it will ask you what transport layer do you use?
  -REST API
  -GraphQL
  -Microservice
  -WebSockets

We pick REST API (for example) and then you will be asked another question:

- Would you like to generate CRUD entry points? (Y/n) -yes (it will create CRUD endpoints in the users.controller.ts file- GET,POST,PATCH,DELETE)

this will create some more of new files -->

```JS
//this command will add [UsersModule] into imports array in the app.module.ts file (root module file)
//this command will create folder users, inside will have dto folder, entities folder and ->

CREATE src/users/users.controller.spec.ts
CREATE src/users/users.controller.ts
CREATE src/users/users.module.ts
CREATE src/users/users.service.spec.ts
CREATE src/users/users.service.ts

CREATE src/users/dto/create-user.dto.ts
CREATE src/users/dto/update-user.dto.ts
CREATE src/users/entities/user.entity.ts

UPDATE package.json
UPDATE src/app.module.ts
```

# ✔️ Exception Handling, to handle different errors

[ --> Different exception options <-- ](https://docs.nestjs.com/exception-filters)

Most used exceptions:

- BadRequestException
- UnathorizedException
- NotFoundEception
- ForbiddenException
- and etc.
- can create your own custome exceptions

for example: when you throw an error page, or 404 - not found page

```JS
//throw an error example
//see --> ninjas.service.ts  line 30

getNinjaById(id: number) {
    const ninja = this.ninjas.find((ninja) => ninja.id === id);

    if (!ninja) {
      throw new Error(`Ninja with id ${id} not found`);  //throws an error
    }

    //if found, return the ninja
    return ninja;
  }
```

```JS
//throw not found
//see --> ninjas.controller.ts  line 64

//if id is not correct it will throw NotFoundException
@Get(':id')
getOneNinja(@Param('id') id: string) {
  // return { id: id, name: 'ninja' + id }; // Return an object with the id, No service used here

  //try-catch block to handle potential errors when getting a ninja by id
  try {
    return this._ninjasServer.getNinjaById(Number(id)); //invoking getNinjaById method from NinjasService to get a ninja by id
    //turn string to number use +id --> return this._ninjasServer.getNinjaById(+id);
  } catch (err) {
    throw new NotFoundException(err.message);
  }
}
```

- This way we are telling Nest JS how to behave in these occasions (when id is not correct, etc.)
- if we don't use "try-catch" block, to catch the error --> by default Nest JS will respond as:

```JS
{
  "statusCode": 500,
  "message": "Internal server error"
}
```

but we want for example show -statusCode: 404 and message: not found, therefore we need to use 'try-catch' block and handle exceptions

- then in services we can throw an error (see ninjas.service.ts line 30) --> then send this response back to controller and in controller throw an exception (see ninjas.controller.ts line 67), telling Nest.JS how we want Nest.JS to behave in different occasions

# Advance exception handling (not need for now)

Also, if you have DB driver that throws very specific exception when it can't find a record in the database, in this case you might want to set up an exception filter that automatically catches that specific exception and responds with a 404, that way you don't need use try-catch block. You can extend and customize this behaviour.

```JS

import { ExceptionFilter, Catch, ArgumentsHost, HttpException } from '@nestjs/common';
import { Request, Response } from 'express';

@Catch(HttpException)   //<---- use @Catch
export class HttpExceptionFilter implements ExceptionFilter {
  catch(exception: HttpException, host: ArgumentsHost) {
    const ctx = host.switchToHttp();
    const response = ctx.getResponse<Response>();
    const request = ctx.getRequest<Request>();
    const status = exception.getStatus();

    response
      .status(status)
      .json({
        statusCode: status,
        timestamp: new Date().toISOString(),
        path: request.url,
      });
  }
}

```

# ⚠️ Pipes in Nest.js

[ --> Pipes <--](https://docs.nestjs.com/pipes)

-Within ninja DTO we MUST to install 2 new packages to our application --> ( class-validator and class-transformer ) to use transformation and validation pipes in Nest.js

```JS
npm i --save class-validator class-transformer
```

Pipes have 2 core use cases: (from link above)

- Transformation: transform input data to the desired form (e.g., from string to integer)
- Validation: evaluate input data and if valid, simply pass it through unchanged; otherwise, throw an exception

#### Example of transformation in Nest.js - specific pipe called -> "the transformation pipe".

Pipes allow to transform data types automatically, see example below -->

```JS
//in ninjas.controller.ts file we could use pipes and change our code to this one

//URL parameter always comes as a string
//we receive "id" as a string in @Param
//ParseIntPipe - allow to transform --> "id" as a number
//there is other build in pipes that you can use, you need to type -> Parse and you will see other options(such as: ParseArrayPipe, ParseBoolPipe, ParseEnumPipe, ParseFloatPipe, ParseUUIDPipe, etc.)
@Get(':id')
  getOneNinja(@Param('id', ParseIntPipe) id: number) {

    try {
      return this._ninjasServer.getNinjaById(id);
    } catch (err) {
      throw new NotFoundException(err.message);
    }
  }


//WITHOUT Transformation pipe/ usaul approach
//in ninjas.controller.ts

  @Get(':id')
  getOneNinja(@Param('id') id: string) {

    try {
      return this._ninjasServer.getNinjaById(Number(id));
    } catch (err) {
      throw new NotFoundException(err.message);
    }
  }
```

Or pipes allows to validate a request body, we can validate does the ninja receives correct weapon, or maybe ninja name should be certain length etc.

[ --> class-validator <--](https://github.com/typestack/class-validator/blob/develop/README.md)

class-validator is a couple extra set of decorators that you can add to your classes, (adding validation - title Length should be between 10 and 20, etc.)

```JS
export class Post {
  @Length(10, 20)  //title Length should be between 10 and 20
  title: string;

  @Contains('hello')  //text must contain word - "hello"
  text: string;

  @IsInt()  //rating should be a Integer
  @Min(0)  //min 0
  @Max(10)  //max 10
  rating: number;

  @IsEmail()   //patern for email, chech that email is filled correctly
  email: string;

  @IsFQDN()
  site: string;

  @IsDate()  //patern for date, also another patern can be for phone nr.
  createDate: Date;
}

//there is a lot of decorators that can be applied to your class for validation
//also can create your own custom decorators
```

#### Example of validation in Nest.js - specific pipe called -> "the validation pipe".

1. Below We are providing additional metadata to this class in order for Nest.js to take advantage of these decorators

```JS
//example
//create-ninja.dto.ts
import { MinLength } from "class-validator";

export class CreateNinjaDto {
  @MinLength(3)  //min length must be at least 3 letters for a "name"
  name: string

  @IsEnum(['stars', 'nunchucks'], {message: "use correct weapon"}) //weapon can be only these names, also can add custom message if it fails validation

  // weapon: string
  //or
  weapon: "stars" | "nunchucks";
  .....

}
```

2. Then we need to add specific pipe called - "the validation pipe".

```JS
//ninjas.controller.ts


//@Body(new ValidationPipe() - this is build in pipe that Nest.js specifically looks at your DTOs, your objects and checks to see what decorators you have for validation in there and then compares it with the object that is comming in --> createNinjaDto1
  @Post()
  createNinja(@Body(new ValidationPipe()) createNinjaDto1: CreateNinjaDto) {
    return this._ninjasServer.createNinja(createNinjaDto1); //invoking createNinja
  }
```

3. If avlidations are not met, Nest.js will respond with error object

🧨 You can use other types of validators apart from 'class-validator', you can use 'joy' and others. But 'class-validator' feels cohesive to the rest of the Nest.js framework.

Also, We can create custome Pipes

# ✅ Guards - it is a way how you protect your routs

[ --> GUARDS <-- ](https://docs.nestjs.com/guards)

Guards have a single responsibility to protect the underlying Routes based on some kind of logic.

For example:

- Authentication and Authorization. Perhaps you want to protect an endpoint to make sure that a user is already Logged-In before they can use specific Route.

- Or maybe you are trying to protect an endpoint to make sure that only a specific type of user can use that Route. For example, an Admin can only change a specific setting that is Guards are for.

```JS
//in ninjas.controller.ts
//If we want to protect a specific route, for example this route to create ninjas

@Post()
createNinja(@Body() createNinjaDto1: CreateNinjaDto) {
  return this._ninjasServer.createNinja(createNinjaDto1); //invoking createNinja
}
```

### How to start using Guards?

In your main project folder - src folder, in terminal we put -->

```JS
nest g guard nameOfTheGuard   //can have any name



//for example, you need to have a black belt to use specific Route to create a new ninja in the database:
nest g guard belt

//it will create these files below:
src/belt/belt.guard.spec.ts
src/belt/belt.guard.ts
```

```JS
//belt.guard.ts

//This is another class with @Injectable, kind of like a provider but it very specifically implements the "CanActivate" Interface. And if you mouse over on "CanActivate" you can see it must return boolean --> true or false

@Injectable()
export class BeltGuard implements CanActivate {  //Guard name - BeltGuard
  canActivate(
    context: ExecutionContext,
  ): boolean | Promise<boolean> | Observable<boolean> {
    return true;
  }
}
```

So the core idea behind Guards is that you can attach a Guard either to an entire controller or individual methods in that controller (similar to ASP.NET Core MVC --> see template below)

```JS
//similar logic in .NET MVC controller file

[Authorize] //Authorize - attribute, only logedIn user will be able to access this method
[HttpGet]
public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0){
///some logic
    return View();
}
```

For example: you can add - @UseGuards and provide your guard in there --> @UseGuards(BeltGuard), because we have thisname in belt.guard.ts file - line 5. So you can have a Guard in front of an entire controller which means that it is going to sit in front of all underlying routes. So if you trying to protect all of the Ninja Routes - all routes in current controller. You do this way. See example below.

```JS
//ninjas.controler.ts

@Controller('ninjas') // This decorator defines a controller that will handle requests to the 'ninjas' path.
@UseGuards(BeltGuard) //<----Guard
export class NinjasController {

  constructor(public readonly _ninjasServer: NinjasService) {}

  @Get()
  getNinjas(@Query('weapon') weapon: 'stars' | 'nunchucks' | 'sward') {
    return .....
  }

  @Get(':id')
  getOneNinja(@Param('id') id: string) {
    return...
  }

  //other Routes
}
```

If you want to protect individual Route, you move this Guard to individual Routes, for example we want to protect this Route, to create new ninja -->

```JS
  @Post()
  @UseGuards(BeltGuard) //<----use Guard in this method only
  createNinja(@Body() createNinjaDto1: CreateNinjaDto) {
    return this._ninjasServer.createNinja(createNinjaDto1);
  }
```

Guards work at a super high level. For example if BeltGuard returns true, then it has an access to those Routes that are protected with Guards. If BeltGuard will return false, then those Routes won't be accessable. See belt.guard.ts file.

Guard has a single purpose of allowing something to move forward or not based on the logic that happens in our case in belt.guard.ts file.
