# NEST JS Framework

- One of the best framework for Node JS
- Node.js Framework for building efficient, reliable and scalable server-side applications
- Nest.JS is cover/abstruction over express.js. NEST JS takes architecture and uses express under the hood.

# START

- you need Node JS and npm package installed

- then we need to install NEST.JS CLI

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

- after creating new NEST.JS --> ninja-api folder using NEST CLI - it creates hiden .git folder, it causes .git which can cause error when you push the code to GitHub. Your main folder Next_AND_NEST folder has a .git folder. After npx create-nest-app ninja-api ,NEST CLI automatically creates its own .git folder inside ninja-api folder. Now you have a .git folder inside .git folder, this is called nested Git repo, and Git doesn't like it. VS Code and GitHub are confused because ninja-api is not part of the main repository remote tracking, ninja-api has no remote link.To solve this error ->
  - open termimal and navigate to your NEST JS project folder
  - type command: ls -a (list all files, including hidden files)
  - rm -rf .git (delete .git folder inside Nest.JS)

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
- The same thing applies for controllers and providers keys in -> app.module.ts file (root file of our tree). You register them the same way when you add ned controllers and providers (services)
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

2. URL can also include a query. For example we can filter ninjas by the typy of ninja that you are getting back from URL query

```JS
//you URL might look something like this-->
//GET  /ninjas?type=fast --> []  //filter and find all fast ninjas by type
```

- Now we need to parse that out of the URL
- it is very similar approach to @Param, but istead of @Param -> we use @Query

```JS
//see --> ninjas.controller.ts

  //@Query('type')  --> //Param decorator helps to get a value from "type" query
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
  updateNinjaById(@Param('id') id: string) {
    return { id: id, name: 'ninja' + id };
  }
```

# 🔴 Services (or Providers)

```JS
//using NEST CLI command - we create new service, with name ninjas
nest g service ninjas

//this command will create -> src/ninjas/ninjas.service.spec.ts
//wil create -> src/ninjas/ninjas.service.ts
//will update -> src/ninjas/ninjas.module.ts (this will add [NinjasService] to providers), to register that service
```

Here you can see benefits of the NEST CLI - it starts to inform you of what is a recommended folder structure, what is a recommended file naming convention. You can of course do all these manually - creating folder - ninjas and write correct file names if you wanted. But is saves you a lot of time in terms of generating files and hooking things up.

- all API logic live in Providers or Services.
- Providers is just a class just like anything else in NEST but they specifically have an @Injectable decorator, this provider is something that can be injected into any class that depends on it. What is Dependency Injection and how it works?

🔥 To understand this concept better lets make an example:

-We mentioned that our API should manage our collection of ninjas. The Ninjas collection in real application will be stored in DB but in your own project it can be stored locally in "Services".
-Then In Services we can make some methods where will be all the logic.
-From controller we call this "Services" methods.

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
