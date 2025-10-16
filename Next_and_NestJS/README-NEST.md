# NEST JS Framework

- One of the best framework for Node JS
- Node.js Framework for building efficient, reliable and scalable server-side applications
- Nest.JS is cover/abstruction over express.js. NEST JS takes architecture and uses express under the hood.

# START

- you need Node JS and npm package installed

- then we need to install nest.js cli

```JS
//in terminal write
npm install -g @nestjs/cli@latest
//or
sudo npm install -g @nestjs/cli@latest

//nest.js CLI --> it is the way to help you to generate new projects and also has a couple other extra commands and create new files for you and it will make it easier
```

To create new application with the NEST CLI you just have to use

```JS
nest new nestProjectName

//then use npm package manager
```

# To run NEST.JS

```JS
npm run start:dev
```

# Main locations of different files (Structure and components of the NEST.JS)

```JS
/src/main.ts  //enty point for your application, have connection PORT, root module

```

When you make a URL request with some method, it goes to Controller first then that it forwards to our service and it will returns back to controller to give a response back.

HTTP GET/ --> Controller --> Service

- it has high level architecture that NEST JS is telling you to use.
- Controllers -> defining the Routes. Routes represented by methods and those methods ulimately call our Services.
- Services - have your business logic and any sort of reusable logic that you want to use across other services or other controllers that you have

# 3 Main components in NEST - Modules, Controllers and Services

# Modules in NEST

```JS
see --> /src/app.module.ts
```

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
- The same thing applies for controllers and providers keys in -> app.module.ts file (root file of our tree). You register them the same way when you add ned controllers and providers (services)
- After creating new Module we need to add new controller and new service for this.

# Controllers

```JS
//using NEST CLI command - we create new controller, with name ninjas
nest g controller ninjas

//this command will create -> src/ninjas/ninjas.controller.spec.ts
//wil create -> src/ninjas/ninjas.controller.ts
//will update -> src/ninjas/ninjas.module.ts --> (this will add [NinjasController] to controllers), to register that controller
```

- at the high level controllers are in charge of defining the path, what are the HTTP methods for each of these path.
- @Controller('ninjas') - tells prefix of your route for all methods within this controller
- then we need to export a class --> export class NinjasController.
- inside your controller you need to define your Routes
- @Get() - inside get method we can provide optional path --> @Get('black'). This method will have "ninjas/black" path

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


  @Get(':id') //parameter id insde Get decorator
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
  updateNinja() {
    return {};
  }


  @Delete(':id')
  deleteNinja() {
    return {};
  }
}
```

#3 4:31

# Services

```JS
//using NEST CLI command - we create new service, with name ninjas
nest g service ninjas

//this command will create -> src/ninjas/ninjas.service.spec.ts
//wil create -> src/ninjas/ninjas.service.ts
//will update -> src/ninjas/ninjas.module.ts (this will add [NinjasService] to providers), to register that service
```

Here you can see benefits of the NEST CLI - it starts to inform you of what is a recommended folder structure, what is a recommended file naming convention. You can of course do all these manually - creating folder - ninjas and write correct file names if you wanted. But is saves you a lot of time in terms of generating files and hooking things up.

# There is also a CLI command that allows you to generate an entire resource along with controllers and providers all in one command

```JS
//for example, giving the name of resource -users
nest g resource users --dry-run

//then also we can specify the dry-run command if you want
//if you run this command
nest g resource users
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
